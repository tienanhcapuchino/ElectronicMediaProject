using ElectronicMedia.Core;
using ElectronicMedia.Core.Repository.Entity;
using ElectronicMedia.Core.Repository.Models;
using ElectronicMedia.Core.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicMediaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly ILogger<PostController> _logger;
        private readonly IPostService _postService;
        private readonly IPostCategoryService _postCategoryService;
        public PostController(ILogger<PostController> logger,
            IPostService postService,
            IPostCategoryService postCategoryService)
        {
            _logger = logger;
            _postService = postService;
            _postCategoryService = postCategoryService;
        }
        [HttpPost("category/create")]
        public async Task<APIResponeModel> CreatePostCategory(PostCategoryModel model)
        {
            try
            {
                if (await _postCategoryService.CreatePostCate(model))
                {
                    return new APIResponeModel()
                    {
                        Code = 200,
                        Message = "OK",
                        IsSucceed = true,
                        Data = model
                    };
                }
                else
                {
                    return new APIResponeModel()
                    {
                        Code = 400,
                        IsSucceed = false,
                        Message = "add failed"
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("error when create post category", ex);
                throw;
            }
        }
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
        [HttpPost("vote")]
        public async Task<APIResponeModel> VotePost([FromBody] PostDetailModel postDetail)
        {
            try
            {
                if (await _postService.VotePost(postDetail))
                {
                    return new APIResponeModel()
                    {
                        Code = 200,
                        Message = "OK",
                        IsSucceed = true,
                        Data = postDetail
                    };
                }
                else
                {
                    return new APIResponeModel()
                    {
                        Code = 400,
                        Message = "failed",
                        IsSucceed = false,
                        Data = postDetail
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"error when vote post at post: {postDetail.PostId} by user {postDetail.AuthorId}", ex);
                return new APIResponeModel()
                {
                    Code = 400,
                    Message = ex.ToString(),
                    Data = postDetail
                };
            }
        }

        [HttpGet("category")]
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

        [HttpPost("create")]
        public async Task<APIResponeModel> CreatePost([FromForm] PostModel model)
        {
            try
            {
                if (await _postService.CreatePost(model))
                {
                    return new APIResponeModel()
                    {
                        Code = 200,
                        Message = "OK",
                        IsSucceed = true,
                        Data = model
                    };
                }
                else
                {
                    return new APIResponeModel()
                    {
                        Code = 400,
                        IsSucceed = false,
                        Message = "add failed"
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("error when create post", ex);
                throw;
            }
        }
    }
}
