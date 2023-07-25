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

using AutoMapper;
using DocumentFormat.OpenXml.Bibliography;
using ElectronicMedia.Core.Automaper;
using ElectronicMedia.Core.Common;
using ElectronicMedia.Core.Common.Extension;
using ElectronicMedia.Core.Repository.DataContext;
using ElectronicMedia.Core.Repository.Domains;
using ElectronicMedia.Core.Repository.Entity;
using ElectronicMedia.Core.Repository.Models;
using ElectronicMedia.Core.Repository.Models.Email;
using ElectronicMedia.Core.RequestBody;
using ElectronicMedia.Core.Services.Interfaces;
using ElectronicMedia.Core.Services.Interfaces.Email;
using log4net.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicMedia.Core.Services.Service
{
    public class PostService : IPostService
    {
        private readonly ElectronicMediaDbContext _context;
        private readonly IPostDetailService _postDetailService;
        private readonly ICommentService _commentService;
        private readonly IReplyCommentService _replyCommentService;
        private readonly IExcelService<Post> _excelService;
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        private readonly log4net.ILog _logger = log4net.LogManager.GetLogger(typeof(PostService));
        public PostService(ElectronicMediaDbContext context,
            IPostDetailService postDetailService,
            ICommentService commentService,
            IReplyCommentService replyCommentService,
            IExcelService<Post> excelService,
            IUserService userService,
            IEmailService emailService
            )
        {
            _context = context;
            _postDetailService = postDetailService;
            _commentService = commentService;
            _replyCommentService = replyCommentService;
            _excelService = excelService;
            _userService = userService;
            _emailService = emailService;
        }

        public async Task<bool> Add(Post entity, bool saveChange = true)
        {
            await _context.AddAsync(entity);
            bool result = true;
            if (saveChange)
            {
                result = await _context.SaveChangesAsync() > 0;
            }
            return result;
        }


        public async Task<bool> CreatePost(PostModel post)
        {
            var categoryIds = await _context.PostCategories.Select(c => c.Id).ToListAsync();
            if (string.IsNullOrEmpty(post.Title)
                || string.IsNullOrEmpty(post.Content)
                || categoryIds == null || !categoryIds.Any()
                || !categoryIds.Contains(post.CategoryId)
                || post.FileURL == null)
            {
                return false;
            }
            var entity = post.MapTo<Post>();
            bool result = await Add(entity);
            return result;
        }
        public async Task<bool> UpdatePost(PostViewModel post)
        {
            var categoryIds = await _context.PostCategories.Select(c => c.Id).ToListAsync();
            var oldPost = await GetById(post.Id);
            bool result = false;
            if (oldPost != null)
            {
                if (string.IsNullOrEmpty(post.Title)
                || string.IsNullOrEmpty(post.Content)
                || categoryIds == null || !categoryIds.Any()
                || !categoryIds.Contains(post.CategoryId))

                {
                    return false;
                }

                var entity = post.MapTo<Post>();
                result = await Update(entity);
                if (result == true)
                {
                    var currentUser = _userService.GetCurrentUser();
                    var role = currentUser.FindFirst("http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;
                    var receiveUser = await _userService.GetByIdAsync(Guid.Parse(entity.UserId));

                    if (post.Status.ToString() == "Published" && oldPost.Status.ToString() != "Published")
                    {
                        var emailModel = await SendEmailForPublishPost(receiveUser.Email, entity);
                        await _emailService.SendEmailAsync(emailModel);
                    }
                }

            }
            return result;

        }
        public async Task<bool> Delete(Guid id, bool saveChange = true)
        {
            bool result = true;
            var post = await GetByIdAsync(id);
            if (post == null)
            {
                return false;
            }
            _context.Posts.Remove(post);
            if (saveChange)
            {
                result = await _context.SaveChangesAsync() > 0;
            }
            return result;
        }

        public async Task<IEnumerable<Post>> GetAllAsync()
        {
            var result = await _context.Posts.ToListAsync();
            return result;
        }
        public async Task<IEnumerable<PostView>> GetNewPost(int take)
        {
            try
            {
                var post = await _context.Posts.Where(y => y.Status == PostStatusModel.Published).OrderBy(x => x.CreatedDate).Take(take).ToListAsync();
                return post.MapToList<PostView>();

            }
            catch (Exception ex)
            {
                throw;
            }

        }
        private async Task<List<Post>> GetListPostsOfLeader(string leaderId)
        {
            var depId = (Guid)_context.Users.FirstOrDefault(x => x.Id == leaderId).DepartmentId;

            var posts = await _context.Posts.Include(x => x.User).Include(x => x.SubCategory).Include(x => x.Category).ToListAsync();
            List<Post> postsLeader = new List<Post>();
            foreach (var post in posts)
            {
                if (post.User.DepartmentId == depId)
                {
                    postsLeader.Add(post);
                }
            }
            return postsLeader;
        }
        public async Task<PagedList<PostViewModel>> GetAllWithPaging(PageRequestBody requestBody)
        {
            try
            {
                var currentUser = _userService.GetCurrentUser();
                var role = currentUser.FindFirst("http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;
                var userId = currentUser.FindFirst("UserId")?.Value;
                var posts = await _context.Posts.Include(x => x.User).ToListAsync();
                var countItem = await CommonService.GetTotalCount<Post>(_context);
                if (role == UserRole.Writer)
                {
                    posts = await _context.Posts.Where(x => x.UserId == userId).Include(x => x.User).ToListAsync();
                    countItem = posts.Count();
                }
                if (role == UserRole.Leader)
                {
                    posts = await GetListPostsOfLeader(userId);
                    countItem = posts.Count();
                }
                var postModels = posts.MapTo<List<PostViewModel>>();
                var result = QueryData<PostViewModel>.QueryForModel(requestBody, postModels).ToList();
                return PagedList<PostViewModel>.ToPagedList(result, requestBody.Page, requestBody.Top, countItem);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<Post> GetByIdAsync(Guid id)
        {
            var post = await _context.Posts.AsNoTracking().Include(x => x.User).Where(x => x.Id == id).SingleOrDefaultAsync();
            return post;
        }
        public async Task<PostViewModel> GetById(Guid id)
        {
            var post = await GetByIdAsync(id);
            return post.MapTo<PostViewModel>();
        }

        public async Task<bool> Update(Post entity, bool saveChange = true)
        {
            _context.Posts.Update(entity);
            bool result = true;
            if (saveChange)
            {
                result = await _context.SaveChangesAsync() > 0;
            }
            return await Task.FromResult(result);
        }

        public async Task<bool> UpdateLikeAndDislike(Guid postId, bool liked)
        {
            var entity = await GetByIdAsync(postId);
            if (entity == null)
            {
                return false;
            }
            if (liked)
            {
                entity.Like += 1;
            }
            else
            {
                entity.Dislike += 1;
            }
            bool result = await Update(entity);
            return result;
        }

        public async Task<bool> VotePost(PostDetailModel postDetail)
        {
            bool result = false;
            var postDetailEntity = await _postDetailService.FindByUserId(postDetail.AuthorId, postDetail.PostId);
            if (postDetailEntity != null)
            {
                if (postDetail.Liked != postDetailEntity.Liked)
                {
                    return false;
                }
                else
                {
                    result = await _postDetailService.DeleteByUserIdAndPostId(Guid.Parse(postDetailEntity.UserId), postDetailEntity.PostId);
                    if (result)
                    {
                        result = await UpdateLikeDelete(postDetailEntity.PostId, postDetail.Liked);
                    }
                    return result;
                }
            }
            result = await _postDetailService.CreatePostDetail(postDetail);
            if (result)
            {
                result = await UpdateLikeAndDislike(postDetail.PostId, postDetail.Liked);
            }
            return result;
        }
        public async Task<bool> DeletePost(Guid postId, string message = "No reasons.")
        {
            var currentUser = _userService.GetCurrentUser();
            var role = currentUser.FindFirst("http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;
            var post = await GetByIdAsync(postId);
            if (post == null) return false;
            var receiveUser = await _userService.GetByIdAsync(Guid.Parse(post.UserId));
            var comments = await _commentService.GetAllCommentsByPost(postId);
            List<ReplyComment> replyComments = new List<ReplyComment>();
            List<Comment> commentEnities = new List<Comment>();
            if (comments != null && comments.CommentModels.Any())
            {
                commentEnities = comments.MapTo<List<Comment>>();
                var commentIds = commentEnities.Select(x => x.Id).ToList();
                replyComments = await _replyCommentService.GetAllRepliesByParentIds(commentIds);
            }
            if (replyComments.Any())
            {
                _context.ReplyComments.RemoveRange(replyComments);
            }
            if (commentEnities.Any())
            {
                _context.Comments.RemoveRange(commentEnities);
            }
            _context.Posts.Remove(post);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                if (role == UserRole.Admin || role == UserRole.EditorDirector)
                {
                    var emailModel = await SendEmailForDelete(receiveUser.Email, message, post);
                    await _emailService.SendEmailAsync(emailModel);
                }
            }
            return true;
        }
        public async Task<DataTable> ExportPosts()
        {
            var currentUser = _userService.GetCurrentUser();
            var role = currentUser.FindFirst("http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;
            var userId = currentUser.FindFirst("UserId")?.Value;
            var posts = await _context.Posts.Include(x => x.User).Include(x => x.SubCategory).Include(x => x.Category).ToListAsync();
            if (role == UserRole.Writer)
            {
                posts = await _context.Posts.Where(x => x.UserId == userId).Include(x => x.User).Include(x => x.SubCategory).Include(x => x.Category).ToListAsync();
              
            }
            if (role == UserRole.Leader)
            {
                posts = await GetListPostsOfLeader(userId);
            }

            DataTable dt = _excelService.ExportToExcel(posts);
            return dt;
        }
        #region private method
        private async Task<bool> UpdateLikeDelete(Guid postId, bool liked)
        {
            if (liked == null) return false;
            var post = await GetByIdAsync(postId);
            if (post == null)
            {
                return false;
            }
            if (liked)
            {
                post.Like -= 1;
            }
            else
            {
                post.Dislike -= 1;
            }
            bool result = await Update(post);
            return result;
        }
        public async Task<PagedList<PostView>> GetPostByCateId(PostRequestBody requestBody)
        {
            try
            {
                if (requestBody.CategoryId != Guid.Empty)
                {
                    var post = await _context.Posts.Include(x => x.User).Where(x => (x.CategoryId == requestBody.CategoryId || x.SubCategoryId == requestBody.CategoryId) && x.Status == PostStatusModel.Published)
                                    .OrderBy(y => y.PublishedDate)
                                    .Skip((requestBody.PageNumber - 1) * requestBody.PageSize)
                                    .Take(requestBody.PageSize)
                                    .ToListAsync();
                    var number = await _context.Posts.Include(x => x.User).Where(x => (x.CategoryId == requestBody.CategoryId || x.SubCategoryId == requestBody.CategoryId) && x.Status == PostStatusModel.Published).CountAsync();
                    return PagedList<PostView>.ToPagedList(post.MapToList<PostView>(), requestBody.PageNumber, requestBody.PageSize, number);
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw;
            }
        }

        Task<PagedList<Post>> ICoreRepository<Post>.GetAllWithPaging(PageRequestBody requestBody)
        {
            throw new NotImplementedException();
        }
        private async Task<EmailModel> SendEmailForDelete(string emailReceive, string message, Post post)
        {
            var currentUser = _userService.GetCurrentUser();
            var roleCurrentUser = currentUser.FindFirst("http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;
            var nameCurrentUser = currentUser.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value;
            var emailCurrentUser = currentUser.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Value;
            EmailModel result = new EmailModel();
            List<string> emailTos = new List<string>();
            emailTos.Add(emailReceive);
            result.Subject = EmailTemplateSubjectConstant.DeletePostSubject;
            string bodyEmail = string.Format(EmailTemplateBodyConstant.DeletePostBody, roleCurrentUser, nameCurrentUser, post.Title, message, emailCurrentUser);
            result.Body = bodyEmail + EmailTemplateBodyConstant.SignatureFooter;
            result.To = emailTos;
            return await Task.FromResult(result);
        }
        private async Task<EmailModel> SendEmailForPublishPost(string emailReceive, Post post)
        {
            var currentUser = _userService.GetCurrentUser();
            var roleCurrentUser = currentUser.FindFirst("http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;
            var nameCurrentUser = currentUser.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value;
            var emailCurrentUser = currentUser.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Value;
            EmailModel result = new EmailModel();
            List<string> emailTos = new List<string>();
            emailTos.Add(emailReceive);
            result.Subject = EmailTemplateSubjectConstant.PostPublishedSubject;
            string bodyEmail = string.Format(EmailTemplateBodyConstant.PublishPostBody, roleCurrentUser, nameCurrentUser, post.Title, DateTime.Now.ToString("dddd, dd MMMM yyyy"), emailCurrentUser);
            result.Body = bodyEmail + EmailTemplateBodyConstant.SignatureFooter;
            result.To = emailTos;
            return await Task.FromResult(result);
        }

        #endregion
    }
}
