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
 * Copy right 2023 - PRN231 - SU23 - Group 10. All Rights Reserved
 * 
 * Unpublished - All rights reserved under the copyright laws 
 * of the Government of Viet Nam
*********************************************************************/

using ElectronicMedia.Core.Automaper;
using ElectronicMedia.Core.Common.Extension;
using ElectronicMedia.Core.Repository.DataContext;
using ElectronicMedia.Core.Repository.Entity;
using ElectronicMedia.Core.Repository.Models;
using ElectronicMedia.Core.Services.Interfaces;
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
        public UserService(ElectronicMediaDbContext context, IOptionsMonitor<AppSetting> optionsMonitor)
        {
            _context = context;
            _appSettings = optionsMonitor.CurrentValue;
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Id == id);
            return await Task.FromResult(user);
        }
        public async Task<UserProfileModel> GetProfileUser(Guid userId)
        {
            var user = await GetByIdAsync(userId);
            if (user == null) throw new Exception($"Cannot find user with id: {userId}");
            if (user.Image == null)
            {
                user.Image = CommonService.InitAvatarUser();
                await Update(user);
            }
            var profile = user.MapTo<UserProfileModel>();
            if (profile == null) throw new Exception("cannot map profile from user");
            return await Task.FromResult(profile);
        }
        public async Task<PagedList<User>> GetAllWithPaging(PageRequestBody requestBody)
        {
            var user = await _context.Users.Skip(requestBody.Skip).Take(requestBody.Top).ToListAsync();
            return PagedList<User>.ToPagedList(user, requestBody.Page, requestBody.Top);
        }

        public Task<bool> Delete(Guid id, bool saveChange = true)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Update(User entity, bool saveChange = true)
        {
            _context.Users.Update(entity);
            bool result = true;
            if (saveChange)
            {
                result = await _context.SaveChangesAsync() > 0;
            }
            return await Task.FromResult(result);
        }

        public async Task<bool> Add(User entity, bool saveChange = true)
        {
            await _context.AddAsync(entity);
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
            if (!profile.Username.Equals(user.Username))
            {
                throw new Exception("Cannot change username");
            }
            if (!await IsDuplicateEmail(userId, profile.Email)
                || !await IsDuplicatePhone(userId, profile.PhoneNumber))
            {

                return false;
            }
            user.FullName = profile.FullName;
            user.Email = profile.Email;
            user.PhoneNumber = profile.PhoneNumber;
            user.Dob = profile.Dob;
            user.Gender = profile.Gender;
            if (string.IsNullOrEmpty(profile.Image))
            {
                user.Image = Convert.FromBase64String(profile.Image);
            }
            bool result = await Update(user);
            return await Task.FromResult(result);
        }
        public async Task<bool> IsDuplicateEmail(Guid userId, string email)
        {
            string regexEmail = @"^[^\s@]+@[^\s@]+\.[^\s@]+$";
            if (!Regex.IsMatch(email, regexEmail)) return false;
            var emails = await _context.Users.Where(x => x.Id != userId).Select(x => x.Email).ToListAsync();
            if (emails != null && emails.Any() && emails.Contains(email))
            {
                return false;
            }
            return true;
        }
        public async Task<bool> IsDuplicatePhone(Guid userId, string phoneNumber)
        {
            string regexPhone = @"^0\d{9}$";
            if (!Regex.IsMatch(phoneNumber, regexPhone)) return false;
            var phones = await _context.Users.Where(x => x.Id != userId).Select(x => x.PhoneNumber).ToListAsync();
            if (phones != null && phones.Any() && phones.Contains(phoneNumber)) return false;
            return true;
        }
        public async Task<List<User>> GetUsersByIds(List<Guid> userIds)
        {
            var result = await _context.Users.Where(x => userIds.Contains(x.Id)).ToListAsync();
            return result;
        }
        #region private method
        private DateTime ConverUnixTimeToDateTime(long utcExpireDate)
        {
            var dateTImeInterval = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTImeInterval.AddSeconds(utcExpireDate).ToUniversalTime();
            return dateTImeInterval;
        }
        private string GenerateRefreshToken()
        {
            var random = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(random);
                return Convert.ToBase64String(random);
            }
        }

        private APIResponeModel IsValidUserRegister(UserRegisterModel model)
        {
            APIResponeModel isValid = new APIResponeModel()
            {
                Code = 200,
                Message = "OK",
                IsSucceed = true,
            };
            string regexPassword = @"^(?=.*[a-zA-Z])(?=.*\d).{8,}$";
            string regexEmail = @"^[^\s@]+@[^\s@]+\.[^\s@]+$";
            string regexPhone = @"^0\d{9}$";
            if (model == null)
            {
                return new APIResponeModel()
                {
                    Code = 400,
                    Message = "cannot leave model is empty!",
                    IsSucceed = false,
                };
            }

            var userEntity = _context.Users.SingleOrDefault(x => x.Username == model.Username
                                                            || x.Email == model.Email
                                                            || x.PhoneNumber == model.PhoneNumber);
            if (userEntity != null)
            {
                return new APIResponeModel()
                {
                    Code = 400,
                    Message = "Account is already exitsed",
                    IsSucceed = false,
                };
            }

            if (!model.Password.Equals(model.Repassword))
            {
                return new APIResponeModel()
                {
                    Code = 404,
                    Message = "Password is not match with repassword!",
                    IsSucceed = false,
                };
            }
            if (!Regex.IsMatch(model.Password, regexPassword))
            {
                return new APIResponeModel()
                {
                    Code = 400,
                    Message = "Password must contain both number and character, at least 8 character!",
                    IsSucceed = false
                };
            }
            if (!Regex.IsMatch(model.Email, regexEmail))
            {
                return new APIResponeModel() { Code = 400, Message = "Email is invalid!", IsSucceed = false };
            }
            if (!Regex.IsMatch(model.PhoneNumber, regexPhone))
            {
                return new APIResponeModel() { Code = 400, Message = "Phone number must is 10 number and start with 0!", IsSucceed = false };
            }
            return isValid;
        }

        public Task<IEnumerable<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
