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

using DocumentFormat.OpenXml.Spreadsheet;
using ElectronicMedia.Core.Automaper;
using ElectronicMedia.Core.Common.Extension;
using ElectronicMedia.Core.Repository.DataContext;
using ElectronicMedia.Core.Repository.Entity;
using ElectronicMedia.Core.Repository.Models;
using ElectronicMedia.Core.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
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
        public UserService(ElectronicMediaDbContext context, IOptionsMonitor<AppSetting> optionsMonitor,
           UserManager<UserIdentity> userManager, RoleManager<IdentityRole> roleManager,
            SignInManager<UserIdentity> signInManager)
        {
            _context = context;
            _appSettings = optionsMonitor.CurrentValue;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
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
            //if (user.Image == null)
            //{
            //    user.Image = CommonService.InitAvatarUser();
            //    await Update(user);
            //}
            var profile = user.MapTo<UserProfileModel>();
            //if (profile == null) throw new Exception("cannot map profile from user");
            return await Task.FromResult(profile);
        }
        public async Task<PagedList<UserIdentity>> GetAllWithPaging(PageRequestBody requestBody)
        {
            var user = await _userManager.Users.ToListAsync();
            return PagedList<UserIdentity>.ToPagedList(user, requestBody.Page, requestBody.Top);
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
        public async Task<bool> UpdateUserProfile(Guid userId, UserProfileModel profile)
        {
            var user = await GetByIdAsync(userId);
            if (user == null) throw new Exception($"Cannot find user with userId: {userId}");
            if (!profile.Username.Equals(user.UserName))
            {
                throw new Exception("Cannot change username");
            }
            user.FullName = profile.FullName;
            user.Email = profile.Email;
            user.PhoneNumber = profile.PhoneNumber;
            user.Dob = profile.Dob;
            user.Gender = profile.Gender;
          /*  if (string.IsNullOrEmpty(profile.Image))
            {
                user.Image = Convert.FromBase64String(profile.Image);
            }*/
            bool result = await Update(user);
            return await Task.FromResult(result);
        }
        #region private method
 
        public Task<IEnumerable<UserIdentity>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<UserIdentity>> GetUsersByIds(List<Guid> userIds)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
