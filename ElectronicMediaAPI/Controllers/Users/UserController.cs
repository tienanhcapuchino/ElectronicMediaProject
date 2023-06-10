using ElectronicMedia.Core;
using ElectronicMedia.Core.Repository.Entity;
using ElectronicMedia.Core.Repository.Models;
using ElectronicMedia.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicMediaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private ILogger<UserController> _logger;
        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet("{userId}")]
        public async Task<User> GetById([FromRoute] Guid userId)
        {
            try
            {
                var user = await _userService.GetByIdAsync(userId);
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError($"error when get user by id: {userId}", ex);
                throw;
            }
        }
        [HttpGet]
        public IActionResult GetAllUser(PageRequestBody requestBody)
        {
            try
            {
                var result = _userService.GetAllWithPaging(requestBody);
                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                return new JsonResult(new ResultDto<PagedList<User>>
                {
                    Status = ApiResultStatus.Failed,
                    ErrorMessage = ex.Message
                });
            }
        }

        [HttpPost("renew")]
        public async Task<APIResponeModel> RenewToken(TokenModel model)
        {
            try
            {
                return await _userService.RenewToken(model);
            }
            catch (Exception ex)
            {
                _logger.LogError($"error when renew token", ex);
                throw;
            }
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
