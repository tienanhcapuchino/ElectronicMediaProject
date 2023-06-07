using ElectronicMedia.Core.Repository.Entity;
using ElectronicMedia.Core.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicMedia.Core.Services.Interfaces
{
    public interface IPostService : ICoreRepository<Post>
    {
        Task<bool> CreatePost(PostModel post);
        Task<bool> UpdateLikeAndDislike(Guid postId, bool liked);
        Task<bool> VotePost(PostDetailModel postDetail);
        Task<PostViewModel> GetById(Guid id);
    }
}
