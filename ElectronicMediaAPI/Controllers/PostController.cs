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

using ElectronicMedia.Core;
using ElectronicMedia.Core.Repository.Entity;
using ElectronicMedia.Core.Repository.Models;
using ElectronicMedia.Core.Services.Interfaces;
using ElectronicMedia.Core.Services.Service.Users;
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
        [HttpGet("id")]
        [BasicAuthentication("Admin")]
        public async Task<IActionResult> GetPostById(Guid id)
        {
            try
            {
                var result = await _postService.GetById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return new JsonResult(new ResultDto<PostViewModel>
                {
                    Status = ApiResultStatus.Failed,
                    ErrorMessage = ex.Message
                });
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
        [HttpPost("page")]
        public async Task<IActionResult> GetPostByPaging(PageRequestBody requestBody)
        {
            try
            {
                var result = await _postService.GetAllWithPaging(requestBody);
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
    }
}
