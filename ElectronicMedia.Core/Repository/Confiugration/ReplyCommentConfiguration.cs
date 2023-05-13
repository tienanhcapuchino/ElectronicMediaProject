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
    public class ReplyCommentConfiguration : IEntityTypeConfiguration<ReplyComment>
    {
        public void Configure(EntityTypeBuilder<ReplyComment> builder)
        {
            builder.ToTable("replyComment");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Content).IsRequired();
            builder.HasOne(x => x.Comment).WithMany(x => x.ReplyComments)
                .HasForeignKey(x => x.ParentId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.User).WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
