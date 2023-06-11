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
                var profile = await _userService.GetProfileUser(userId);
                return profile;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error when get user profile with userId: {userId}", ex);
                throw;
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
                _logger.LogError($"Error when check duplicate email with userId: {userId} and email: {email}", ex);
                throw;
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
                _logger.LogError($"Error when check duplicate phone with userId: {userId} and phone: {phone}", ex);
                throw;
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
                _logger.LogError($"Error when update profile user with userId: {userId}", ex);
                throw;
            }
        }
    }
}
