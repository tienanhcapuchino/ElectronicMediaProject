using ElectronicMedia.Core.Services.Interfaces.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicMedia.Core.Services.Service.Users
{
    public class AuthService : IAuthService
    {
        public bool IsAuthorized(ClaimsPrincipal user, string requiredRole)
        {
            // Implement your role-based authorization logic here
            // Example: Check if the user has the required role
            bool hasRequiredRole = user.IsInRole(requiredRole);
            return hasRequiredRole;
        }
    }
}
