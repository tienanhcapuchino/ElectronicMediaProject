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
        public IActionResult UserManager(string role, int currentPage = 1, string search = "")
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
            if (tokenModel != null && tokenModel.RoleName.Equals(UserRole.Admin))
            {
                PageList<UsersModel> pageRequest = null;
                string realRole = string.IsNullOrEmpty(role) ? string.Empty : role;
                PageRequestBody pageRequestBody = new PageRequestBody()
                {
                    Page = currentPage,
                    Top = 5,
                    Skip = 0,
                    SearchText = string.IsNullOrEmpty(search) ? "" : search,
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
                        ColumnName = "RoleName",
                        IsNullValue = string.IsNullOrEmpty(role) ? true : false,
                        IncludeNullValue= true,
                        Value = new List<string>(){ realRole }
                    },
                },
                    AdditionalFilters = new List<AdditionalFilter>
                {
                    new AdditionalFilter{}
                },
                };
                if (!string.IsNullOrEmpty(search))
                {
                    ViewBag.SearchText = search;
                }
                if (!string.IsNullOrEmpty(role))
                {
                    ViewBag.RoleSearch = role;
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
            var tokenModel = _tokenService.GetTokenModelUI(token);
            if (tokenModel != null && tokenModel.ExpiredTime >= DateTime.Now.Ticks)
            {
                return Unauthorized();
            }
            if (tokenModel != null && tokenModel.RoleName.Equals(UserRole.Admin))
            {
                string url = $"{RoutesManager.UpdateRole}/{userId}?newRole={newRole}";
                HttpResponseMessage respone = CommonUIService.GetDataAPI(url, MethodAPI.PUT, token);
                if (respone.IsSuccessStatusCode)
                {
                    return RedirectToAction("UserManager");
                }
            }
            return Content("Error when change role!");
        }

        public IActionResult DeactiveUser(Guid id, bool isActive)
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
            if (tokenModel != null && tokenModel.RoleName.Equals(UserRole.Admin))
            {
                string url = $"{RoutesManager.Deactivate}/{id}?isActive={isActive}";
                HttpResponseMessage respone = CommonUIService.GetDataAPI(url, MethodAPI.PUT, token);
                if (respone.IsSuccessStatusCode)
                {
                    return RedirectToAction("UserManager");
                }
            }
            return Content("Error when deactive user");
        }

        public IActionResult Add(UserAddModel userModel)
        {
            var token = _tokenService.GetToken();
            if (userModel == null) userModel = new UserAddModel();
            if (string.IsNullOrEmpty(token))
            {
                return View("Views/Account/Login.cshtml");
            }
            var tokenModel = _tokenService.GetTokenModelUI(token);
            if (tokenModel != null && tokenModel.ExpiredTime >= DateTime.Now.Ticks)
            {
                return Unauthorized();
            }
            if (tokenModel != null && tokenModel.RoleName.Equals(UserRole.Admin))
            {
                if (TempData["AddFailed"] != null)
                    ViewBag.ErrorAdd = TempData["AddFailed"] as string;
                return View(userModel);
            }
            return View("Views/Account/Login.cshtml");
        }

        public IActionResult DoAdd(UserAddModel user)
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
            if (tokenModel != null && tokenModel.RoleName.Equals(UserRole.Admin))
            {
                var jsonData = JsonConvert.SerializeObject(user);
                HttpResponseMessage respone = CommonUIService.GetDataAPI(RoutesManager.AddNewUser, MethodAPI.POST, token, jsonData);
                if (respone.IsSuccessStatusCode)
                {
                    var data = respone.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    var resultData = JsonConvert.DeserializeObject<APIResponeModel>(data);
                    if (resultData.Code == 200)
                    {
                        TempData["AddUserSuccess"] = "Add successfully";
                        return RedirectToAction("UserManager");
                    }
                    else
                    {
                        TempData["AddFailed"] = resultData.Message;
                        return RedirectToAction("Add", new {userModel = user});
                    }
                }
            }
            return Content("Error when add user");
        }
        public IActionResult DownloadExcel()
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
            if (tokenModel != null && tokenModel.RoleName.Equals(UserRole.Admin))
            {
                HttpResponseMessage respone = CommonUIService.GetDataAPI(RoutesManager.ExportUsers, MethodAPI.GET, token);
                if (respone.IsSuccessStatusCode)
                {
                    byte[] fileContent = respone.Content.ReadAsByteArrayAsync().GetAwaiter().GetResult();
                    string fileName = "Export_Users.xlsx";
                    return File(fileContent, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                }
            }
            return Content("Error when dowload excel!");
        }

        public IActionResult UserProfile()
        {
            var token = _tokenService.GetToken();
            if (string.IsNullOrEmpty(token))
            {
                return View("Views/Account/Login.cshtml");
            }
            var userId = Guid.Parse(_tokenService.GetTokenModelUI(token).UserId);
            HttpResponseMessage respone = CommonUIService.GetDataAPI($"{RoutesManager.GetUserProfile}/{userId}", MethodAPI.GET, token);
            if (respone.IsSuccessStatusCode)
            {
                var data = respone.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                UserProfileModel userProfile = JsonConvert.DeserializeObject<UserProfileModel>(data);
                if (TempData["UpdateFailed"] != null)
                {
                    ViewBag.UpdateFailed = TempData["UpdateFailed"] as string;
                }
                if (TempData["UpdateSuccess"] != null)
                {
                    ViewBag.UpdateSuccess = TempData["UpdateSuccess"] as string;
                }
                return View(userProfile);
            }
            return Content($"Error when get user profile: {userId}");
        }

        //public IActionResult UpdateProfile(UserProfileModel userProfile, IFormFile formFile)
        //{
        //    var token = _tokenService.GetToken();
        //    if (string.IsNullOrEmpty(token))
        //    {
        //        return View("Views/Account/Login.cshtml");
        //    }
        //    byte[] fileBytes;
        //    using (var formData = new MultipartFormDataContent())
        //    {
        //        using (var memoryStream = new MemoryStream())
        //        {
        //            formFile.CopyToAsync(memoryStream).GetAwaiter().GetResult();
        //            fileBytes = memoryStream.ToArray();
        //        }
        //        var fileContent = new ByteArrayContent(fileBytes);
        //        fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
        //        {
        //            Name = "file",
        //            FileName = formFile.FileName // Tên tập tin, có thể chỉnh sửa nếu cần thiết
        //        };
        //        HttpClient httpClient = new HttpClient();
        //        formData.Add(fileContent);
        //        HttpResponseMessage saveImage = httpClient.PostAsync(RoutesManager.SaveImage, formData).GetAwaiter().GetResult();
        //        if (saveImage.IsSuccessStatusCode)
        //        {
        //            var data = saveImage.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        //            userProfile.Image = data;
        //        }
        //    }
        //    var jsonData = JsonConvert.SerializeObject(userProfile);
        //    HttpResponseMessage respone = CommonUIService.GetDataAPI(RoutesManager.UpdateUserProfile, MethodAPI.POST, token, jsonData);
        //    if (respone.IsSuccessStatusCode)
        //    {
        //        var dataRespone = respone.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        //        APIResponeModel result = JsonConvert.DeserializeObject<APIResponeModel>(dataRespone);
        //        if (result.Code == 200)
        //        {
        //            TempData["UpdateSuccess"] = "Updated your profile!";
        //            return RedirectToAction("UserProfile");
        //        }
        //        else
        //        {
        //            TempData["UpdateFailed"] = result.Message;
        //            return RedirectToAction("UserProfile");
        //        }
        //    }
        //    return Content($"Error when update user profile: {userProfile.Id}");
        //}
        public IActionResult PasswordChange()
        {
            var token = _tokenService.GetToken();
            if (string.IsNullOrEmpty(token))
            {
                return View("Views/Account/Login.cshtml");
            }
            if (TempData["ChangeSuccess"] != null)
            {
                ViewBag.ChangeSuccess = TempData["ChangeSuccess"] as string;
            }
            if (TempData["ChangeFail"] != null)
            {
                ViewBag.ChangeFail = TempData["ChangeFail"] as string;
            }
            return View();
        }
        public IActionResult DoChangePass(ChangePassModel passwordModel)
        {
            var token = _tokenService.GetToken();
            if (string.IsNullOrEmpty(token))
            {
                return View("Views/Account/Login.cshtml");
            }
            var userId = Guid.Parse(_tokenService.GetTokenModelUI(token).UserId);
            passwordModel.UserId = userId.ToString();
            string jsonData = JsonConvert.SerializeObject(passwordModel);
            HttpResponseMessage respone = CommonUIService.GetDataAPI(RoutesManager.ChangePass, MethodAPI.POST, token, jsonData);
            if (respone.IsSuccessStatusCode)
            {
                var data = respone.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                var result = JsonConvert.DeserializeObject<APIResponeModel>(data);
                if (result.Code == 200)
                {
                    TempData["ChangeSuccess"] = "Change password successfully";
                }
                else
                {
                    TempData["ChangeFail"] = result.Message;
                }
                return RedirectToAction("PasswordChange");
            }
            return Content("Error when change password");
        }
    }
}
