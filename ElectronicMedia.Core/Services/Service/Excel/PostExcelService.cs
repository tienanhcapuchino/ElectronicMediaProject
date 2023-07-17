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
using ElectronicMedia.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicMedia.Core.Services.Service
{
    public class PostExcelService : IExcelService<Post>
    {
        private const string SHEET_NAME = "Posts_Export";
        public DataTable ExportToExcel(List<Post> values)
        {
            DataTable dt = new DataTable(SHEET_NAME);
            dt.Columns.AddRange(new DataColumn[11]
            {
                new DataColumn("No"),
                new DataColumn("Title"),
                new DataColumn("Author"),
                new DataColumn("CreatedDate"),
                new DataColumn("PublishedDate"),
                new DataColumn("Category"),
                new DataColumn("SubCategory"),
                new DataColumn("Rate"),
                new DataColumn("Like"),
                new DataColumn("Dislike"),
                new DataColumn("Status")
            });
            int i = 1;
            foreach (Post post in values)
            {
                dt.Rows.Add(
                    i,
                    post.Title,
                    post.User.FullName,
                    post.CreatedDate,
                    post.PublishedDate,
                    post.Category.Name,
                    post.SubCategory != null ? post.SubCategory.Name : "None",
                    post.Rate != null ? post.Rate : "None",
                    post.Like,
                    post.Dislike,
                    post.Status
                    );
                i++;
            }
            return dt;
        }
    }
}
