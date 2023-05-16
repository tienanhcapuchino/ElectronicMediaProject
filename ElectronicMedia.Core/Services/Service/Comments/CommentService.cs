using ElectronicMedia.Core.Repository.DataContext;
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
    public class CommentService : ICommentService
    {
        private readonly ElectronicMediaDbContext _context;
        public CommentService(ElectronicMediaDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Add(Comment entity)
        {
            await _context.AddAsync(entity);
            bool result = await _context.SaveChangesAsync() > 0;
            return result;
        }

        public Task<bool> CreateComment(CommentModel comment)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Comment>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<CommentModel>> GetAllCommentsByPost(Guid postId)
        {
            throw new NotImplementedException();
        }

        public Task<Comment> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Guid id, Comment entity)
        {
            throw new NotImplementedException();
        }
    }
}
