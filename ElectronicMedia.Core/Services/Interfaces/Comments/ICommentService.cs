using ElectronicMedia.Core.Repository.Entity;
using ElectronicMedia.Core.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicMedia.Core.Services.Interfaces
{
    public interface ICommentService : ICoreRepository<Comment>
    {
        Task<List<CommentModel>> GetAllCommentsByPost(Guid postId);
        Task<bool> CreateComment(Guid userId, Guid postId, string content);
    }
}
