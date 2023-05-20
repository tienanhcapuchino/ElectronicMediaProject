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
    public class PostDetailService : IPostDetailService
    {
        private readonly ElectronicMediaDbContext _dbContext;
        public PostDetailService(ElectronicMediaDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> Add(PostDetail entity)
        {
            await _dbContext.AddAsync(entity);
            bool result = await _dbContext.SaveChangesAsync() > 0;
            return result;
        }

        public async Task<bool> CreatePostDetail(PostDetailModel model)
        {
            if (model == null || model.AuthorId == null || model.PostId == null) return false;
            var entity = model.MapTo<PostDetail>();
            bool result = await Add(entity);
            return result;
        }

        public Task<bool> Delete(Guid id, bool saveChange = true)
        {
            throw new NotImplementedException();
        }

        public async Task<PostDetail> FindByUserId(Guid userId, Guid postId)
        {
            return await _dbContext.PostDetails.SingleOrDefaultAsync(x => x.PostId == postId && x.UserId == userId);
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
