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

using ElectronicMedia.Core.Common;
using ElectronicMedia.Core.Repository.Entity;
using ElectronicMedia.Core.Repository.Models;
using ElectronicWeb.Routes;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ElectronicWeb.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult ForgotPassword(string email)
        {
            if(!string.IsNullOrEmpty(email))
            {
                string data = JsonConvert.SerializeObject(email);
                var result  = CommonUIService.GetDataAPI(RoutesManager.ResetPassword, MethodAPI.POST,null,data);
                if (result.IsSuccessStatusCode)
                {
                    ViewBag.Status = 200;
                    ViewBag.Message = "Your password has been reset successfully!. Please check your email.";
                   
                }
                else
                {
                    ViewBag.Status = 400;
                    ViewBag.Message = "Your password has been reset failed!. Please input correct email.";
                }
                return View();

            }
            return View();
        }
    }
}
