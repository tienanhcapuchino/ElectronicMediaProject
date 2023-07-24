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
    public class CommentService : ICommentService
    {
        private readonly ElectronicMediaDbContext _context;
        public CommentService(ElectronicMediaDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Add(Comment entity, bool saveChange = true)
        {
            await _context.AddAsync(entity);
            bool result = true;
            if (saveChange)
            {
                result = await _context.SaveChangesAsync() > 0;
            }
            return await Task.FromResult(result);
        }

        public async Task<bool> CreateComment(CommentAddModel model)
        {
            if (string.IsNullOrEmpty(model.Content))
            {
                return false;
            }
            var entity = new Comment()
            {
                UserId = model.UserId.ToString(),
                PostId = model.PostId,
                CreatedDate = DateTime.UtcNow,
                Content = model.Content
            };
            bool result = await Add(entity);
            return result;
        }

        public Task<bool> Delete(Guid id, bool saveChange = true)
        {
            throw new NotImplementedException();
        }

        public Task<PagedList<Comment>> GetAllAsync(PageRequestBody requestBody)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Comment>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<CommentDto> GetAllCommentsByPost(Guid postId)
        {
            var result = new CommentDto();
            var comments = await _context.Comments.Where(c => c.PostId == postId).Include(x => x.User).OrderBy(c => c.CreatedDate).ToListAsync();
            var countNumber = await _context.Comments.Where(c => c.PostId == postId).OrderBy(c => c.CreatedDate).CountAsync();
            var listComment = comments.MapToList<CommentModel>();
            if (listComment.Count() > 0)
            {
                listComment.ForEach(x =>
                {
                    var reply = _context.ReplyComments.Where(r => r.ParentId == x.Id).Include(y => y.User).ToList();
                    if (reply.Count() > 0)
                    {
                        x.ReplyComment.AddRange(reply.MapToList<ReplyCommentModel>());
                    }
                    countNumber += reply.Count();
                });
            }
            result.CountComment = countNumber;
            result.CommentModels = listComment;
            return result;
        }

        public Task<PagedList<Comment>> GetAllWithPaging(PageRequestBody requestBody)
        {
            throw new NotImplementedException();
        }

        public Task<Comment> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Update(Comment entity, bool saveChange = true)
        {
            _context.Comments.Update(entity);
            bool result = true;
            if (saveChange)
            {
                result = await _context.SaveChangesAsync() > 0;
            }
            return await Task.FromResult(result);
        }
    }
}
