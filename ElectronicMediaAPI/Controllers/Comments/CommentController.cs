﻿using ElectronicMedia.Core.Repository.Models;
using ElectronicMedia.Core.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicMediaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ILogger<CommentController> _logger;
        private readonly ICommentService _commentService;
        public CommentController(ILogger<CommentController> logger, ICommentService commentService)
        {
            _logger = logger;
            _commentService = commentService;
        }
        [HttpGet("{postId}")]
        public async Task<List<CommentModel>> GetCommentsByPost([FromRoute] Guid postId)
        {
            try
            {
                var result = await _commentService.GetAllCommentsByPost(postId);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("error when create post category", ex);
                throw;
            }
        }
        [HttpPost("{userId}/create/{postId}")]
        public async Task<APIResponeModel> CreateComment([FromRoute] Guid userId, [FromRoute] Guid postId, string content)
        {
            try
            {
                if (await _commentService.CreateComment(userId, postId, content))
                {
                    return new APIResponeModel()
                    {
                        Code = 200,
                        Message = "OK",
                        IsSucceed = true,
                        Data = content
                    };
                }
                else
                {
                    return new APIResponeModel()
                    {
                        Code = 400,
                        Message = "add failed"
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"error when create comment at post: {postId} and user: {userId}", ex);
                throw;
            }
        }
    }
}