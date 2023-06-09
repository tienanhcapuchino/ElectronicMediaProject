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

using ElectronicMedia.Core.Repository.Models;
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
        [HttpGet("duplicateemail/{userId}")]
        public async Task<bool> CheckDupicateEmail([FromRoute] Guid userId, string email)
        {
            try
            {
                if (string.IsNullOrEmpty(email))
                {
                    throw new Exception("Cannot leave email is empty!");
                }
                var result = await _userService.IsDuplicateEmail(userId, email);
                return result;
            }
            catch (Exception ex)
            {
                _logger.Error($"Error when check duplicate email with userId: {userId}", ex);
                return false;
            }
        }
        [HttpGet("duplicatephone/{userId}")]
        public async Task<bool> CheckDupicatePhone([FromRoute] Guid userId, string phone)
        {
            try
            {
                if (string.IsNullOrEmpty(phone))
                {
                    throw new Exception("Cannot leave phone is empty!");
                }
                var result = await _userService.IsDuplicatePhone(userId, phone);
                return result;
            }
            catch (Exception ex)
            {
                _logger.Error($"Error when check duplicate phone with userId: {userId}", ex);
                return false;
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
    }
}
