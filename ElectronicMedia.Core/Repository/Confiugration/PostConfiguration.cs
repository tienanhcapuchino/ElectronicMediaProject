using ElectronicMedia.Core.Repository.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicMedia.Core.Repository.Confiugration
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable("post");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.PublishedDate).IsRequired().HasDefaultValue(DateTime.UtcNow);
            builder.Property(t => t.CreatedDate).IsRequired().HasDefaultValue(DateTime.UtcNow);
            builder.Property(t => t.UpdatedDate).IsRequired().HasDefaultValue(DateTime.UtcNow);
            builder.Property(x => x.Rate);
            builder.Property(x => x.Status).HasDefaultValue(0);
            builder.Property(t => t.Content).IsRequired();
            builder.Property(t => t.Title).IsRequired();
            builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.Category).WithMany().HasForeignKey(x => x.CategoryId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
