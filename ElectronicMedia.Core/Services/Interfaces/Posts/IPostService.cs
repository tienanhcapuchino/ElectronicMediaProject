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
        Task<bool> CreatePostCategory(string categoryName);
        Task<bool> EditCategory(Guid cateId);
        Task<bool> CreatePost(PostModel post);
        Task<bool> UpdateLikeAndDislike(bool liked);
        Task<bool> CreateSubCategories(List<PostCategoryModel> subCategories);
    }
}
