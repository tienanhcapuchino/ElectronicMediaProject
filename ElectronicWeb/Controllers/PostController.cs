using ElectronicMedia.Core;
using ElectronicMedia.Core.Common;
using ElectronicMedia.Core.Repository.Entity;
using ElectronicMedia.Core.Services.Interfaces;
using ElectronicWeb.Models;
using ElectronicWeb.Routes;
using ElectronicWeb.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

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
