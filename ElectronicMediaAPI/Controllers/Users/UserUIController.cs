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

using ElectronicMedia.Core.Repository.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicMediaAPI.Controllers
{
    public partial class UserController
    {
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
        [HttpPost("update/profile/{userId}")]
        public async Task<bool> UpdateProfile([FromRoute] Guid userId, [FromBody] UserProfileModel profile)
        {
            try
            {
                var result = await _userService.UpdateUserProfile(userId, profile);
                return result;
            }
            catch (Exception ex)
            {
                _logger.Error($"Error when update profile user with userId: {userId}", ex);
                return false;
            }
        }

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
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
    }
}
