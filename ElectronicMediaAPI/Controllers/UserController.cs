using ElectronicMedia.Core.Repository.Models;
using ElectronicMedia.Core.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicMediaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private ILogger<UserController> _logger;
        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }
        [HttpPost("login")]
        public async Task<APIResponeModel> Login(UserLoginModel model)
        {
            try
            {
                var result = await _userService.Login(model);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("error when login", ex);
                throw;
            }
        }
        [HttpPost("register")]
        public async Task<APIResponeModel> Register(UserRegisterModel model)
        {
            try
            {
                var result = await _userService.Register(model);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("error when login", ex);
                throw;
            }
        }
    }
}
