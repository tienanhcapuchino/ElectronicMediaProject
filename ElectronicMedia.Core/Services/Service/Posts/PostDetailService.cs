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
    public class PostDetailService : IPostDetailService
    {
        private readonly ElectronicMediaDbContext _dbContext;
        public PostDetailService(ElectronicMediaDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> Add(PostDetail entity, bool saveChange = true)
        {
            await _dbContext.AddAsync(entity);
            bool result = true;
            if (saveChange)
            {
                result = await _dbContext.SaveChangesAsync() > 0;
            }
            return result;
        }

        public async Task<bool> CreatePostDetail(PostDetailModel model)
        {
            if (model == null || model.AuthorId == null || model.PostId == null) return false;
            var entity = model.MapTo<PostDetail>();
            bool result = await Add(entity);
            return result;
        }

        public Task<bool> Delete(Guid id, bool saveChange = true)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteByUserIdAndPostId(Guid userId, Guid postId)
        {
            var postDetail = await _dbContext.PostDetails.SingleOrDefaultAsync(x => x.PostId == postId && x.UserId == userId);
            if (postDetail == null) return false;
            _dbContext.PostDetails.Remove(postDetail);
            bool result = await _dbContext.SaveChangesAsync() > 0;
            return result;
        }

        public async Task<PostDetail> FindByUserId(Guid userId, Guid postId)
        {
            return await _dbContext.PostDetails.SingleOrDefaultAsync(x => x.PostId == postId && x.UserId == userId);
        }

        public async Task<IEnumerable<PostDetail>> GetAllAsync()
        {
            return await _dbContext.PostDetails.ToListAsync();
        }

        public Task<PagedList<PostDetail>> GetAllWithPaging(PageRequestBody requestBody)
        {
            throw new NotImplementedException();
        }

        public Task<PostDetail> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Update(PostDetail entity, bool saveChange = true)
        {
            _dbContext.PostDetails.Update(entity);
            bool result = true;
            if (saveChange)
            {
                result = await _dbContext.SaveChangesAsync() > 0;
            }
            return await Task.FromResult(result);
        }
    }
}
