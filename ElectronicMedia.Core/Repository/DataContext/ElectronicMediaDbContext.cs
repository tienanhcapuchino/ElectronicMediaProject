using ElectronicMedia.Core.Repository.Confiugration;
using ElectronicMedia.Core.Repository.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicMedia.Core.Repository.DataContext
{
    public class ElectronicMediaDbContext : DbContext
    {
        public ElectronicMediaDbContext(DbContextOptions<ElectronicMediaDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new PostCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new PostDetailConfiguration());
            modelBuilder.ApplyConfiguration(new CommentConfiguration());
            modelBuilder.ApplyConfiguration(new ReplyCommentConfiguration());
            modelBuilder.ApplyConfiguration(new PostConfiguration());
        }
        #region entity
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostCategory> PostCategories { get; set; }
        public DbSet<PostDetail> PostDetails { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<ReplyComment> ReplyComments { get; set; }
        #endregion
    }
}
