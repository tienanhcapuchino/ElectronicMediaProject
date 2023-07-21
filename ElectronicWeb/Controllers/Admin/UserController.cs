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

namespace ElectronicWeb.Controllers.Admin
{
    public class UserController : Controller
    {
        private readonly ITokenService _tokenService;
        public UserController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }
        public IActionResult UserManager(int currentPage = 1)
        {
            var token = _tokenService.GetToken();
            Request.Cookies.TryGetValue("user", out string tokenCookieModel);
            if (string.IsNullOrEmpty(token))
            {
                return View("Views/Account/Login.cshtml");
            }
            var tokenModel = _tokenService.GetTokenModelUI(token);
            if (tokenModel != null && tokenModel.ExpiredTime >= DateTime.Now.Ticks)
            {
                return Unauthorized();
            }
            if (tokenModel != null && tokenModel.RoleName.Equals("Admin"))
            {
                PageList<UserIdentity> pageRequest = null;
                PageRequestBody pageRequestBody = new PageRequestBody()
                {
                    Page = currentPage,
                    Top = 5,
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
                HttpResponseMessage respone = CommonUIService.GetDataAPI(RoutesManager.GetUerssWithPaging, MethodAPI.POST, token, data);
                if (respone.IsSuccessStatusCode)
                {
                    var content = respone.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    pageRequest = JsonConvert.DeserializeObject<PageList<UserIdentity>>(content);
                    return View(pageRequest);
                }
            }
            return Forbid();
        }
    }
}
