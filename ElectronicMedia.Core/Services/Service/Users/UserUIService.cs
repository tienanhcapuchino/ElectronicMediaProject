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
using ElectronicMedia.Core.Common;
using ElectronicMedia.Core.Common.Extension;
using ElectronicMedia.Core.Repository.Domains;
using ElectronicMedia.Core.Repository.Entity;
using ElectronicMedia.Core.Repository.Models;
using ElectronicMedia.Core.Repository.Models.Email;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicMedia.Core.Services.Service
{
    public partial class UserService
    {

        public async Task<string> GenerateToken(UserIdentity us)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var secretKeyBytes = Encoding.UTF8.GetBytes(_appSettings.SecretKey);
            var userIdentity = await _userManager.FindByIdAsync(us.Id.ToString());
            var roles = await _userManager.GetRolesAsync(userIdentity);
            string role = roles[0].ToString();
            var tokenDecription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]{
                new Claim(ClaimTypes.Name, us.UserName),
                new Claim(ClaimTypes.Role, role),
                new Claim(ClaimTypes.Email, us.Email),
                new Claim("UserId", us.Id.ToString()),
                new Claim("TokenId", Guid.NewGuid().ToString())}),
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha512Signature)
            };
            var token = jwtTokenHandler.CreateToken(tokenDecription);
            string accessToken = jwtTokenHandler.WriteToken(token);
            return accessToken;
        }

        public async Task<APIResponeModel> Login(UserLoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null && !user.IsActived)
            {
                return new APIResponeModel()
                {
                    Code = 404,
                    Message = "your account is deleted! Please try with another.",
                    IsSucceed = false
                };
            }
            var result = new APIResponeModel
            {
                Code = 404,
                Message = "Username or password is not correct!",
                IsSucceed = false
            };
            var loginResult = await _signInManager.CheckPasswordSignInAsync(user, model.Password, lockoutOnFailure: true);
            if (loginResult.Succeeded)
            {
                result.Code = 200;
                result.Message = "Login successfully!";
                result.IsSucceed = true;
                result.Data = await GenerateToken(user);
            }
            else if (loginResult.IsLockedOut)
            {
                result.Code = 400;
                result.Message = "Your account is loocked!";
                result.IsSucceed = false;
            }
            return result;
        }

        public async Task<APIResponeModel> Register(UserRegisterModel model)
        {
            var result = new APIResponeModel()
            {
                Code = 200,
                IsSucceed = true,
                Message = "Register successfully!",
                Data = model
            };
            var user = model.MapTo<UserIdentity>();

            var resultAddUser = await _userManager.CreateAsync(user, model.Password);
            if (resultAddUser.Succeeded == true)
            {
                if (await _roleManager.RoleExistsAsync(UserRole.NormalUser))
                {
                    await _userManager.AddToRoleAsync(user, UserRole.NormalUser);
                }
            }
            else
            {
                string errorMesage = "";
                foreach (var error in resultAddUser.Errors)
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

            return result;
        }

        public async Task<bool> UpdateRole(Guid userId, string roleType)
        {
            var userEntity = await GetByIdAsync(userId);
            if (userEntity == null) return false;
            //Retrieve the user's current roles
            var currentRoles = await _userManager.GetRolesAsync(userEntity);
            //Update the user's roles
            foreach (var role in currentRoles)
            {
                if (role != roleType)
                {
                    await _userManager.RemoveFromRoleAsync(userEntity, role);
                }
            }
            await _userManager.AddToRoleAsync(userEntity, roleType);
            return true;
        }
        public async Task<bool> DeactivateOrActivateUser(bool isActive, Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null) return false;
            user.IsActived = isActive;
            _context.Users.Update(user);
            bool result = await _context.SaveChangesAsync() > 0;
            return result;
        }
        public async Task<APIResponeModel> AddNewUser(UserAddModel model)
        {
            APIResponeModel result = new APIResponeModel()
            {
                Code = 200,
                IsSucceed = true,
                Message = "add successfully!",
                Data = model
            };
            string passwordGen = CommonService.GeneratePassword(8);
            var entity = model.MapTo<UserIdentity>();
            var resultAdd = await _userManager.CreateAsync(entity, passwordGen);
            if (resultAdd.Succeeded)
            {
                if (await _roleManager.RoleExistsAsync(model.RoleName))
                {
                    await _userManager.AddToRoleAsync(entity, model.RoleName);
                }
                var emailModel = await GetEmailForAddUser(model, passwordGen);
                await _emailService.SendEmailAsync(emailModel);
            }
            else
            {
                string errorMesage = "";
                foreach (var error in resultAdd.Errors)
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
            return result;
        }

        private async Task<EmailModel> GetEmailForAddUser(UserAddModel model, string password)
        {
            EmailModel result = new EmailModel();
            List<string> emailTos = new List<string>();
            emailTos.Add(model.Email);
            result.Subject = EmailTemplateSubjectConstant.AddNewUserSubject;
            string bodyEmail = string.Format(EmailTemplateBodyConstant.AddNewUserBody, model.Email, model.Email, model.Username, password, model.RoleName);
            result.Body = bodyEmail + EmailTemplateBodyConstant.SignatureFooter;
            result.To = emailTos;
            return await Task.FromResult(result);
        }
    }
}
