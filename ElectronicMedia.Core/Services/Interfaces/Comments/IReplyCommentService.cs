using ElectronicMedia.Core.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicMedia.Core.Services.Interfaces
{
    public interface IReplyCommentService : ICoreRepository<ReplyComment>
    {
        Task<List<ReplyComment>> GetReplyCommentsByParentId(Guid parentId);
        Task<bool> CreateReplyComment(ReplyComment replyComment);
    }
}
