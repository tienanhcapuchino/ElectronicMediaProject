using ElectronicMedia.Core.Repository.Entity;
using ElectronicMedia.Core.Repository.Models;
using ElectronicMedia.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicMedia.Core.Services.Service
{
    public class PostDetailService : IPostDetailService
    {
        public Task<bool> Add(PostDetail entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreatePostDetail(PostDetailModel model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(Guid id, bool saveChange = true)
        {
            throw new NotImplementedException();
        }

        public Task<List<PostDetail>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<PostDetail> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Guid id, PostDetail entity)
        {
            throw new NotImplementedException();
        }
    }
}
