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

using ElectronicMedia.Core.Repository.Entity;
using ElectronicMedia.Core;
using ElectronicMedia.Core.Repository.Models;
using ElectronicMedia.Core.Services.Interfaces;
using ElectronicMedia.Core.Services.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace ElectronicMediaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        private readonly log4net.ILog _logger = log4net.LogManager.GetLogger(typeof(DepartmentController));
        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [Authorize(Roles = "Admin, EditorDirector")]
        [HttpPost("add")]
        public async Task<APIResponeModel> AddDepartment([FromBody] DepartmentModel model)
        {
            try
            {
                var result = await _departmentService.AddDepartment(model);
                return result;
            }
            catch (Exception ex)
            {
                _logger.Error($"error when add department with id: {model.Id}");
                return new APIResponeModel()
                {
                    Code = 400,
                    Message = ex.Message,
                    Data = ex.ToString(),
                    IsSucceed = false
                };
            }
        }

        [Authorize(Roles = "Admin, EditorDirector")]
        [HttpPut("update")]
        public async Task<APIResponeModel> Update([FromBody] DepartmentModel model)
        {

            try
            {
                var result = await _departmentService.UpdateDepartment(model);
                return result;
            }
            catch (Exception ex)
            {
                _logger.Error($"error when update department with id: {model.Id}");
                return new APIResponeModel()
                {
                    Code = 400,
                    Message = ex.Message,
                    Data = ex.ToString(),
                    IsSucceed = false
                };
            }
        }

        [Authorize(Roles = "Admin, EditorDirector")]
        [HttpDelete("delete/{depId}")]
        public async Task<APIResponeModel> Delete([FromRoute] Guid depId)
        {
            try
            {
                bool result = await _departmentService.Delete(depId);
                if (result)
                {
                    return new APIResponeModel()
                    {
                        Code = StatusCodes.Status200OK,
                        Message = "delete successfully",
                        Data = depId,
                        IsSucceed = true
                    };
                }
                else
                {
                    return new APIResponeModel()
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Message = "delete failed",
                        Data = depId,
                        IsSucceed = false
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"error when delete department with id: {depId}");
                return new APIResponeModel()
                {
                    Code = 400,
                    Message = ex.Message,
                    Data = ex.ToString(),
                    IsSucceed = false
                };
            }
        }

        [Authorize(Roles = "Admin, EditorDirector")]
        [HttpPost("page")]
        public async Task<IActionResult> GetAllDepartments(PageRequestBody requestBody)
        {
            try
            {
                var result = await _departmentService.GetAllWithPagingModel(requestBody);
                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                _logger.Error("error when get all departments with paging", ex);
                return new JsonResult(new ResultDto<PagedList<DepartmentModel>>
                {
                    Status = ApiResultStatus.Failed,
                    ErrorMessage = ex.Message
                });
            }
        }

        [Authorize(Roles = "Admin, EditorDirector")]
        [HttpGet("detail/{id}")]
        public async Task<DepartmentViewDetail> GetDepartmentById([FromRoute] Guid id)
        {
            try
            {
                var result = await _departmentService.ViewDetailDepartment(id);
                return result;
            }
            catch (Exception ex)
            {
                _logger.Error($"error when get department detail with departmentId: {id}", ex);
                return new DepartmentViewDetail();
            }
        }

        [Authorize(Roles = "Admin, EditorDirector, Leader")]
        [HttpGet("members")]
        public async Task<List<MemberModel>> GetAllMembers()
        {
            try
            {
                var result = await _departmentService.GetMembersToAssign();
                return result;
            }
            catch (Exception ex)
            {
                _logger.Error($"error when get all members", ex);
                return new List<MemberModel>();
            }
        }

        [Authorize(Roles = "Admin, EditorDirector")]
        [HttpGet("leaders")]
        public async Task<List<MemberModel>> GetAllLeaders()
        {
            try
            {
                var result = await _departmentService.GetLeadersToAssign();
                return result;
            }
            catch (Exception ex)
            {
                _logger.Error($"error when get all members", ex);
                return new List<MemberModel>();
            }
        }

        [Authorize(Roles = "Admin, EditorDirector, Leader")]
        [HttpDelete("kick/{memberId}/{departmentId}")]
        public async Task<APIResponeModel> KickMembers([FromRoute] string memberId, [FromRoute] Guid departmentId)
        {
            try
            {
                var result = await _departmentService.KickMember(departmentId, memberId);
                return result;
            }
            catch (Exception ex)
            {
                _logger.Error($"error when get all members", ex);
                return new APIResponeModel()
                {
                    Code = 400,
                    Message = ex.Message,
                    Data = ex.ToString(),
                    IsSucceed = false
                };
            }
        }

        [Authorize(Roles = "Admin, EditorDirector, Leader")]
        [HttpPut("assignmember/{memberId}/{departmentId}")]
        public async Task<bool> AssignMember([FromRoute] Guid memberId, [FromRoute] Guid departmentId)
        {
            try
            {
                bool result = await _departmentService.AssignMemberToDepartment(departmentId, memberId);
                return result;
            }
            catch(Exception ex)
            {
                _logger.Error($"error when get all members", ex);
                return false;
            }
        }
    }
}
