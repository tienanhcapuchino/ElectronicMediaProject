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
