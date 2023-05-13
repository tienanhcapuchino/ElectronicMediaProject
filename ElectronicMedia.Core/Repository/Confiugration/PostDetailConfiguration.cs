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
    public class PostDetailConfiguration : IEntityTypeConfiguration<PostDetail>
    {
        public void Configure(EntityTypeBuilder<PostDetail> builder)
        {
            builder.ToTable("postDetail");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Dislike);
            builder.Property(p => p.Like);
            builder.HasOne(p => p.Post).WithMany().HasForeignKey(x => x.PostId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(p => p.User).WithMany().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
