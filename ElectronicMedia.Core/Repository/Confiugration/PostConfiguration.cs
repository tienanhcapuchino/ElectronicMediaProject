/*********************************************************************
 * 
 * PROPRIETARY and CONFIDENTIAL
 * 
 * This is licensed from, and is trade secret of:
 * 
 *          Group 10 - PRN231 - SU23
 *          FPT University, Education and Training zone
 *          Hoa Lac Hi-tech Park, Km29, Thang Long Highway
 *          Ha Noi, Viet Nam
 *          
 * Refer to your License Agreement for restrictions on use,
 * duplication, or disclosure
 * 
 * RESTRICTED RIGHTS LEGEND
 * 
 * Use, duplication or disclosure is the
 * subject to restriction in Articles 736 and 738 of the 
 * 2005 Civil Code, the Intellectual Property Law and Decree 
 * No. 85/2011/ND-CP amending and supplementing a number of 
 * articles of Decree 100/ND-CP/2006 of the Government of Viet Nam
 * 
 * 
 * Copy right 2023 © PRN231 - SU23 - Group 10 ®. All Rights Reserved
 * 
 * Unpublished - All rights reserved under the copyright laws 
 * of the Government of Viet Nam
*********************************************************************/

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
            builder.Property(x => x.Image);
            builder.Property(x => x.Status).HasDefaultValue(PostStatusModel.Pending);
            builder.Property(t => t.Content).IsRequired();
            builder.Property(t => t.Title).IsRequired();
            builder.Property(t => t.Like).HasDefaultValue(0);
            builder.Property(t => t.Dislike).HasDefaultValue(0);
            builder.HasOne(x => x.User).WithMany(x => x.Posts).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
