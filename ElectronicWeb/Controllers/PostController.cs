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
using ElectronicMedia.Core.Common;
using ElectronicMedia.Core.Repository.Entity;
using ElectronicWeb.Models;
using ElectronicWeb.Routes;
using ElectronicWeb.Service;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ElectronicWeb.Controllers
{
    public class PostController : Controller
    {

        private readonly ITokenService _tokenService;
        public PostController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }
        public IActionResult Index(int currentPage = 1)
        {
            PageList<Post> pageRequest = null;
            PageRequestBody pageRequestBody = new PageRequestBody()
            {
                Page = currentPage,
                Top = 10,
                Skip = 0,
                SearchText = string.Empty,
                SearchByColumn = new List<string>() { },
                OrderBy = new PageRequestOrderBy()
                {
                    OrderByDesc = true,
                    OrderByKeyWord = string.Empty,
                },
                Filter = new List<PageRequestFilter>
                {
                    new PageRequestFilter
                    {
                        ColumnName = "",
                        IsNullValue = true,
                        IncludeNullValue= true,
                        Value = new List<string>()
                    }

                },
                AdditionalFilters = new List<AdditionalFilter>
                {
                    new AdditionalFilter{}
                   
                },

            };

            string data = JsonConvert.SerializeObject(pageRequestBody);
            string token = _tokenService.GetToken();

            var result = CommonUIService.GetDataAPI(RoutesManager.GetPostsWithPaging, MethodAPI.POST,token, data);
            if (result.IsSuccessStatusCode)
            {
                var content = result.Content.ReadAsStringAsync().Result;
                pageRequest = JsonConvert.DeserializeObject<PageList<Post>>(content);              
            }
            return View(pageRequest);
        }

        [HttpGet]
        public IActionResult Update(string pid)
        {
            string url = RoutesManager.GetPostById +""+ pid;
            string token = _tokenService.GetToken();
            PostVM post = null;
            var result = CommonUIService.GetDataAPI(url, MethodAPI.GET, token);
            if (result.IsSuccessStatusCode)
            {
                var content = result.Content.ReadAsStringAsync().Result;
                post = JsonConvert.DeserializeObject<PostVM>(content);
            }
            return View(post);
        }
        [HttpPost]
        public IActionResult Delete(PostVM post)
        {
            return View();
        }
    }
}
