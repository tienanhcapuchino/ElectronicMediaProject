using ElectronicMedia.Core.Automaper;
using ElectronicMedia.Core.Repository.DataContext;
using ElectronicMedia.Core.Repository.Entity;
using ElectronicMedia.Core.Repository.Models;
using ElectronicMedia.Core.Services.Interfaces;
using Konscious.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ElectronicMedia.Core.Services.Service
{
    public class UserService : IUserService
    {
        const int memorySize = 1024;
        const int iterations = 10;
        private readonly ElectronicMediaDbContext _context;
        private readonly AppSetting _appSettings;
        public UserService(ElectronicMediaDbContext context, IOptionsMonitor<AppSetting> optionsMonitor)
        {
            _context = context;
            _appSettings = optionsMonitor.CurrentValue;
        }

        public async Task<string> GenerateToken(User us)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var secretKeyBytes = Encoding.UTF8.GetBytes(_appSettings.SecretKey);
            var tokenDecription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]{
                new Claim(ClaimTypes.Name, us.Username),
                new Claim(ClaimTypes.Role, us.Role.ToString()),
                new Claim(ClaimTypes.Email, us.Email),
                new Claim("UserId", us.Id.ToString()),
                new Claim("TokenId", Guid.NewGuid().ToString())}),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha512Signature)
            };
            var token = jwtTokenHandler.CreateToken(tokenDecription);
            string accessToken = jwtTokenHandler.WriteToken(token);
            var userToken = new UserToken()
            {
                UserId = us.Id,
                IsRevoked = false,
                IsUsed = false,
                IssuedAt = DateTime.UtcNow,
                ExpiredAt = DateTime.UtcNow.AddMinutes(30),
                AccessToken = accessToken,
                RefreshToken = GenerateRefreshToken(),
                JwtId = token.Id,
            };
            await _context.AddAsync(userToken);
            await _context.SaveChangesAsync();
            return accessToken;
        }

        public async Task<APIResponeModel> Login(UserLoginModel model)
        {
            var result = new APIResponeModel();
            var userLogin = await _context.Users.SingleOrDefaultAsync(x => x.Username == model.Username
            || x.Email == model.Username);
            if (userLogin == null)
            {
                result.Code = 404;
                result.Message = "Username or password is not correct!";
                result.IsSucceed = false;
            }
            else
            {
                if (!VerifyPassword(model.Password, userLogin.Password))
                {
                    return new APIResponeModel()
                    {
                        Code = 404,
                        Message = "Username or password is not correct!",
                        IsSucceed = false
                    };
                }
                result.Code = 200;
                result.Message = "Login successfully!";
                result.IsSucceed = true;
                await GenerateToken(userLogin);
            }
            return result;
        }

        public async Task<APIResponeModel> Register(UserRegisterModel model)
        {
            var result = new APIResponeModel();
            result = IsValidUserRegister(model);
            if (!result.IsSucceed)
            {
                return result;
            }
            var user = model.MapTo<User>();
            if (!await Add(user))
            {
                return new APIResponeModel()
                {
                    Code = 400,
                    Message = "Add user to database failed",
                    IsSucceed = false
                };
            }
            return result;
        }
        public async Task<APIResponeModel> RenewToken(TokenModel model)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKeyBytes = Encoding.UTF8.GetBytes(_appSettings.SecretKey);
            var tokenValidateParam = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),
                ClockSkew = TimeSpan.Zero,
                ValidateLifetime = false, //không kiểm tra token hết hạn
            };
            try
            {
                // Check 1: AccessToken valid format
                var tokenInVerification = jwtTokenHandler.ValidateToken(model.AccessToken, tokenValidateParam, out var validatedToken);

                // Check 2: Check alg
                if (validatedToken is JwtSecurityToken jwtSecurityToken)
                {
                    var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512, StringComparison.InvariantCultureIgnoreCase);
                    if (!result)
                    {
                        return new APIResponeModel
                        {
                            IsSucceed = false,
                            Message = "Invalid Token",
                        };
                    }
                }

                // Check 3: Check access expire
                var UtcExpireDate = long.Parse(tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
                var expireDate = ConverUnixTimeToDateTime(UtcExpireDate);
                if (expireDate > DateTime.UtcNow)
                {
                    return new APIResponeModel
                    {
                        IsSucceed = false,
                        Message = "Access token has not yet expired",
                    };
                }

                // Check 4: Check refresh token exist in db
                var storedToken = _context.UserTokens.FirstOrDefault(x => x.RefreshToken == model.RefreshToken);
                if (storedToken == null)
                {
                    return new APIResponeModel
                    {
                        IsSucceed = false,
                        Message = "Refresh token does not exist"
                    };
                }

                // Check 5: Check refresh token isUsed/revoked
                if (storedToken.IsUsed)
                {
                    return new APIResponeModel
                    {
                        IsSucceed = false,
                        Message = "Refresh token has been used"
                    };
                }
                if (storedToken.IsRevoked)
                {
                    return new APIResponeModel
                    {
                        IsSucceed = false,
                        Message = "Refresh token has been revoked"
                    };
                }

                // Check 6: Check refresh token isUsed/revoked
                var jti = tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
                if (storedToken.JwtId != jti)
                {
                    return new APIResponeModel
                    {
                        IsSucceed = false,
                        Message = "Token doesn't match"
                    };
                }

                // Update token is used
                storedToken.IsUsed = true;
                storedToken.IsRevoked = true;
                _context.Update(storedToken);
                await _context.SaveChangesAsync();

                //Create new token
                var user = await GetByIdAsync(storedToken.UserId);
                var token = await GenerateToken(user);

                return new APIResponeModel
                {
                    IsSucceed = true,
                    Message = "Renew token Success",
                    Data = token
                };
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message);
                return new APIResponeModel
                {
                    IsSucceed = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<User> GetByIdAsync(Guid id)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Id == id);
            return await Task.FromResult(user);
        }

        public Task<List<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Guid id, User entity)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Add(User entity)
        {
            await _context.Users.AddAsync(entity);
            bool result = await _context.SaveChangesAsync() > 0;
            return result;
        }
        #region private method
        private DateTime ConverUnixTimeToDateTime(long utcExpireDate)
        {
            var dateTImeInterval = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTImeInterval.AddSeconds(utcExpireDate).ToUniversalTime();
            return dateTImeInterval;
        }
        private string GenerateRefreshToken()
        {
            var random = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(random);
                return Convert.ToBase64String(random);
            }
        }
        private bool VerifyPassword(string password, string hashedPassword)
        {
            var saltPlusHash = Convert.FromBase64String(hashedPassword);
            var salt = new byte[16];
            var hash = new byte[saltPlusHash.Length - 16];
            Buffer.BlockCopy(saltPlusHash, 0, salt, 0, salt.Length);
            Buffer.BlockCopy(saltPlusHash, salt.Length, hash, 0, hash.Length);

            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
            {
                Salt = salt,
                DegreeOfParallelism = 4,
                Iterations = iterations,
                MemorySize = memorySize
            };
            var computedHash = argon2.GetBytes(16);

            for (int i = 0; i < hash.Length; i++)
            {
                if (computedHash[i] != hash[i])
                {
                    Array.Clear(salt, 0, salt.Length);
                    Array.Clear(argon2.GetBytes(memorySize), 0, argon2.GetBytes(memorySize).Length);
                    return false;
                }
            }
            Array.Clear(salt, 0, salt.Length);
            Array.Clear(argon2.GetBytes(memorySize), 0, argon2.GetBytes(memorySize).Length);
            return true;
        }
        private APIResponeModel IsValidUserRegister(UserRegisterModel model)
        {
            APIResponeModel isValid = new APIResponeModel()
            {
                Code = 200,
                Message = "OK",
                IsSucceed = true,
            };
            string regexPassword = @"^(?=.*[a-zA-Z])(?=.*\d).{8,}$";
            string regexEmail = @"^[^\s@]+@[^\s@]+\.[^\s@]+$";
            string regexPhone = @"^0\d{9}$";
            if (model == null)
            {
                return new APIResponeModel()
                {
                    Code = 400,
                    Message = "cannot leave model is empty!",
                    IsSucceed = false,
                };
            }

            var userEntity = _context.Users.SingleOrDefault(x => x.Username == model.Username
                                                            || x.Email == model.Email
                                                            || x.PhoneNumber == model.PhoneNumber);
            if (userEntity != null)
            {
                return new APIResponeModel()
                {
                    Code = 400,
                    Message = "Account is already exitsed",
                    IsSucceed = false,
                };
            }

            if (!model.Password.Equals(model.Repassword))
            {
                return new APIResponeModel()
                {
                    Code = 404,
                    Message = "Password is not match with repassword!",
                    IsSucceed = false,
                };
            }
            if (!Regex.IsMatch(model.Password, regexPassword))
            {
                return new APIResponeModel()
                {
                    Code = 400,
                    Message = "Password must contain both number and character, at least 8 character!",
                    IsSucceed = false
                };
            }
            if (!Regex.IsMatch(model.Email, regexEmail))
            {
                return new APIResponeModel() { Code = 400, Message = "Email is invalid!", IsSucceed = false };
            }
            if (!Regex.IsMatch(model.PhoneNumber, regexPhone))
            {
                return new APIResponeModel() { Code = 400, Message = "Phone number must is 10 number and start with 0!", IsSucceed = false };
            }
            return isValid;
        }
        #endregion
    }
}
