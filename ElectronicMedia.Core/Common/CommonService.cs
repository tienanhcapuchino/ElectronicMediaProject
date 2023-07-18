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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicMedia.Core.Common
{
    public static class CommonService
    {
        public static HttpResponseMessage GetDataAPI(string url, MethodAPI method, string token, string? jsonData = null)
        {
            HttpClient client = new HttpClient();
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            HttpResponseMessage respone = new HttpResponseMessage();
            if (method == MethodAPI.GET)
            {
                respone = client.GetAsync(url).GetAwaiter().GetResult();
            }
            else if (method == MethodAPI.DELETE)
            {
                respone = client.DeleteAsync(url).GetAwaiter().GetResult();
            }
            else if (!string.IsNullOrEmpty(jsonData) && method == MethodAPI.POST)
            {
                HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                respone = client.PostAsync(url, content).GetAwaiter().GetResult();
            }
            else if (!string.IsNullOrEmpty(jsonData) && method == MethodAPI.PUT)
            {
                HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                respone = client.PutAsync(url, content).GetAwaiter().GetResult();
            }
            return respone;
        }
    }
}
