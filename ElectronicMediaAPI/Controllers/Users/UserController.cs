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
using ElectronicMedia.Core;
using ElectronicMedia.Core.Repository.Entity;
using ElectronicMedia.Core.Repository.Models;
using ElectronicMedia.Core.Repository.Models.Email;
using ElectronicMedia.Core.Services.Interfaces;
using ElectronicMedia.Core.Services.Interfaces.Email;
using ElectronicMedia.Core.Services.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicMediaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public IEmailService _emailService;
        private readonly log4net.ILog _logger = log4net.LogManager.GetLogger(typeof(UserController));
        public UserController(IUserService userService, IEmailService mailService)
        {
            _userService = userService;
            _emailService = mailService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("getall")]
        public IActionResult GetAllUser(PageRequestBody requestBody)
        {
            try
            {
                var result = _userService.GetAllWithPaging(requestBody);
                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                return new JsonResult(new ResultDto<PagedList<UserIdentity>>
                {
                    Status = ApiResultStatus.Failed,
                    ErrorMessage = ex.Message
                });
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
                _logger.Error("error when login", ex);
                return new APIResponeModel()
                {
                    Code = 400,
                    Message = ex.ToString(),
                    IsSucceed = false
                };
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
                _logger.Error("error when login", ex);
                return new APIResponeModel()
                {
                    Code = 400,
                    Message = ex.ToString(),
                    IsSucceed = false
                };
            }
        }
        [HttpPost("testSendMail")]
        public async Task<APIResponeModel> TestSendMail(EmailModel emailModel)
        {

            try
            {
                await _emailService.SendEmailAsync(emailModel);
                return new APIResponeModel()
                {
                    Code = 200,
                    Message = "OK",
                    IsSucceed = true,
                    Data = "Send email success"
                };

            }
            catch (Exception ex)
            {
                return new APIResponeModel()
                {
                    Code = 400,
                    Message = "Error: " + ex.Message,
                    IsSucceed = false,
                    Data = ex.ToString(),
                };
            }

        }
    }
}
