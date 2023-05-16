using ElectronicMedia.Core.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    }
}
