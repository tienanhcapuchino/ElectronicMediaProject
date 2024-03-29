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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicMedia.Core.Services.Interfaces
{
    public interface IDepartmentService : ICoreRepository<Department>
    {
        Task<bool> AssignMemberToDepartment(Guid depId, Guid memberId);
        Task<APIResponeModel> AddDepartment(DepartmentModel department);
        Task<APIResponeModel> UpdateDepartment(DepartmentModel department);
        Task<APIResponeModel> KickMember(Guid departmentId, string userId);
        Task<PagedList<DepartmentModel>> GetAllWithPagingModel(PageRequestBody requestBody);
        Task<DepartmentViewDetail> ViewDetailDepartment(Guid departmentId);
        Task<List<MemberModel>> GetMembersToAssign();
        Task<List<MemberModel>> GetLeadersToAssign();
    }
}
