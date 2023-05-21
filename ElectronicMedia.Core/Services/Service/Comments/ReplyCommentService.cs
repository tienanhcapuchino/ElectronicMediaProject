using ElectronicMedia.Core.Repository.DataContext;
using ElectronicMedia.Core.Repository.Entity;
using ElectronicMedia.Core.Services.Interfaces;
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

        public Task<bool> CreateReplyComment(ReplyComment replyComment)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(Guid id, bool saveChange = true)
        {
            throw new NotImplementedException();
        }

        public Task<List<ReplyComment>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ReplyComment> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ReplyComment>> GetReplyCommentsByParentId(Guid parentId)
        {
            throw new NotImplementedException();
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
    }
}
