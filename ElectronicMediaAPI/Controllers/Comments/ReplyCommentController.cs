using ElectronicMedia.Core.Repository.Models;
using ElectronicMedia.Core.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ElectronicMediaAPI.Controllers.Comments
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReplyCommentController : ControllerBase
    {
        private readonly ILogger<ReplyCommentController> _logger;
        private readonly IReplyCommentService _replyCommentService;
        public ReplyCommentController(ILogger<ReplyCommentController> logger, IReplyCommentService replyCommentService)
        {
            _logger = logger;
            _replyCommentService = replyCommentService;
        }
        [HttpGet("replycomments/{parentId}")]
        public async Task<List<ReplyCommentModel>> GetReplyCommentsByParentId([FromRoute] Guid parentId)
        {
            try
            {
                var result = await _replyCommentService.GetReplyCommentsByParentId(parentId);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error when get all reply comments at comment: {parentId}", ex);
                throw;
            }
        }
        [HttpPost("create")]
        public async Task<APIResponeModel> CreateReplyComment([FromBody] ReplyCommentModel replyComment)
        {
            try
            {
                var result = await _replyCommentService.CreateReplyComment(replyComment);
                if (result)
                {
                    return new APIResponeModel()
                    {
                        Code = 200,
                        Message = "ok",
                        IsSucceed = true,
                        Data = replyComment
                    };
                }
                else
                {
                    return new APIResponeModel()
                    {
                        Code = 400,
                        Message = "create failed",
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error when create reply comment", ex);
                throw;
            }
        }
        [HttpDelete("remove/{replyId}")]
        public async Task<APIResponeModel> RemoveReplyComment([FromRoute] Guid replyId)
        {
            try
            {
                bool result = await _replyCommentService.Delete(replyId);
                if (result)
                {
                    return new APIResponeModel()
                    {
                        Code = 200,
                        Message = "Ok",
                        Data = replyId,
                        IsSucceed = true,
                    };
                }
                else
                {
                    return new APIResponeModel()
                    {
                        Code = 400,
                        Message = "failed",
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error when remove reply comment: {replyId}", ex);
                throw;
            }
        }
        [HttpPut("edit/{replyId}")]
        public async Task<APIResponeModel> EditReplyComment([FromRoute] Guid replyId, string content)
        {
            try
            {
                bool result = await _replyCommentService.UpdateReplyComment(replyId, content);
                if (result)
                {
                    return new APIResponeModel()
                    {
                        Code = 200,
                        Message = "Ok",
                        Data = replyId + " - " + content,
                        IsSucceed = true,
                    };
                }
                else
                {
                    return new APIResponeModel()
                    {
                        Code = 400,
                        Message = "failed",
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error when edit reply comment: {replyId}", ex);
                throw;
            }
        }
    }
}
