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


using ElectronicWeb.Models;
using System.IdentityModel.Tokens.Jwt;

namespace ElectronicWeb.Service
{
    public class TokenService : ITokenService
    {
        private IHttpContextAccessor _contextAccessor;
        public TokenService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
        public string GetToken()
        {
            var token = _contextAccessor.HttpContext.Request.Cookies["token"];
            return token;
        }

        public async Task<TokenOutputModel> GetTokenModel()
        {
            var token = GetToken();
            var tokenHandler = new JwtSecurityTokenHandler();
            if (!string.IsNullOrEmpty(token))
            {
                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
                if (jwtToken != null)
                {
                    var claims = jwtToken.Claims.ToList();
                    var expTime = claims.Where(c => c.Type.Equals("exp")).FirstOrDefault().Value;
                    var roleClaim = claims.Where(c => c.Type.Equals("role")).FirstOrDefault().Value;
                    var claimMail = claims.Where(c => c.Type.Equals("email")).FirstOrDefault().Value;
                    var claimUsername = claims.Where(c => c.Type.Equals("unique_name")).FirstOrDefault().Value;
                    long expDate = long.Parse(expTime);
                    TokenOutputModel result = new TokenOutputModel()
                    {
                        Email = claimMail,
                        ExpiredTime = expDate,
                        RoleName = roleClaim,
                        Username = claimUsername,
                    };

                    var userIdClaims = claims.Where(c => c.Type.Equals("UserId")).FirstOrDefault();
                    if (userIdClaims != null)
                    {
                        result.UserId = userIdClaims.Value;
                    }
                    return await Task.FromResult(result);
                }
            }
            return null;
        }
    }
}
