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

    }
}
