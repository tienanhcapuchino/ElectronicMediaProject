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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ElectronicMediaAPI.Controllers.Category
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly IPostCategoryService _postCategoryService;
        public CategoryController(ILogger<CategoryController> logger,
            IPostCategoryService postCategoryService)
        {
            _postCategoryService = postCategoryService;
            _logger = logger;
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
                return new JsonResult(new ResultDto<PagedList<User>>
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
                _logger.LogError("error when get category", ex);
                return new JsonResult(new ResultDto<PagedList<User>>
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
                return new JsonResult(new ResultDto<PagedList<User>>
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
                return new JsonResult(new ResultDto<PagedList<User>>
                {
                    Status = ApiResultStatus.Failed,
                    ErrorMessage = ex.Message
                });
            }
        }
    }
}
