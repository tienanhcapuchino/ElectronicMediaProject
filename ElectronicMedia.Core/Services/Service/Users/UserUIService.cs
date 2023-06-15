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

using ElectronicMedia.Core.Automaper;
using ElectronicMedia.Core.Common;
using ElectronicMedia.Core.Common.Extension;
using ElectronicMedia.Core.Repository.Entity;
using ElectronicMedia.Core.Repository.Models;
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

        public async Task<string> GenerateToken(User us)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var secretKeyBytes = Encoding.UTF8.GetBytes(_appSettings.SecretKey);
            var tokenDecription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]{
                new Claim(ClaimTypes.Name, us.UserName),
                new Claim(ClaimTypes.Role, us.Role.ToString()),
                new Claim(ClaimTypes.Email, us.Email),
                new Claim("UserId", us.Id.ToString()),
                new Claim("TokenId", Guid.NewGuid().ToString())}),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha512Signature)
            };
            var token = jwtTokenHandler.CreateToken(tokenDecription);
            string accessToken = jwtTokenHandler.WriteToken(token);
            var userToken = new UserToken()
            {
                UserId = us.Id,
                IsRevoked = false,
                IsUsed = false,
                IssuedAt = DateTime.UtcNow,
                ExpiredAt = DateTime.UtcNow.AddMinutes(30),
                AccessToken = accessToken,
                RefreshToken = GenerateRefreshToken(),
                JwtId = token.Id,
            };
            await _context.AddAsync(userToken);
            await _context.SaveChangesAsync();
            return accessToken;
        }

        public async Task<APIResponeModel> Login(UserLoginModel model)
        {
            var result = new APIResponeModel();
            var userLogin = await _context.Users.SingleOrDefaultAsync(x => x.UserName == model.Username
            || x.Email == model.Username);
            if (userLogin == null)
            {
                result.Code = 404;
                result.Message = "Username or password is not correct!";
                result.IsSucceed = false;
            }
            else
            {
                if (!CommonService.VerifyPassword(model.Password, userLogin.Password))
                {
                    return new APIResponeModel()
                    {
                        Code = 404,
                        Message = "Username or password is not correct!",
                        IsSucceed = false
                    };
                }
                result.Code = 200;
                result.Message = "Login successfully!";
                result.IsSucceed = true;
                await GenerateToken(userLogin);
            }
            return result;
        }

        public async Task<APIResponeModel> Register(UserRegisterModel model)
        {
            var result = new APIResponeModel();
            result = IsValidUserRegister(model);
            if (!result.IsSucceed)
            {
                return result;
            }
            var user = model.MapTo<User>();

            await Add(user);
            UserIdentity u = new UserIdentity()
            {
                Id = user.Id.ToString(),
                Email = user.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = user.UserName
            };
            var resultAddUser = await userManager.CreateAsync(u, model.Password);


            if (await roleManager.RoleExistsAsync(UserRole.NormalUser))
            {
                await userManager.AddToRoleAsync(u, UserRole.NormalUser);
            }
            /* if (!await Add(user))
             {

                 return new APIResponeModel()
                 {
                     Code = 400,
                     Message = "Add user to database failed",
                     IsSucceed = false
                 };
             }*/
            return result;
        }
        public async Task<APIResponeModel> RenewToken(TokenModel model)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKeyBytes = Encoding.UTF8.GetBytes(_appSettings.SecretKey);
            var tokenValidateParam = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),
                ClockSkew = TimeSpan.Zero,
                ValidateLifetime = false, //không kiểm tra token hết hạn
            };
            try
            {
                // Check 1: AccessToken valid format
                var tokenInVerification = jwtTokenHandler.ValidateToken(model.AccessToken, tokenValidateParam, out var validatedToken);

                // Check 2: Check alg
                if (validatedToken is JwtSecurityToken jwtSecurityToken)
                {
                    var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512, StringComparison.InvariantCultureIgnoreCase);
                    if (!result)
                    {
                        return new APIResponeModel
                        {
                            IsSucceed = false,
                            Message = "Invalid Token",
                        };
                    }
                }

                // Check 3: Check access expire
                var UtcExpireDate = long.Parse(tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
                var expireDate = ConverUnixTimeToDateTime(UtcExpireDate);
                if (expireDate > DateTime.UtcNow)
                {
                    return new APIResponeModel
                    {
                        IsSucceed = false,
                        Message = "Access token has not yet expired",
                    };
                }

                // Check 4: Check refresh token exist in db
                var storedToken = _context.UserTokens.FirstOrDefault(x => x.RefreshToken == model.RefreshToken);
                if (storedToken == null)
                {
                    return new APIResponeModel
                    {
                        IsSucceed = false,
                        Message = "Refresh token does not exist"
                    };
                }

                // Check 5: Check refresh token isUsed/revoked
                if (storedToken.IsUsed)
                {
                    return new APIResponeModel
                    {
                        IsSucceed = false,
                        Message = "Refresh token has been used"
                    };
                }
                if (storedToken.IsRevoked)
                {
                    return new APIResponeModel
                    {
                        IsSucceed = false,
                        Message = "Refresh token has been revoked"
                    };
                }

                // Check 6: Check refresh token isUsed/revoked
                var jti = tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
                if (storedToken.JwtId != jti)
                {
                    return new APIResponeModel
                    {
                        IsSucceed = false,
                        Message = "Token doesn't match"
                    };
                }

                // Update token is used
                storedToken.IsUsed = true;
                storedToken.IsRevoked = true;
                _context.Update(storedToken);
                await _context.SaveChangesAsync();

                //Create new token
                var user = await GetByIdAsync(storedToken.UserId);
                var token = await GenerateToken(user);

                return new APIResponeModel
                {
                    IsSucceed = true,
                    Message = "Renew token Success",
                    Data = token
                };
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message);
                return new APIResponeModel
                {
                    IsSucceed = false,
                    Message = ex.Message,
                };
            }
        }
    }
}
