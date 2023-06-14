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

using ElectronicMedia.Core.Repository.DataContext;
using ElectronicMedia.Core.Repository.Entity;
using ElectronicMedia.Core.Repository.Models;
using ElectronicMedia.Core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicMedia.Core.Services.Service
{
    public class PostStatisticService : IPostStatisticService
    {
        private readonly ElectronicMediaDbContext _context;
        public PostStatisticService(ElectronicMediaDbContext context)
        {
            _context = context;
        }
        public async Task<PostStatisticModel> GetToltalPostInAnDepartment(Guid departmentId)
        {
            PostStatisticModel result = new PostStatisticModel()
            {
                NumberPost = 0,
                Month = DateTime.Now.Month,
            };
            int count = 0;
            var users = await _context.Users.Where(x => x.DepartmentId == departmentId).Include(x => x.Posts).ToListAsync();
            if (users != null && users.Count > 0)
            {
                foreach (var item in users)
                {
                    if (item.Posts != null && item.Posts.Any())
                    {
                        int countPost = item.Posts.Where(x => x.Status == PostStatusModel.Published
                                                            && x.PublishedDate.Value.Month == result.Month)
                                                            .ToList().Count();
                        count += countPost;
                    }
                }
            }
            result.NumberPost = count;
            return result;
        }

        public async Task<PostStatisticModel> GetTotalPostForEachWriter(Guid userId)
        {
            var writer = await _context.Users.Where(x => x.Id == userId).Include(x => x.Posts).FirstOrDefaultAsync();
            PostStatisticModel result = new PostStatisticModel()
            {
                NumberPost = 0,
                Month = DateTime.Now.Month,
            };
            if (writer != null && writer.Posts != null && writer.Posts.Any())
            {
                result.NumberPost = writer.Posts.Where(x => x.Status == PostStatusModel.Published
                && x.PublishedDate.Value.Month == result.Month)
                    .ToList().Count();
            }
            return result;
        }
    }
}
