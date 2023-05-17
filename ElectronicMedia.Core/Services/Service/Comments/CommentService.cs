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

        public async Task<bool> CreateComment(Guid userId, Guid postId, string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                return false;
            }
            var entity = new Comment()
            {
                UserId = userId,
                PostId = postId,
                CreatedDate = DateTime.UtcNow,
                Content = content
            };
            bool result = await Add(entity);
            return result;
        }

        public Task<bool> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Comment>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<CommentModel>> GetAllCommentsByPost(Guid postId)
        {
            var comments = await _context.Comments.Where(c => c.PostId == postId).OrderBy(c => c.CreatedDate).ToListAsync();
            var result = comments.MapTo<List<CommentModel>>();
            return result;
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
