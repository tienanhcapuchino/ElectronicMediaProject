using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicMedia.Core.Services.Interfaces.Users
{
    public interface IAuthService
    {
        bool IsAuthorized(ClaimsPrincipal user, string requiredRole);
    }
}
