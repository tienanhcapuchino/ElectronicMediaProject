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

using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using ElectronicMedia.Core;
using ElectronicMedia.Core.Common;
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

        [Authorize(Roles = $"{UserRole.Admin}")]
        [HttpPost("getall")]
        public async Task<IActionResult> GetAllUser(PageRequestBody requestBody)
        {
            try
            {
                var result = await _userService.GetAllWithPagingModels(requestBody);
                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                _logger.Error($"Error when get all users with paging", ex);
                return new JsonResult(new ResultDto<PagedList<UsersModel>>
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
        [HttpPost("testsendmail")]
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

        [Authorize(Roles = $"{UserRole.Admin}")]
        [HttpGet("export")]
        public async Task<IActionResult> ExportUsers()
        {
            try
            {
                var dt = await _userService.ExportUsers();
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.ColumnWidth = 25;
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Export_Users.xlsx");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error("error when export users to excel", ex);
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("departmentId/{userId}")]
        public async Task<string> GetDepartmentIdByUserId([FromRoute] Guid userId)
        {
            try
            {
                var user = await _userService.GetByIdAsync(userId);
                if (user == null || user.DepartmentId == null)
                {
                    return Guid.Empty.ToString();
                }
                return user.DepartmentId.ToString();
            }
            catch(Exception ex)
            {
                _logger.Error($"error when get departmentId by userId: {userId}", ex);
                return Guid.Empty.ToString();
            }
        }
    }
}
