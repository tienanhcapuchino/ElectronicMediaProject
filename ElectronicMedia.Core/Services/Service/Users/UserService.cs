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
            var user = GetByIdAsync(userId);
            if (user == null) throw new Exception($"Cannot find user with id: {userId}");
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
