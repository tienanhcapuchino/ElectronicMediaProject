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
            base.OnModelCreating(modelBuilder);
        }
        #region entity
        public DbSet<User> Users { get; set; }
        #endregion
    }
}
