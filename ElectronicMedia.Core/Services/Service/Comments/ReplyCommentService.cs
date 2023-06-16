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
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicMedia.Core.Services.Service
{
    public class ReplyCommentService : IReplyCommentService
    {
        private readonly ElectronicMediaDbContext _context;
        public ReplyCommentService(ElectronicMediaDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Add(ReplyComment entity, bool saveChange = true)
        {
            await _context.AddAsync(entity);
            bool result = true;
            if (saveChange)
            {
                result = await _context.SaveChangesAsync() > 0;
            }
            return result;
        }

        public async Task<bool> CreateReplyComment(ReplyCommentModel replyComment)
        {
            if (string.IsNullOrEmpty(replyComment.Content))
            {
                throw new Exception("Cannot leave content in comment is blank!");
            }
            var enity = replyComment.MapTo<ReplyComment>();
            var result = await Add(enity);
            return result;
        }

        public async Task<bool> Delete(Guid id, bool saveChange = true)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null)
            {
                throw new Exception($"Error when remove reply comment. Cannot find replycomment at: {id}");
            }
            _context.ReplyComments.Remove(entity);
            bool result = true;
            if (saveChange)
            {
                result = await _context.SaveChangesAsync() > 0;
            }
            return result;
        }

        public Task<PagedList<ReplyComment>> GetAllAsync(PageRequestBody requestBody)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ReplyComment>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<PagedList<ReplyComment>> GetAllWithPaging(PageRequestBody requestBody)
        {
            throw new NotImplementedException();
        }

        public async Task<ReplyComment> GetByIdAsync(Guid id)
        {
            return await _context.ReplyComments.Where(x => x.Id == id).SingleOrDefaultAsync();
        }

        public async Task<List<ReplyCommentModel>> GetReplyCommentsByParentId(Guid parentId)
        {
            var replys = await _context.ReplyComments.Where(x => x.ParentId == parentId).ToListAsync();
            var result = replys.MapTo<List<ReplyCommentModel>>();
            return result;
        }

        public async Task<bool> Update(ReplyComment entity, bool saveChange = true)
        {
            _context.ReplyComments.Update(entity);
            bool result = true;
            if (saveChange)
            {
                result = await _context.SaveChangesAsync() > 0;
            }
            return await Task.FromResult(result);
        }

        public async Task<bool> UpdateReplyComment(Guid replyId, string content)
        {
            var entity = await GetByIdAsync(replyId);
            if (entity == null)
            {
                return false;
            }
            if (string.IsNullOrEmpty(content))
            {
                return false;
            }
            entity.Content = content;
            entity.UpdatedDate = DateTime.Now;
            bool result = await Update(entity);
            return result;
        }
        public async Task<List<ReplyComment>> GetAllRepliesByParentIds(List<Guid> parentIds)
        {
            var result = await _context.ReplyComments.Where(x => parentIds.Contains(x.ParentId)).ToListAsync();
            return result;
        }
    }
}
