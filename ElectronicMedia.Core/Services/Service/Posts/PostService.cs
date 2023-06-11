using ElectronicMedia.Core.Automaper;
using ElectronicMedia.Core.Repository.DataContext;
using ElectronicMedia.Core.Repository.Entity;
using ElectronicMedia.Core.Repository.Models;
using ElectronicMedia.Core.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicMedia.Core.Services.Service
{
    public class PostService : IPostService
    {
        private readonly ElectronicMediaDbContext _context;
        private readonly IPostDetailService _postDetailService;
        public PostService(ElectronicMediaDbContext context,
            IPostDetailService postDetailService)
        {
            _context = context;
            _postDetailService = postDetailService;
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

        public Task<IEnumerable<Post>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<PagedList<Post>> GetAllWithPaging(PageRequestBody requestBody)
        {
            try
            {
                var posts = await _context.Posts.ToListAsync();
                var result = QueryData<Post>.QueryForModel(requestBody,posts).ToList();
                return PagedList<Post>.ToPagedList(result, requestBody.Page, requestBody.Top);
            }
            catch (Exception ex)
            {
                throw;
            }
        }



        public async Task<Post> GetByIdAsync(Guid id)
        {
            var post = await _context.Posts.Where(x => x.Id == id).SingleOrDefaultAsync();
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
                    result = await _postDetailService.DeleteByUserIdAndPostId(postDetailEntity.UserId, postDetailEntity.PostId);
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
        #endregion
    }
}
