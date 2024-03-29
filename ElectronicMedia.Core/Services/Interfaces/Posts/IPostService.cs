﻿/*********************************************************************
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
using ElectronicMedia.Core.Repository.Models;
using ElectronicMedia.Core.RequestBody;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicMedia.Core.Services.Interfaces
{
    public interface IPostService : ICoreRepository<Post>
    {
        Task<bool> CreatePost(PostModel post);
        Task<bool> UpdatePost(PostViewModel post);
        Task<bool> UpdateLikeAndDislike(Guid postId, bool liked);
        Task<bool> VotePost(PostDetailModel postDetail);
        Task<PostViewModel> GetById(Guid id);
        Task<bool> DeletePost(Guid postId,string message = "No reasons.");
        Task<DataTable> ExportPosts();
        Task<IEnumerable<PostView>> GetNewPost(int take);
        Task<PagedList<PostView>> GetPostByCateId(PostRequestBody requestBody);
        Task<PagedList<PostViewModel>> GetAllWithPaging(PageRequestBody requestBody); 
    }
}
