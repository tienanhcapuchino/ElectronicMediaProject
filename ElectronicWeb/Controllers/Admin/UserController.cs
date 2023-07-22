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

using DocumentFormat.OpenXml.Spreadsheet;
using ElectronicMedia.Core;
using ElectronicMedia.Core.Common;
using ElectronicMedia.Core.Repository.Entity;
using ElectronicMedia.Core.Repository.Models;
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
        public IActionResult UserManager(int currentPage = 1, string search = "")
        {
            var token = _tokenService.GetToken();
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
                PageList<UsersModel> pageRequest = null;
                PageRequestBody pageRequestBody = new PageRequestBody()
                {
                    Page = currentPage,
                    Top = 5,
                    Skip = 0,
                    SearchText = string.IsNullOrEmpty(search) ? string.Empty : search,
                    SearchByColumn = new List<string>() { "Email", "UserName", "FullName" },
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
                if(!string.IsNullOrEmpty(search))
                {
                    ViewBag.SearchText = search;
                }
                string data = JsonConvert.SerializeObject(pageRequestBody);
                HttpResponseMessage respone = CommonUIService.GetDataAPI(RoutesManager.GetUerssWithPaging, MethodAPI.POST, token, data);
                if (respone.IsSuccessStatusCode)
                {
                    var content = respone.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    pageRequest = JsonConvert.DeserializeObject<PageList<UsersModel>>(content);
                    if (TempData["AddUserSuccess"] != null)
                    {
                        ViewBag.AddSuccess = TempData["AddUserSuccess"] as string;
                    }
                    return View(pageRequest);
                }
            }
            return Forbid();
        }

        public IActionResult ChangeRole(Guid userId, string newRole)
        {
            var token = _tokenService.GetToken();
            if (string.IsNullOrEmpty(token))
            {
                return View("Views/Account/Login.cshtml");
            }
            string url = $"{RoutesManager.UpdateRole}/{userId}?newRole={newRole}";
            HttpResponseMessage respone = CommonUIService.GetDataAPI(url, MethodAPI.PUT, token);
            if (respone.IsSuccessStatusCode)
            {
                return RedirectToAction("UserManager");
            }
            return BadRequest();
        }

        public IActionResult DeactiveUser(Guid id, bool isActive)
        {
            var token = _tokenService.GetToken();
            if (string.IsNullOrEmpty(token))
            {
                return View("Views/Account/Login.cshtml");
            }
            string url = $"{RoutesManager.Deactivate}/{id}?isActive={isActive}";
            HttpResponseMessage respone = CommonUIService.GetDataAPI(url, MethodAPI.PUT, token);
            if (respone.IsSuccessStatusCode)
            {
                return RedirectToAction("UserManager");
            }
            return BadRequest();
        }

        public IActionResult AddUser()
        {
            return View("Views/User/Add.cshtml");
        }

        public IActionResult DoAdd(UserAddModel user)
        {
            var token = _tokenService.GetToken();
            if (string.IsNullOrEmpty(token))
            {
                return View("Views/Account/Login.cshtml");
            }
            var jsonData = JsonConvert.SerializeObject(user);
            HttpResponseMessage respone = CommonUIService.GetDataAPI(RoutesManager.AddNewUser, MethodAPI.POST, token, jsonData);
            if (respone.IsSuccessStatusCode)
            {
                TempData["AddUserSuccess"] = "Add successfully";
                return RedirectToAction("UserManager");
            }
            return BadRequest();
        }
        public IActionResult DownloadExcel()
        {
            var token = _tokenService.GetToken();
            if (string.IsNullOrEmpty(token))
            {
                return View("Views/Account/Login.cshtml");
            }
            HttpResponseMessage respone = CommonUIService.GetDataAPI(RoutesManager.ExportUsers, MethodAPI.GET, token);
            if (respone.IsSuccessStatusCode)
            {
                byte[] fileContent = respone.Content.ReadAsByteArrayAsync().GetAwaiter().GetResult();
                string fileName = "Export_Users.xlsx";
                return File(fileContent, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
            return Content("Error when dowload excel!");
        }
    }
}
