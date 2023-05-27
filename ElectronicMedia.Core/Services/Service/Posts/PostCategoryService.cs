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
    public class PostCategoryService : IPostCategoryService
    {
        private readonly ElectronicMediaDbContext _context;
        public PostCategoryService(ElectronicMediaDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Add(PostCategory entity, bool saveChange = true)
        {
            await _context.AddAsync(entity);
            bool result = true;
            if (saveChange)
            {
                result = await _context.SaveChangesAsync() > 0;
            }
            return await Task.FromResult(result);
        }
        public async Task<bool> CreatePostCate(PostCategoryModel model)
        {
            if (string.IsNullOrEmpty(model.Name))
            {
                return false;
            }
            var categories = (await GetAllAsync()).Select(x => x.Name).Where(x => x.Equals(model.Name)).ToList();
            if (categories.Any()) return false;
            PostCategory cate = model.MapTo<PostCategory>();
            bool result = await Add(cate);
            return result;
        }

        public async Task<bool> Delete(Guid id, bool saveChange = true)
        {
            var cate = await GetByIdAsync(id);
            if (cate == null) return false;
            _context.PostCategories.Remove(cate);
            bool result = true;
            if (saveChange)
            {
                result = await _context.SaveChangesAsync() > 0;
            }
            return result;
        }

        public async Task<PagedList<PostCategory>> GetAllWithPaging(PageRequestBody requestBody)
        {
            try
            {
                var category = await _context.PostCategories.ToListAsync();
                return PagedList<PostCategory>.ToPagedList(category, requestBody.Page, requestBody.Top);
            }
            catch(Exception ex) 
            {
                throw;
            }
        }

        public async Task<PostCategory> GetByIdAsync(Guid id)
        {
            return await _context.PostCategories.SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<PostCategory>> GetPostCateParent()
        {
            return await _context.PostCategories.Where(x => x.ParentId == null).ToListAsync();
        }

        public async Task<bool> Update(PostCategory entity, bool saveChange = true)
        {
            _context.PostCategories.Update(entity);
            bool result = true;
            if (saveChange)
            {
                result = await _context.SaveChangesAsync() > 0;
            }
            return await Task.FromResult(result);
        }

        public async Task<bool> UpdatePostCate(Guid id, PostCategoryModel model)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null) return false;
            entity = model.MapTo<PostCategory>();
            bool result = await Update(entity);
            return result;
        }

        public async Task<IEnumerable<PostCategory>> GetAllAsync()
        {
            return await _context.PostCategories.ToListAsync();
        }
    }
}
