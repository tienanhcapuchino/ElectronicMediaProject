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

using DocumentFormat.OpenXml.Wordprocessing;
using ElectronicMedia.Core.Automaper;
using ElectronicMedia.Core.Common.Extension;
using ElectronicMedia.Core.Repository.DataContext;
using ElectronicMedia.Core.Repository.Entity;
using ElectronicMedia.Core.Repository.Models;
using ElectronicMedia.Core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

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
                var category = await _context.PostCategories.Skip((requestBody.Page - 1) * requestBody.Top)
                    .Take(requestBody.Top).ToListAsync();
                var counitem = await CommonService.GetTotalCount<PostCategory>(_context);
                var result = QueryData<PostCategory>.QueryForModel(requestBody,category).ToList();
                return PagedList<PostCategory>.ToPagedList(result, requestBody.Page, requestBody.Top, counitem);
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

        public async Task<IEnumerable<PostCategoryDto>> GetPostCateParent()
        {
            var categorys = await _context.PostCategories.Where(x => x.ParentId == null).ToListAsync();
            var result = new List<PostCategoryDto>();
            categorys.ForEach(parent =>
            {
                var category = parent.MapTo<PostCategoryDto>();
                var chidrent = _context.PostCategories.Where(x => x.ParentId == parent.Id).ToList();
                var countPostInCategory = _context.Posts.Where(x => x.CategoryId == parent.Id).Count();
                category.CountPost = countPostInCategory;
                category.Childrens.AddRange(chidrent.MapToList<PostCategoryDto>());
                result.Add(category);
            });
            return result;
        }
        public async Task<List<PostCategory>> GetSubPostCateByParent(Guid parentId)
        {
            return await _context.PostCategories.Where(x => x.ParentId ==  parentId).ToListAsync();
        }
        public async Task<List<PostCategory>> GetAllSubCategory()
        {
            return await _context.PostCategories.Where(x => x.ParentId != null).ToListAsync();
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
        public async Task<List<PostCategory>> GetCategoryParent()
        {
            return await _context.PostCategories.Where(x => x.ParentId == null).ToListAsync();
        }
    }
}
