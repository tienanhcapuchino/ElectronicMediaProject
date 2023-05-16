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
        public PostController(ILogger<PostController> logger, IPostService postService)
        {
            _logger = logger;
            _postService = postService;
        }
        [HttpPost("category/create")]
        public async Task<APIResponeModel> CreatePostCategory(string categoryName)
        {
            try
            {
                if (await _postService.CreatePostCategory(categoryName))
                {
                    return new APIResponeModel()
                    {
                        Code = 200,
                        Message = "OK",
                        IsSucceed = true,
                        Data = categoryName
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
        [HttpPost("create")]
        public async Task<APIResponeModel> CreatePost(PostModel model)
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
