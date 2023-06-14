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

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicMedia.Core.Repository.Entity
{
    public class Post
    {
        public Guid Id { get; set; }
        public virtual User User { get; set; }
        [Column("AuthorId")]
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? PublishedDate { get; set; } = null;
        public DateTime? UpdatedDate { get; set; } = null;
        public virtual PostCategory Category { get; set; }
        public Guid CategoryId { get; set; }
        [Column("SubCategoryId")]
        public Guid? SubCategoryId { get; set; } = Guid.Empty;
        public virtual PostCategory SubCategory { get; set; }
        public double? Rate { get; set; }
        public int Like { get; set; } = 0;
        public int Dislike { get; set; } = 0;
        [Column(TypeName ="image")]
        public byte[]? Image { get; set; }
        public PostStatusModel Status { get; set; }
    }
    public enum PostStatusModel : byte
    {
        Pending = 0,
        Approved = 1,
        Published = 2,
        Denied = 3,
    }
}
