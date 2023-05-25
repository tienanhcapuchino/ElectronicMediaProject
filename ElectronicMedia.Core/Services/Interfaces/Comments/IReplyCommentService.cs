using ElectronicMedia.Core.Repository.Entity;
using ElectronicMedia.Core.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicMedia.Core.Services.Interfaces
{
    public interface IReplyCommentService : ICoreRepository<ReplyComment>
    {
        Task<List<ReplyCommentModel>> GetReplyCommentsByParentId(Guid parentId);
        Task<bool> CreateReplyComment(ReplyCommentModel replyComment);
        Task<bool> UpdateReplyComment(Guid replyId, string content);
    }
}
