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
using ElectronicMedia.Core.Services.Interfaces;
using ElectronicMedia.Core.Services.Service;
using ElectronicMediaAPI.Controllers.Post;
using Microsoft.AspNetCore.Mvc;
using ElectronicMedia.Core.Common;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using ElectronicMedia.Core.Repository.Models;

namespace ElectronicMediaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly log4net.ILog _logger = log4net.LogManager.GetLogger(typeof(CategoryController));
        private readonly IPostCategoryService _postCategoryService;
        public CategoryController(IPostCategoryService postCategoryService)
        {
            _postCategoryService = postCategoryService;
        }
        // GET: api/<CategoryController>
        [HttpPost]
        public async Task<IActionResult> GetAllCategory(PageRequestBody requestBody)
        {
            try
            {
                var result = await _postCategoryService.GetAllWithPaging(requestBody);
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
        [HttpGet("category")]
        //[Authorize(Roles = $"{UserRole.Admin},{UserRole.NormalUser}")]
        public async Task<IActionResult> GetAllCate()
        {
            try
            {
                var result = await _postCategoryService.GetAllAsync();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var category = await _postCategoryService.GetByIdAsync(id);
                return Ok(category);
            }
            catch(Exception ex)
            {
                _logger.Error("error when get category", ex);
                return new JsonResult(new ResultDto<PagedList<UserIdentity>>
                {
                    Status = ApiResultStatus.Failed,
                    ErrorMessage = ex.Message
                });
            }
        }

        // POST api/<CategoryController>
        [HttpPost("category/create")]
        //[Authorize(UserRole.Admin)]
        public async Task<IActionResult> CreatePostCategory(PostCategoryModel model)
        {
            try
            {
                return Ok(await _postCategoryService.CreatePostCate(model));
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

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                return Ok(_postCategoryService.Delete(id));
            }catch(Exception ex)
            {
                return new JsonResult(new ResultDto<PagedList<UserIdentity>>
                {
                    Status = ApiResultStatus.Failed,
                    ErrorMessage = ex.Message
                });
            }
        }
    }
}
