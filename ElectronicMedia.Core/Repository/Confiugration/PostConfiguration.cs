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
            builder.Property(x => x.Image).IsRequired();
            builder.Property(x => x.Status).HasDefaultValue(PostStatusModel.Pending);
            builder.Property(t => t.Content).IsRequired();
            builder.Property(t => t.Title).IsRequired();
            builder.Property(t => t.Like).HasDefaultValue(0);
            builder.Property(t => t.Dislike).HasDefaultValue(0);
            builder.HasOne(x => x.User).WithMany(x => x.Posts).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
