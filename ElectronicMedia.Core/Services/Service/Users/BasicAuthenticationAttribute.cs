using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElectronicMedia.Core.Repository.DataContext;
using ElectronicMedia.Core.Repository.Models;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using ElectronicMedia.Core.Services.Interfaces.Users;

namespace ElectronicMedia.Core.Services.Service.Users
{
    public class BasicAuthenticationAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string requiredRole;

        public BasicAuthenticationAttribute(string requiredRole)
        {
            this.requiredRole = requiredRole;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var authService = context.HttpContext.RequestServices.GetRequiredService<IAuthService>();
            if (!authService.IsAuthorized(context.HttpContext.User, requiredRole))
            {
                context.Result = new StatusCodeResult(403); // Forbidden
            }
        }
    }
}
