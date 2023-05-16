using ElectronicMedia.Core.Repository.Entity;
using ElectronicMedia.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicMedia.Core.Services.Service
{
    public class ReplyCommentService : IReplyCommentService
    {
        public Task<bool> Add(ReplyComment entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateReplyComment(ReplyComment replyComment)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(Guid id)
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

        public Task<bool> Update(Guid id, ReplyComment entity)
        {
            throw new NotImplementedException();
        }
    }
}
