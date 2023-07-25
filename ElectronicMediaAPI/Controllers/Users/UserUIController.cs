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
using ElectronicMedia.Core.Common;
using ElectronicMedia.Core.Repository.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicMediaAPI.Controllers
{
    public partial class UserController
    {
        [Authorize]
        [HttpGet("profile/{userId}")]
        public async Task<UserProfileModel> GetUserProfile([FromRoute] Guid userId)
        {
            try
            {
                _logger.Info("START get user profile!");
                var profile = await _userService.GetProfileUser(userId);
                return profile;
            }
            catch (Exception ex)
            {
                _logger.Error($"Error when get user profile with userId: {userId}", ex);
                return null;
            }
        }

        [Authorize]
        [HttpPost("update/profile")]
        public APIResponeModel UpdateProfile(UserProfileUpdateModel profile)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors)
                                         .Select(e => e.ErrorMessage)
                                         .ToList();
                    return new APIResponeModel()
                    {
                        Code = 400,
                        Data = errors,
                        IsSucceed = false,
                        Message = string.Join(";", errors)
                    };
                }
                var result = _userService.UpdateUserProfile(profile);
                return result;
            }
            catch (Exception ex)
            {
                _logger.Error($"Error when update profile user with userId: {profile.Id}", ex);
                throw;
            }
        }

        [Authorize(Roles = $"{UserRole.Admin}")]
        [HttpPut("roleupdate/{userId}")]
        public async Task<APIResponeModel> UpdateRole([FromRoute] Guid userId, string newRole)
        {
            try
            {
                var result = await _userService.UpdateRole(userId, newRole);
                if (result)
                {
                    return new APIResponeModel()
                    {
                        IsSucceed = true,
                        Code = 200,
                        Message = "update successfully",
                        Data = userId.ToString()
                    };
                }
                else
                {
                    return new APIResponeModel()
                    {
                        IsSucceed = false,
                        Code = 400,
                        Message = "update failed!",
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"Error when update role of user with userId: {userId}", ex);
                return new APIResponeModel()
                {
                    Data = ex.ToString(),
                    Message = ex.Message,
                    IsSucceed = false,
                    Code = 400
                };
            }
        }

        [Authorize(Roles = $"{UserRole.Admin}")]
        [HttpPut("deactivate/{userId}")]
        public async Task<APIResponeModel> DeactivateUser([FromRoute] Guid userId, bool isActive)
        {
            try
            {
                bool result = await _userService.DeactivateOrActivateUser(isActive, userId);
                if (result)
                {
                    return new APIResponeModel()
                    {
                        IsSucceed = true,
                        Code = 200,
                        Message = "update successfully",
                        Data = userId.ToString()
                    };
                }
                else
                {
                    return new APIResponeModel()
                    {
                        IsSucceed = false,
                        Code = 400,
                        Message = "update failed!",
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"Error when deactivate user with userId: {userId}", ex);
                return new APIResponeModel()
                {
                    Data = ex.ToString(),
                    Message = ex.Message,
                    IsSucceed = false,
                    Code = 400
                };
            }
        }
        [HttpPost("resetpassword")]
        public async Task<APIResponeModel> ResetPassword([FromBody] string email)
        {
            try
            {
                bool result = await _userService.ResetPassword(email);
                if (result)
                {
                    return new APIResponeModel()
                    {
                        IsSucceed = true,
                        Code = 200,
                        Message = "reset successfully"        
                    };
                }
                else
                {
                    return new APIResponeModel()
                    {
                        IsSucceed = false,
                        Code = 400,
                        Message = "reset failed!",
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"Error when reset password", ex);
                return new APIResponeModel()
                {
                    Data = ex.ToString(),
                    Message = ex.Message,
                    IsSucceed = false,
                    Code = 400
                };
            }
        }

        [Authorize(Roles = $"{UserRole.Admin}")]
        [HttpPost("adduser")]
        public async Task<APIResponeModel> AddNewUser([FromBody] UserAddModel userModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors)
                                         .Select(e => e.ErrorMessage)
                                         .ToList();
                    return new APIResponeModel()
                    {
                        Code = 400,
                        Data = errors,
                        IsSucceed = false,
                        Message = string.Join(";", errors)
                    };
                }
                var result = await _userService.AddNewUser(userModel);
                return result;
            }
            catch (Exception ex)
            {
                _logger.Error($"Error when add new user", ex);
                return new APIResponeModel()
                {
                    Data = userModel,
                    Message = ex.ToString(),
                    IsSucceed = false,
                    Code = 400
                };
            }
        }
    }
}
