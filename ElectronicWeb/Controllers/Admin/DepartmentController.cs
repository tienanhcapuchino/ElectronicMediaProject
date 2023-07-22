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

using ElectronicMedia.Core.Repository.Models;
using ElectronicMedia.Core;
using ElectronicWeb.Models;
using ElectronicWeb.Service;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ElectronicMedia.Core.Common;
using ElectronicWeb.Routes;

namespace ElectronicWeb.Controllers.Admin
{
    public class DepartmentController : Controller
    {
        private readonly ITokenService _tokenService;
        public DepartmentController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public IActionResult Index(int currentPage = 1)
        {
            var token = _tokenService.GetToken();
            if (string.IsNullOrEmpty(token))
            {
                return View("Views/Account/Login.cshtml");
            }
            PageList<DepartmentModel> pageRequest = null;
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
            HttpResponseMessage respone = CommonUIService.GetDataAPI(RoutesManager.GetAllDepartments, MethodAPI.POST, token, data);
            if (respone.IsSuccessStatusCode)
            {
                if (TempData["DeleteSuccess"] != null)
                {
                    ViewBag.DeleteSuccess = TempData["DeleteSuccess"] as string;
                }
                var content = respone.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                pageRequest = JsonConvert.DeserializeObject<PageList<DepartmentModel>>(content);
                return View(pageRequest);
            }
            return Content("Error when get all departments!");
        }

        public IActionResult Delete(Guid depId)
        {
            var token = _tokenService.GetToken();
            if (string.IsNullOrEmpty(token))
            {
                return View("Views/Account/Login.cshtml");
            }
            HttpResponseMessage respone = CommonUIService.GetDataAPI($"{RoutesManager.DeleteDepartment}/{depId}", MethodAPI.DELETE, token);
            if (respone.IsSuccessStatusCode)
            {
                TempData["DeleteSuccess"] = "Delete successfully";
                return RedirectToAction("Index");
            }
            return Content("Error when delete department!");
        }
        public IActionResult Update(Guid depId)
        {
            var token = _tokenService.GetToken();
            if (string.IsNullOrEmpty(token))
            {
                return View("Views/Account/Login.cshtml");
            }
            HttpResponseMessage respone = CommonUIService.GetDataAPI($"{RoutesManager.ViewDetailDepartment}/{depId}", MethodAPI.GET, token);
            if (respone.IsSuccessStatusCode)
            {
                var data = respone.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                DepartmentViewDetail result = JsonConvert.DeserializeObject<DepartmentViewDetail>(data);
                if (result.Members != null && result.Members.Count > 0)
                {
                    var leader = result.Members.Where(x => x.RoleName.ToLower().Equals("leader")).FirstOrDefault();
                    if (leader != null)
                    {
                        ViewBag.ToolTipLeader = "true";
                    }
                }
                if (TempData["KickSuccess"] != null)
                {
                    ViewBag.KickSuccess = TempData["KickSuccess"] as string;
                }
                if (TempData["AssignSuccess"] != null)
                {
                    ViewBag.AssignSuccess = TempData["AssignSuccess"] as string;
                }
                if (TempData["UpdateFail"] != null)
                {
                    ViewBag.UpdateFail = TempData["UpdateFail"] as string;
                }
                if (TempData["UpdateSuccess"] != null)
                {
                    ViewBag.UpdateSuccess = TempData["UpdateSuccess"] as string;
                }
                return View(result);
            }
            return Content($"Error when get detail department at departmentId: {depId}");
        }

        public IActionResult KickMember(string memId, Guid depId)
        {
            var token = _tokenService.GetToken();
            if (string.IsNullOrEmpty(token))
            {
                return View("Views/Account/Login.cshtml");
            }
            string url = RoutesManager.KickMember + "/" + memId + "/" + depId;
            HttpResponseMessage respone = CommonUIService.GetDataAPI(url, MethodAPI.DELETE, token);
            if (respone.IsSuccessStatusCode)
            {
                TempData["KickSuccess"] = "Kick successfully";
                return RedirectToAction("Update", new { depId = depId});
            }
            return Content($"Error when kick member: {memId}");
        }

        public IActionResult AssignLeader(Guid depId)
        {
            var token = _tokenService.GetToken();
            if (string.IsNullOrEmpty(token))
            {
                return View("Views/Account/Login.cshtml");
            }
            HttpResponseMessage responeCheck = CommonUIService.GetDataAPI($"{RoutesManager.ViewDetailDepartment}/{depId}", MethodAPI.GET, token);
            if (responeCheck.IsSuccessStatusCode)
            {
                var data = responeCheck.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                DepartmentViewDetail result = JsonConvert.DeserializeObject<DepartmentViewDetail>(data);
                if (result.Members != null && result.Members.Count > 0)
                {
                    var leader = result.Members.Where(x => x.RoleName.ToLower().Equals("leader")).FirstOrDefault();
                    if (leader != null)
                    {
                        return RedirectToAction("Update", new { depId = depId });
                    }
                }
            }
            HttpResponseMessage respone = CommonUIService.GetDataAPI(RoutesManager.GetLeaders, MethodAPI.GET, token);
            if (respone.IsSuccessStatusCode)
            {
                var data = respone.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                List<MemberModel> result = JsonConvert.DeserializeObject<List<MemberModel>>(data);
                ViewBag.DepId = depId;
                return View(result);
            }
            return Content("Error when get all leaders");
        }
        public IActionResult AssignMember(Guid depId)
        {
            var token = _tokenService.GetToken();
            if (string.IsNullOrEmpty(token))
            {
                return View("Views/Account/Login.cshtml");
            }
            HttpResponseMessage respone = CommonUIService.GetDataAPI(RoutesManager.GetMembers, MethodAPI.GET, token);
            if (respone.IsSuccessStatusCode)
            {
                var data = respone.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                List<MemberModel> result = JsonConvert.DeserializeObject<List<MemberModel>>(data);
                if (TempData["AssignSuccess"] != null)
                {
                    ViewBag.AssignSuccess = TempData["AssignSuccess"] as string;
                }
                ViewBag.DepId = depId;
                return View(result);
            }
            return Content("Error when get all members");
        }

        public IActionResult DoAssign(string memId, Guid depId, string check)
        {
            var token = _tokenService.GetToken();
            if (string.IsNullOrEmpty(token))
            {
                return View("Views/Account/Login.cshtml");
            }
            string url = RoutesManager.AssignMember + "/" + memId + "/" + depId;
            HttpResponseMessage respone = CommonUIService.GetDataAPI(url, MethodAPI.PUT, token);
            if (respone.IsSuccessStatusCode)
            {
                if (check.Equals("true"))
                {
                    TempData["AssignSuccess"] = "Assign leader successfully!";
                    return RedirectToAction("Update", new {depId = depId});
                }
                else
                {
                    TempData["AssignSuccess"] = "Assign member successfully!";
                    return RedirectToAction("AssignMember", new { depId = depId });
                }
            }
            return Content($"Error when assign member: {memId} to department: {depId}");
        }

        public IActionResult DoUpdate(DepartmentModel department)
        {
            var token = _tokenService.GetToken();
            if (string.IsNullOrEmpty(token))
            {
                return View("Views/Account/Login.cshtml");
            }
            string jsonData = JsonConvert.SerializeObject(department);
            HttpResponseMessage respone = CommonUIService.GetDataAPI(RoutesManager.UpdateDepartment, MethodAPI.PUT, token, jsonData);
            if (respone.IsSuccessStatusCode)
            {
                var data = respone.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                APIResponeModel result = JsonConvert.DeserializeObject<APIResponeModel>(data);
                if (result.Code == 200)
                {
                    TempData["UpdateSuccess"] = "Update successfully";
                    return RedirectToAction("Update", new {depId = department.Id});
                }
                else
                {
                    TempData["UpdateFail"] = result.Message;
                    return RedirectToAction("Update", new { depId = department.Id });
                }
            }
            return Content($"error when update department: {department.Name}");
        }

    }
}
