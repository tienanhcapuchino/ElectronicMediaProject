using ElectronicMedia.Core.Automaper;
using ElectronicMedia.Core.Repository.DataContext;
using ElectronicMedia.Core.Repository.Entity;
using ElectronicMedia.Core.Repository.Models;
using ElectronicMedia.Core.Services.Interfaces;
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
                || !categoryIds.Contains(post.CategoryId))
            {
                return false;
            }
            var entity = post.MapTo<Post>();
            bool result = await Add(entity);
            return result;
        }

        public async Task<bool> CreatePostCategory(PostCategoryModel model)
        {
            if (string.IsNullOrEmpty(model.Name))
            {
                return false;
            }
            var categories = await _context.PostCategories.Where(x => x.Name == model.Name).Select(x => x.Name).ToListAsync();
            if (categories.Any()) return false;
            PostCategory cate = model.MapTo<PostCategory>();
            await _context.PostCategories.AddAsync(cate);
            bool result = await _context.SaveChangesAsync() > 0;
            return result;
        }

        public async Task<bool> CreateSubCategories(List<PostCategoryModel> subCategories)
        {
            foreach (var category in subCategories)
            {
                if (string.IsNullOrEmpty(category.Name))
                {
                    return false;
                }
            }
            var categories = subCategories.MapTo<List<PostCategory>>();
            await _context.PostCategories.AddRangeAsync(categories);
            bool result = await _context.SaveChangesAsync() > 0;
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
            //var postDetails = await _context.PostDetails.Where(x => x.PostId == id).ToListAsync();
            //_context.Remove(post);
            //_c
            //throw new NotImplementedException();
            _context.Posts.Remove(post);
            if (saveChange)
            {
                result = await _context.SaveChangesAsync() > 0;
            }
            return result;
        }

        public async Task<bool> EditCategory(Guid cateId, PostCategoryModel model)
        {
            if (string.IsNullOrEmpty(model.Name)) return false;
            var oldEntity = await _context.PostCategories.SingleOrDefaultAsync(x => x.Id == cateId);
            if (oldEntity == null)
            {
                return false;
            }
            var newEntity = model.MapTo<PostCategory>();
            _context.PostCategories.Update(newEntity);
            bool result = await _context.SaveChangesAsync() > 0;
            return result;
        }

        public Task<List<Post>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Post> GetByIdAsync(Guid id)
        {
            var post = await _context.Posts.Where(x => x.Id == id).SingleOrDefaultAsync();
            return post;
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
