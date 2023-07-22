/*********************************************************************
 * 
 * PROPRIETARY and CONFIDENTIAL
 * 
 * This is licensed from, and is trade secret of:
 * 
 *          Group 10 - PRN231 - SU23
 *          FPT University, Education and Training zone
 *          Hoa Lac Hi-tech Park, Km29, Thang Long Highway
 *          Ha Noi, Viet Nam
 *          
 * Refer to your License Agreement for restrictions on use,
 * duplication, or disclosure
 * 
 * RESTRICTED RIGHTS LEGEND
 * 
 * Use, duplication or disclosure is the
 * subject to restriction in Articles 736 and 738 of the 
 * 2005 Civil Code, the Intellectual Property Law and Decree 
 * No. 85/2011/ND-CP amending and supplementing a number of 
 * articles of Decree 100/ND-CP/2006 of the Government of Viet Nam
 * 
 * 
 * Copy right 2023 © PRN231 - SU23 - Group 10 ®. All Rights Reserved
 * 
 * Unpublished - All rights reserved under the copyright laws 
 * of the Government of Viet Nam
*********************************************************************/

using AutoMapper;
using ElectronicMedia.Core.Automaper;
using ElectronicMedia.Core.Common.Extension;
using ElectronicMedia.Core.Repository.DataContext;
using ElectronicMedia.Core.Repository.Entity;
using ElectronicMedia.Core.Repository.Models;
using ElectronicMedia.Core.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicMedia.Core.Services.Service
{
    public class PostService : IPostService
    {
        private readonly ElectronicMediaDbContext _context;
        private readonly IPostDetailService _postDetailService;
        private readonly ICommentService _commentService;
        private readonly IReplyCommentService _replyCommentService;
        private readonly IExcelService<Post> _excelService;
        public PostService(ElectronicMediaDbContext context,
            IPostDetailService postDetailService,
            ICommentService commentService,
            IReplyCommentService replyCommentService,
            IExcelService<Post> excelService
            )
        {
            _context = context;
            _postDetailService = postDetailService;
            _commentService = commentService;
            _replyCommentService = replyCommentService;
            _excelService = excelService;
        }

        public async Task<bool> Add(Post entity, bool saveChange = true)
        {
            await _context.AddAsync(entity);
            bool result = true;
            if (saveChange)
            {
                result = await _context.SaveChangesAsync() > 0;
            }
            return result;
        }


        public async Task<bool> CreatePost(PostModel post)
        {
            var categoryIds = await _context.PostCategories.Select(c => c.Id).ToListAsync();
            if (string.IsNullOrEmpty(post.Title)
                || string.IsNullOrEmpty(post.Content)
                || categoryIds == null || !categoryIds.Any()
                || !categoryIds.Contains(post.CategoryId)
                || post.FileURL == null)
            {
                return false;
            }
            var entity = post.MapTo<Post>();
            bool result = await Add(entity);
            return result;
        }
        public async Task<bool> UpdatePost(PostViewModel post)
        {
            var categoryIds = await _context.PostCategories.Select(c => c.Id).ToListAsync();
            if (string.IsNullOrEmpty(post.Title)
                || string.IsNullOrEmpty(post.Content)
                || categoryIds == null || !categoryIds.Any()
                || !categoryIds.Contains(post.CategoryId))

            {
                return false;
            }
            var entity = post.MapTo<Post>();
            bool result = await Update(entity);
            return result;
        }
        public async Task<bool> Delete(Guid id, bool saveChange = true)
        {
            bool result = true;
            var post = await GetByIdAsync(id);
            if (post == null)
            {
                return false;
            }
            _context.Posts.Remove(post);
            if (saveChange)
            {
                result = await _context.SaveChangesAsync() > 0;
            }
            return result;
        }

        public async Task<IEnumerable<Post>> GetAllAsync()
        {
            var result = await _context.Posts.ToListAsync();
            return result;
        }
        public async Task<IEnumerable<PostView>> GetNewPost(int take)
        {
            try
            {
                var post = await _context.Posts.Where(y => y.Status == PostStatusModel.Published).OrderBy(x => x.CreatedDate).Take(take).ToListAsync();
                return post.MapToList<PostView>();

            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public async Task<PagedList<PostViewModel>> GetAllWithPaging(PageRequestBody requestBody)
        {
            try
            {
                var posts = await _context.Posts.Include(x => x.User).Skip((requestBody.Page - 1) * requestBody.Top)
                    .Take(requestBody.Top).ToListAsync();
                var countItem = await CommonService.GetTotalCount<Post>(_context);
                var postModels = posts.MapTo<List<PostViewModel>>();
                var result = QueryData<PostViewModel>.QueryForModel(requestBody, postModels).ToList();
                return PagedList<PostViewModel>.ToPagedList(result, requestBody.Page, requestBody.Top, countItem);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Post> GetByIdAsync(Guid id)
        {
            var post = await _context.Posts.Include(x => x.User).Where(x => x.Id == id).SingleOrDefaultAsync();
            return post;
        }
        public async Task<PostViewModel> GetById(Guid id)
        {
            var post = await GetByIdAsync(id);
            return post.MapTo<PostViewModel>();
        }

        public async Task<bool> Update(Post entity, bool saveChange = true)
        {
            _context.Posts.Update(entity);
            bool result = true;
            if (saveChange)
            {
                result = await _context.SaveChangesAsync() > 0;
            }
            return await Task.FromResult(result);
        }

        public async Task<bool> UpdateLikeAndDislike(Guid postId, bool liked)
        {
            var entity = await GetByIdAsync(postId);
            if (entity == null)
            {
                return false;
            }
            if (liked)
            {
                entity.Like += 1;
            }
            else
            {
                entity.Dislike += 1;
            }
            bool result = await Update(entity);
            return result;
        }

        public async Task<bool> VotePost(PostDetailModel postDetail)
        {
            bool result = false;
            var postDetailEntity = await _postDetailService.FindByUserId(postDetail.AuthorId, postDetail.PostId);
            if (postDetailEntity != null)
            {
                if (postDetail.Liked != postDetailEntity.Liked)
                {
                    return false;
                }
                else
                {
                    result = await _postDetailService.DeleteByUserIdAndPostId(Guid.Parse(postDetailEntity.UserId), postDetailEntity.PostId);
                    if (result)
                    {
                        result = await UpdateLikeDelete(postDetailEntity.PostId, postDetail.Liked);
                    }
                    return result;
                }
            }
            result = await _postDetailService.CreatePostDetail(postDetail);
            if (result)
            {
                result = await UpdateLikeAndDislike(postDetail.PostId, postDetail.Liked);
            }
            return result;
        }
        public async Task<bool> DeletePost(Guid postId)
        {
            var post = await GetByIdAsync(postId);
            if (post == null) return false;
            var comments = await _commentService.GetAllCommentsByPost(postId);
            List<ReplyComment> replyComments = new List<ReplyComment>();
            List<Comment> commentEnities = new List<Comment>();
            if (comments != null && comments.Any())
            {
                commentEnities = comments.MapTo<List<Comment>>();
                var commentIds = commentEnities.Select(x => x.Id).ToList();
                replyComments = await _replyCommentService.GetAllRepliesByParentIds(commentIds);
            }
            if (replyComments.Any())
            {
                _context.ReplyComments.RemoveRange(replyComments);
            }
            if (commentEnities.Any())
            {
                _context.Comments.RemoveRange(commentEnities);
            }
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<DataTable> ExportPosts()
        {
            var posts = await _context.Posts.Include(x => x.User).Include(x => x.SubCategory).Include(x => x.Category).ToListAsync();
            DataTable dt = _excelService.ExportToExcel(posts);
            return dt;
        }
        #region private method
        private async Task<bool> UpdateLikeDelete(Guid postId, bool liked)
        {
            if (liked == null) return false;
            var post = await GetByIdAsync(postId);
            if (post == null)
            {
                return false;
            }
            if (liked)
            {
                post.Like -= 1;
            }
            else
            {
                post.Dislike -= 1;
            }
            bool result = await Update(post);
            return result;
        }
        public async Task<IEnumerable<PostView>> GetPostByCateId(Guid cateId, int top)
        {
            var post = await _context.Posts.Include(x => x.User).Where(x => x.CategoryId == cateId && x.Status == PostStatusModel.Published).OrderBy(y => y.PublishedDate).Take(top).ToListAsync();
            return post.MapToList<PostView>();
        }

        Task<PagedList<Post>> ICoreRepository<Post>.GetAllWithPaging(PageRequestBody requestBody)
        {
            throw new NotImplementedException();
        }

        public Task<List<Post>> GetPostByCateId(Guid cateId)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
