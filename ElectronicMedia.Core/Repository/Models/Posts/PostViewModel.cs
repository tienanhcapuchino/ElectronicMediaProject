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
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicMedia.Core.Repository.Models
{
    public class PostViewModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? PublishedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid CategoryId { get; set; }
        public Guid? SubCategoryId { get; set; }
        public double? Rate { get; set; }
        public int Like { get; set; }
        public int Dislike { get; set; }
        public string Image { get; set; }
        public PostStatusModel Status { get; set; }
    }
    public class PostView
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string AuthorName { get; set; }
        public Guid AuthorId { get; set; }
        public string ImageUser { get; set; }
    }
}
