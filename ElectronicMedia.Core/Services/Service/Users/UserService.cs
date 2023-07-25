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

using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Spreadsheet;
using ElectronicMedia.Core.Automaper;
using ElectronicMedia.Core.Common.Extension;
using ElectronicMedia.Core.Repository.DataContext;
using ElectronicMedia.Core.Repository.Entity;
using ElectronicMedia.Core.Repository.Models;
using ElectronicMedia.Core.Services.Interfaces;
using ElectronicMedia.Core.Services.Interfaces.Email;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ElectronicMedia.Core.Services.Service
{
    public partial class UserService : IUserService
    {
        private readonly ElectronicMediaDbContext _context;
        private readonly AppSetting _appSettings;
        private readonly UserManager<UserIdentity> _userManager;
        private readonly SignInManager<UserIdentity> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailService _emailService;
        private readonly IExcelService<UserIdentity> _excelService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IFileStorageService _fileStorageService;
        public UserService(ElectronicMediaDbContext context, IOptionsMonitor<AppSetting> optionsMonitor,
           UserManager<UserIdentity> userManager, RoleManager<IdentityRole> roleManager,
            SignInManager<UserIdentity> signInManager,
            IEmailService emailService,
            IExcelService<UserIdentity> excelService,
            IHttpContextAccessor contextAccessor,
            IFileStorageService fileStorageService)
        {
            _context = context;
            _appSettings = optionsMonitor.CurrentValue;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _excelService = excelService;
            _contextAccessor = contextAccessor;
            _fileStorageService = fileStorageService;
        }

        public async Task<UserIdentity> GetByIdAsync(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            return await Task.FromResult(user);
        }
        public async Task<UserProfileModel> GetProfileUser(Guid userId)
        {
            var user = await GetByIdAsync(userId);
            if (user == null) throw new Exception($"Cannot find user with id: {userId}");
            var profile = user.MapTo<UserProfileModel>();

            return await Task.FromResult(profile);
        }
        public async Task<PagedList<UsersModel>> GetAllWithPagingModels(PageRequestBody requestBody)
        {
            var currentUser = GetCurrentUser();
            var userId = currentUser.FindFirst("UserId")?.Value;
            var user = await _userManager.Users.Where(x => !x.Id.Equals(userId)).Include(x => x.Department).ToListAsync();
            var resultTemp = user.MapToList<UsersModel>();
            foreach (var item in user)
            {
                foreach (var model in resultTemp)
                {
                    if (item.Id.Equals(model.UserId))
                    {
                        var roles = await _userManager.GetRolesAsync(item);
                        if (roles != null && roles.Any())
                            model.RoleName = roles.First();
                    }
                }
            }
            var result = QueryData<UsersModel>.QueryForModel(requestBody, resultTemp).ToList();
            var countItem = await CommonService.GetTotalCount<UserIdentity>(_context);
            return PagedList<UsersModel>.ToPagedList(result, requestBody.Page, requestBody.Top, countItem);
        }
        public async Task<PagedList<UserIdentity>> GetAllWithPaging(PageRequestBody requestBody)
        {
            var user = await _userManager.Users.Skip((requestBody.Page - 1) * requestBody.Top)
                    .Take(requestBody.Top).ToListAsync();
            var result = QueryData<UserIdentity>.QueryForModel(requestBody, user).ToList();
            var countItem = await CommonService.GetTotalCount<UserIdentity>(_context);
            return PagedList<UserIdentity>.ToPagedList(result, requestBody.Page, requestBody.Top, countItem);
        }
        public Task<bool> Delete(Guid id, bool saveChange = true)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Update(UserIdentity entity, bool saveChange = true)
        {
            await _userManager.UpdateAsync(entity);
            bool result = true;
            if (saveChange)
            {
                result = await _context.SaveChangesAsync() > 0;
            }
            return await Task.FromResult(result);
        }

        public async Task<bool> Add(UserIdentity entity, bool saveChange = true)
        {
            await _userManager.CreateAsync(entity);
            bool result = true;
            if (saveChange)
            {
                result = await _context.SaveChangesAsync() > 0;
            }
            return result;
        }
        public APIResponeModel UpdateUserProfile(UserProfileUpdateModel profile)
        {
            APIResponeModel result = new APIResponeModel()
            {
                Data = profile,
                Code = 200,
                IsSucceed = true,
                Message = "update successfully"
            };
            //var user = GetByIdAsync(Guid.Parse(profile.Id)).GetAwaiter().GetResult();
            //if (user == null)
            //{
            //    return new APIResponeModel()
            //    {
            //        Data = profile,
            //        Code = 400,
            //        IsSucceed = false,
            //        Message = "user doesn't exist"
            //    };
            //}
            //if (!profile.Username.Equals(user.UserName))
            //{
            //    return new APIResponeModel()
            //    {
            //        Data = profile,
            //        Code = 400,
            //        IsSucceed = false,
            //        Message = "cannot change username"
            //    };
            //}
            var updateProfile = profile.MapTo<UserIdentity>();
            IFormFile fileDemo = ConvertObjectToIFormFile(profile.ImageFile);
            updateProfile.Image = _fileStorageService.SaveImageFile(fileDemo).GetAwaiter().GetResult();
            _context.Entry(updateProfile).State = EntityState.Detached;
            var resultUpdate = _userManager.UpdateAsync(updateProfile).GetAwaiter().GetResult();
            if (resultUpdate.Succeeded)
            {
                return result;
            }
            else
            {
                string errorMesage = "";
                foreach (var error in resultUpdate.Errors)
                {
                    errorMesage += error.Description;
                }
                return new APIResponeModel()
                {
                    Code = 400,
                    Message = errorMesage,
                    IsSucceed = false
                };
            }
        }

        public async Task<IEnumerable<UserIdentity>> GetAllAsync()
        {
            var result = await _userManager.Users.ToListAsync();
            foreach (var user in result)
            {
                var department = await _context.Departments.FirstOrDefaultAsync(x => x.Id == user.DepartmentId);
                if (department != null)
                {
                    user.Department = department;
                }
            }
            return result;
        }

        public Task<List<UserIdentity>> GetUsersByIds(List<Guid> userIds)
        {
            throw new NotImplementedException();
        }
        public async Task<DataTable> ExportUsers()
        {
            var users = (await GetAllAsync()).ToList();
            var result = _excelService.ExportToExcel(users);
            return result;
        }

        public async Task SetRoleForMembersInDepartment(List<MemberModel> members, List<UserIdentity> userEntitys)
        {
            foreach (var item in userEntitys)
            {
                foreach (var model in members)
                {
                    if (item.Id.Equals(model.MemberId))
                    {
                        var roles = await _userManager.GetRolesAsync(item);
                        if (roles != null && roles.Any())
                            model.RoleName = roles.First();
                    }
                }
            }
        }

        private IFormFile ConvertObjectToIFormFile(object imageFileObject)
        {
            JsonDocument jsonDocument = JsonDocument.Parse(imageFileObject.ToString());
            JsonElement root = jsonDocument.RootElement;

            // Extract the necessary properties
            string contentDisposition = root.GetProperty("ContentDisposition").GetString();
            string contentType = root.GetProperty("ContentType").GetString();
            string fileName = root.GetProperty("FileName").GetString();
            long length = root.GetProperty("Length").GetInt64();

            // Create a temporary MemoryStream to hold the file content
            IFormFile formFile = new FormFile(Stream.Null, 0, length, "userProfile.ImageFile", fileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = contentType
            };
            return formFile;
        }
    }
}
