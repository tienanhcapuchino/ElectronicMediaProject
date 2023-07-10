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

using ClosedXML.Excel;
using ElectronicMedia.Core;
using ElectronicMedia.Core.Common;
using ElectronicMedia.Core.Repository.Entity;
using ElectronicMedia.Core.Repository.Models;
using ElectronicMedia.Core.Services.Interfaces;
using ElectronicMedia.Core.Services.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicMediaAPI.Controllers.Post
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class PostController : ControllerBase
    {
        private readonly log4net.ILog _logger = log4net.LogManager.GetLogger(typeof(PostController));
        private readonly IPostService _postService;
        private readonly IFileStorageService _fileStorageService;
        public PostController(IPostService postService,
            IFileStorageService fileStorageService)
        {
            _postService = postService;
            _fileStorageService = fileStorageService;
        }
        [HttpGet("{id}")]
        [Authorize(Roles = "NormalUser,Admin")]
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
                _logger.Error($"error when vote post at post: {postDetail.PostId} by user {postDetail.AuthorId}", ex);
                return new APIResponeModel()
                {
                    Code = 400,
                    Message = ex.ToString(),
                    Data = postDetail
                };
            }
        }
        [HttpPost("create")]
        public async Task<APIResponeModel> CreatePost([FromForm] PostModel model)
        {
            try
            {
                if (await _postService.CreatePost(model))
                {
                    _fileStorageService.SaveImageFile(model.FileURL);
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
                _logger.Error("error when create post", ex);
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
                return new JsonResult(new ResultDto<PagedList<UserIdentity>>
                {
                    Status = ApiResultStatus.Failed,
                    ErrorMessage = ex.Message
                });
            }
        }
        [HttpDelete("delete/{postId}")]
        public async Task<APIResponeModel> DeletePost([FromRoute] Guid postId)
        {
            try
            {
                var post = await _postService.GetByIdAsync(postId);
                if (await _postService.DeletePost(postId))
                {
                    if (!string.IsNullOrEmpty(post.Image))
                    {
                        _fileStorageService.DeleteImageFile(post.Image);
                    }
                    return new APIResponeModel()
                    {
                        Code = StatusCodes.Status200OK,
                        Message = "Ok",
                        Data = post,
                        IsSucceed = true
                    };
                }
                else
                {
                    return new APIResponeModel()
                    {
                        Code = StatusCodes.Status400BadRequest,
                        IsSucceed = false,
                        Data = post,
                        Message = "delete failed"
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"Error when delete post with postId {postId}");
                return new APIResponeModel()
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = ex.ToString(),
                    IsSucceed = false,
                    Data = postId
                };
            }
        }

        [HttpGet("export")]
        public async Task<IActionResult> ExportPost()
        {
            try
            {
                var dt = await _postService.ExportPosts();
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.ColumnWidth = 25;
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Export_Posts.xlsx");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error("error when export posts to excel!", ex);
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet("/category/{cateId}")]
        public async Task<IActionResult> GetPostByCateId(Guid cateId)
        {
            try
            {
                var result = await _postService.GetPostByCateId(cateId);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
    }
}
