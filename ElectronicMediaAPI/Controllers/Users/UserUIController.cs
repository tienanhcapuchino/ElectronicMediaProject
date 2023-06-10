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
    }
}
