using ElectronicMedia.Core.Repository.Entity;
using ElectronicMedia.Core.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicMedia.Core.Services.Interfaces
{
    public interface IPostCategoryService : ICoreRepository<PostCategory>
    {
        Task<bool> CreatePostCate(PostCategoryModel model);
        Task<bool> UpdatePostCate(Guid id, PostCategoryModel model);
        Task<List<PostCategory>> GetPostCateParent();
    }
}
