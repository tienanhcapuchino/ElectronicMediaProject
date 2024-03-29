﻿/*********************************************************************
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
using ElectronicMedia.Core.Common;
using ElectronicMedia.Core.Repository.Entity;
using ElectronicMedia.Core.Repository.Models;
using ElectronicMedia.Core.Services.Interfaces;
using ElectronicWeb.Models;
using ElectronicWeb.Routes;
using ElectronicWeb.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Security.Policy;

namespace ElectronicWeb.Controllers
{
    public class PostController : Controller
    {

        private readonly ITokenService _tokenService;

        public PostController(ITokenService tokenService)
        {
            _tokenService = tokenService;

        }
        public IActionResult Add()
        {
            return View();
        }
        public IActionResult Index(int currentPage = 1, string text = "", string status = "All")
        {
            string token = _tokenService.GetToken();
            var user = _tokenService.GetTokenModelUI(token);
            if (string.IsNullOrEmpty(token))
            {
                return View("Views/Account/Login.cshtml");
            }
            if(user.RoleName == UserRole.NormalUser)
            {
                return View("Views/Home/Index.cshtml");
            }
            PageList<PostViewModel> pageRequest = null;

            PageRequestBody pageRequestBody = new PageRequestBody()
            {
                Page = currentPage,
                Top = 10,
                Skip = 0,
                SearchText = (string.IsNullOrEmpty(text) ? string.Empty : text),
                SearchByColumn = new List<string>() { "Title", "Content" },
                OrderBy = new PageRequestOrderBy()
                {
                    OrderByDesc = true,
                    OrderByKeyWord = string.Empty,
                },
                Filter = new List<PageRequestFilter>
                {
                    new PageRequestFilter
                    {
                        ColumnName = "Status",
                        IsNullValue = (status=="All"?true:false),
                        IncludeNullValue= true,
                        Value = new List<string>(){status}

                    }
                },
                AdditionalFilters = new List<AdditionalFilter>
                {
                    new AdditionalFilter{}

                },

            };
            string data = JsonConvert.SerializeObject(pageRequestBody);       
           
            var result = CommonUIService.GetDataAPI(RoutesManager.GetPostsWithPaging, MethodAPI.POST, token, data);

            if (result.IsSuccessStatusCode)
            {
                var content = result.Content.ReadAsStringAsync().Result;
                pageRequest = JsonConvert.DeserializeObject<PageList<PostViewModel>>(content);
            }
            else
            {
                return View("Views/Account/Login.cshtml");
            }
            ViewBag.User = user;
            ViewBag.Text = text;
            ViewBag.SelectedStatus = status;
            return View(pageRequest);
        }

        [HttpGet]
        public IActionResult Update(string pid, int currentPage)
        {
            string token = _tokenService.GetToken();
            var user = _tokenService.GetTokenModelUI(token);
            if (string.IsNullOrEmpty(token))
            {
                return View("Views/Account/Login.cshtml");
            }
            if (user.RoleName == UserRole.NormalUser)
            {
                return View("Views/Home/Index.cshtml");
            }
          
            if (string.IsNullOrEmpty(token))
            {
                return View("Views/Account/Login.cshtml");
            }
            string url = RoutesManager.GetPostById + "" + pid;
            PostViewModel post = null;
            List<PostCategory> categories = null;
            List<PostCategory> subCategories = null;
            var getPost = CommonUIService.GetDataAPI(url, MethodAPI.GET, token);
            var getCategories = CommonUIService.GetDataAPI(RoutesManager.GetAllCategory, MethodAPI.GET, token);
            var getsubCategories = CommonUIService.GetDataAPI(RoutesManager.GetAllSubCategory, MethodAPI.GET, token);
            if (getPost.IsSuccessStatusCode)
            {
                var content = getPost.Content.ReadAsStringAsync().Result;
                post = JsonConvert.DeserializeObject<PostViewModel>(content);
            }

            if (getCategories.IsSuccessStatusCode)
            {
                var content = getCategories.Content.ReadAsStringAsync().Result;
                categories = JsonConvert.DeserializeObject<List<PostCategory>>(content);
            }

            if (getsubCategories.IsSuccessStatusCode)
            {
                var content = getsubCategories.Content.ReadAsStringAsync().Result;
                subCategories = JsonConvert.DeserializeObject<List<PostCategory>>(content);
            }

            TempData["CurrentPage"] = currentPage;
            ViewBag.Category = categories;
            ViewBag.User = user;
            ViewBag.SubCategory = subCategories;
            return View(post);
        }


        [HttpPost]
        public IActionResult Update(PostViewModel post)
        {
            try
            {
                string token = _tokenService.GetToken();
                var user = _tokenService.GetTokenModelUI(token);
                if (string.IsNullOrEmpty(token))
                {
                    return View("Views/Account/Login.cshtml");
                }
                if (user.RoleName == UserRole.NormalUser)
                {
                    return View("Views/Home/Index.cshtml");
                }
                post.UpdatedDate = DateTime.Now;
                if (!string.IsNullOrEmpty(post.Title) && !string.IsNullOrEmpty(post.Content) && ! string.IsNullOrEmpty(post.Description))
                {
                    string data = JsonConvert.SerializeObject(post);
                    var result = CommonUIService.GetDataAPI(RoutesManager.UpdatePost, MethodAPI.POST, token, data);
                    if (result.IsSuccessStatusCode)
                    {
                        int page = int.Parse(TempData["CurrentPage"].ToString());
                        var content = result.Content.ReadAsStringAsync().Result;
                        return Redirect("/Post?currentPage=" + page);
                    }
                    else
                    {
                        TempData["message"] = "Error when update post!";
                        return Redirect("/Post/Update?pid=" + post.Id);
                    }
                }
                else
                {
                    TempData["message"] = "Title and Content,Description must be not empty!";
                    return Redirect("/Post/Update?pid=" + post.Id);
                }
            }
            catch (Exception ex)
            {

                TempData["message"] = ex.Message;
                return Redirect("/Post/Update?pid=" + post.Id);
            }
        }

        public IActionResult Detail(string pid)
        {
            string token = _tokenService.GetToken();
            var user = _tokenService.GetTokenModelUI(token);
            if (string.IsNullOrEmpty(token))
            {
                return View("Views/Account/Login.cshtml");
            }
            if (user.RoleName == UserRole.NormalUser)
            {
                return View("Views/Home/Index.cshtml");
            }
            string url = RoutesManager.GetPostById + "" + pid;

            var getPost = CommonUIService.GetDataAPI(url, MethodAPI.GET, token);
            PostViewModel post = null;
            if (getPost.IsSuccessStatusCode)
            {
                var content = getPost.Content.ReadAsStringAsync().Result;
                post = JsonConvert.DeserializeObject<PostViewModel>(content);
            }
            return View(post);
        }
        [HttpGet]
        public IActionResult Delete(string pid)
        {
            string token = _tokenService.GetToken();
            var user = _tokenService.GetTokenModelUI(token);
            if (string.IsNullOrEmpty(token))
            {
                return View("Views/Account/Login.cshtml");
            }
            if (user.RoleName == UserRole.NormalUser || user.RoleName == UserRole.Leader)
            {
                return View("Views/Home/Index.cshtml");
            }
            string url = RoutesManager.GetPostById + "" + pid;

            var getPost = CommonUIService.GetDataAPI(url, MethodAPI.GET, token);
            PostViewModel post = null;
            if (getPost.IsSuccessStatusCode)
            {
                var content = getPost.Content.ReadAsStringAsync().Result;
                post = JsonConvert.DeserializeObject<PostViewModel>(content);
            }
            ViewBag.user = user;
            return View(post);
        }

        [HttpPost]
        public IActionResult Delete(string postId, string message = "No Reasons.")
        {
            string token = _tokenService.GetToken();
            var user = _tokenService.GetTokenModelUI(token);
            string url = RoutesManager.DeletePost + postId;
            string data = JsonConvert.SerializeObject(message);
            var deletePost = CommonUIService.GetDataAPI(url, MethodAPI.POST, token,data);
            if(deletePost.IsSuccessStatusCode)
            {
                ViewBag.Message = "Delete Post Successful!";

            }
            ViewBag.user = user;
            return View();
        }
        public IActionResult DownloadExcel()
        {
            string token = _tokenService.GetToken();
            var user = _tokenService.GetTokenModelUI(token);
            if (string.IsNullOrEmpty(token))
            {
                return View("Views/Account/Login.cshtml");
            }
            if (user.RoleName == UserRole.NormalUser)
            {
                return View("Views/Home/Index.cshtml");
            }
            HttpResponseMessage respone = CommonUIService.GetDataAPI(RoutesManager.ExportPosts, MethodAPI.GET, token);
            if (respone.IsSuccessStatusCode)
            {
                byte[] fileContent = respone.Content.ReadAsByteArrayAsync().GetAwaiter().GetResult();
                string fileName = "Export_Posts.xlsx";
                return File(fileContent, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
            return Content("Error when dowload excel!");
        }

        public IActionResult CategoryPost(string id)
        {
            return View();
        }

    }
}

