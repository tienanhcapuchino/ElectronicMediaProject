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

using DocumentFormat.OpenXml.InkML;
using ElectronicMedia.Core.Automaper;
using ElectronicMedia.Core.Common;
using ElectronicMedia.Core.Common.Extension;
using ElectronicMedia.Core.Repository.DataContext;
using ElectronicMedia.Core.Repository.Entity;
using ElectronicMedia.Core.Repository.Models;
using ElectronicMedia.Core.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicMedia.Core.Services.Service
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUserService _userService;
        private readonly ElectronicMediaDbContext _dbContext;
        private readonly UserManager<UserIdentity> _userManager;
        public DepartmentService(IUserService userService, 
            ElectronicMediaDbContext dbContext,
            UserManager<UserIdentity> userManager)
        {
            _userService = userService;
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<bool> Add(Department entity, bool saveChange = true)
        {
            await _dbContext.Departments.AddAsync(entity);
            bool result = true;
            if (saveChange)
            {
                result = await _dbContext.SaveChangesAsync() > 0;
            }
            return await Task.FromResult(result);
        }

        public async Task<APIResponeModel> AddDepartment(DepartmentModel department)
        {
            APIResponeModel result = new APIResponeModel()
            {
                Code = 200,
                Message = "Add success",
                Data = department,
                IsSucceed = true
            };
            if (department == null || string.IsNullOrEmpty(department.Name))
            {
                return new APIResponeModel()
                {
                    Code = 400,
                    Message = "Cannot leave name is empty",
                    Data = department,
                    IsSucceed = false
                };
            }
            if (await IsDuplicateName(department.Name))
            {
                return new APIResponeModel()
                {
                    Code = 400,
                    Message = "Department name is already exist!",
                    Data = department,
                    IsSucceed = false
                };
            }
            var entity = department.MapTo<Department>();
            await Add(entity);
            return result;
        }

        public async Task<bool> AssignMemberToDepartment(Guid depId, Guid memberId)
        {
            var member = await _userService.GetByIdAsync(memberId);
            member.DepartmentId = depId;
            bool result = await _userService.Update(member);
            return await Task.FromResult(result);
        }

        public async Task<bool> Delete(Guid id, bool saveChange = true)
        {
            var users = await _userManager.Users.Where(x => x.DepartmentId == id).ToListAsync();
            if (users != null && users.Any())
            {
                foreach (var item in users)
                {
                    item.DepartmentId = null;
                    await _userService.Update(item);
                }
            }
            bool result = true;
            var dep = await GetByIdAsync(id);
            _dbContext.Departments.Remove(dep);
            if (saveChange) result = await _dbContext.SaveChangesAsync() > 0;
            return result;
        }

        public Task<IEnumerable<Department>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<PagedList<Department>> GetAllWithPaging(PageRequestBody requestBody)
        {
            var departments = await _dbContext.Departments.Skip((requestBody.Page - 1) * requestBody.Top)
                    .Take(requestBody.Top).ToListAsync();
            var countItem = await CommonService.GetTotalCount<Department>(_dbContext);
            var result = QueryData<Department>.QueryForModel(requestBody, departments).ToList();
            return PagedList<Department>.ToPagedList(result, requestBody.Page, requestBody.Top, countItem);
        }

        public async Task<PagedList<DepartmentModel>> GetAllWithPagingModel(PageRequestBody requestBody)
        {
            var departments = await _dbContext.Departments.Skip((requestBody.Page - 1) * requestBody.Top)
                    .Take(requestBody.Top).ToListAsync();
            var countItem = await CommonService.GetTotalCount<Department>(_dbContext);
            var tempResult = departments.MapToList<DepartmentModel>();
            var result = QueryData<DepartmentModel>.QueryForModel(requestBody, tempResult).ToList();
            return PagedList<DepartmentModel>.ToPagedList(result, requestBody.Page, requestBody.Top, countItem);
        }

        public async Task<Department> GetByIdAsync(Guid id)
        {
            var result = await _dbContext.Departments.Include(x => x.Members).FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }

        public async Task<List<MemberModel>> GetLeadersToAssign()
        {
            var leaders = (await _userManager.GetUsersInRoleAsync(UserRole.Leader)).ToList();
            var leaderResult = leaders.Where(x => x.DepartmentId == null).ToList();
            var result = leaderResult.MapToList<MemberModel>();
            return result;
        }

        public async Task<List<MemberModel>> GetMembersToAssign()
        {
            var leaders = (await _userManager.GetUsersInRoleAsync(UserRole.Writer)).ToList();
            var leaderResult = leaders.Where(x => x.DepartmentId == null).ToList();
            var result = leaderResult.MapToList<MemberModel>();
            return result;
        }

        public async Task<APIResponeModel> KickMember(Guid departmentId, string userId)
        {
            var department = await GetByIdAsync(departmentId);
            var members = department.Members.ToList();
            if (members != null && members.Any())
            {
                var user = members.Where(x => x.Id.Equals(userId)).FirstOrDefault();
                if (user != null)
                {
                    user.DepartmentId = null;
                    await _userManager.UpdateAsync(user);
                    return new APIResponeModel()
                    {
                        Code = 200,
                        IsSucceed = true,
                        Message = "Kick successfully",
                        Data = userId
                    };
                }
            }
            return new APIResponeModel()
            {
                Code = 400,
                IsSucceed = false,
                Message = "Kick failed",
                Data = userId + ";" + departmentId
            };
        }

        public async Task<bool> Update(Department entity, bool saveChange = true)
        {
            _dbContext.Departments.Update(entity);
            bool result = true;
            if (saveChange)
            {
                result = await _dbContext.SaveChangesAsync() > 0;
            }
            return await Task.FromResult(result);
        }

        public async Task<APIResponeModel> UpdateDepartment(DepartmentModel department)
        {

            APIResponeModel result = new APIResponeModel()
            {
                Code = 200,
                Message = "update success",
                Data = department,
                IsSucceed = true
            };
            if (department == null || string.IsNullOrEmpty(department.Name))
            {
                return new APIResponeModel()
                {
                    Code = 400,
                    Message = "Cannot leave name is empty",
                    Data = department,
                    IsSucceed = false
                };
            }
            if (await IsDuplicateNameUdate(department.Id, department.Name))
            {
                return new APIResponeModel()
                {
                    Code = 400,
                    Message = "Department name is already exist!",
                    Data = department,
                    IsSucceed = false
                };
            }
            var entity = department.MapTo<Department>();
            await Update(entity, false);
            await _dbContext.SaveChangesAsync();
            return result;
        }

        public async Task<DepartmentViewDetail> ViewDetailDepartment(Guid departmentId)
        {
            var dep = await GetByIdAsync(departmentId);
            var result = dep.MapTo<DepartmentViewDetail>();
            await _userService.SetRoleForMembersInDepartment(result.Members, dep.Members.ToList());
            return result;
        }

        #region private method
        private async Task<bool> IsDuplicateName(string name)
        {
            var department = await _dbContext.Departments.Where(x => x.Name.Equals(name)).FirstOrDefaultAsync();
            if (department != null)
            {
                return true;
            }
            return false;
        }
        private async Task<bool> IsDuplicateNameUdate(Guid depId, string name)
        {
            var department = await _dbContext.Departments.Where(x => x.Id != depId && x.Name.Equals(name)).FirstOrDefaultAsync();
            if (department != null)
            {
                return true;
            }
            return false;
        }
        #endregion
    }
}
