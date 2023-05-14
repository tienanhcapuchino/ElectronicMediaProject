using ElectronicMedia.Core.Repository.DataContext;
using ElectronicMedia.Core.Repository.Entity;
using ElectronicMedia.Core.Repository.Models;
using ElectronicMedia.Core.Services.Interfaces;
using Konscious.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public UserService(ElectronicMediaDbContext context)
        {
            _context = context;
        }

        public Task<string> GenerateToken()
        {
            throw new NotImplementedException();
        }

        public async Task<APIResponeModel> Login(UserLoginModel model)
        {
            var result = new APIResponeModel();
            var userLogin = await _context.Users.SingleOrDefaultAsync(x => (x.Username == model.Username 
            || x.Email == model.Username) 
            && VerifyPassword(model.Password, x.Password));
            if (userLogin == null)
            {
                result.Code = 404;
                result.Message = "Username or password is not correct!";
                result.IsSucceed = false;
            }
            else
            {
                result.Code = 200;
                result.Message = "Login successfully!";
                result.IsSucceed = true;
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
            User user = new User()
            {
                FullName = model.Username,
                Email = model.Email,
                Username = model.Username,
                Password = EncodePassword(model.Password),
                Dob = model.Dob,
                Role = RoleType.UserNormal,
                Gender = model.Gender,
                IsActived = true,
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return result;
        }
        #region private method
        private string EncodePassword(string password)
        {
            var salt = new byte[16];
            using (var rng = new System.Security.Cryptography.RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }

            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
            {
                Salt = salt,
                DegreeOfParallelism = 4,
                Iterations = iterations,
                MemorySize = memorySize
            };
            var hash = argon2.GetBytes(16);
            var saltPlusHash = new byte[16 + hash.Length];
            Buffer.BlockCopy(salt, 0, saltPlusHash, 0, salt.Length);
            Buffer.BlockCopy(hash, 0, saltPlusHash, salt.Length, hash.Length);
            Array.Clear(salt, 0, salt.Length);
            Array.Clear(argon2.GetBytes(memorySize), 0, argon2.GetBytes(memorySize).Length);
            return Convert.ToBase64String(saltPlusHash);
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
        private APIResponeModel IsValidUserRegister(UserRegisterModel model) {
            APIResponeModel isValid = new APIResponeModel()
            {
                Code = 200,
                Message = "OK",
                IsSucceed = true,
            };
            string regexPassword = @"^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{8,}$\r\n";
            string regexEmail = @"^[^\s@]+@[^\s@]+\.[^\s@]+$";
            string regexPhone = @"^0\d{9}$";
            if (model == null)
            {
                isValid = new APIResponeModel()
                {
                    Code = 400,
                    Message = "cannot leave model is empty!",
                    IsSucceed = false,
                };
            }

            if (!model.Password.Equals(model.Repassword))
            {
                isValid = new APIResponeModel()
                {
                    Code = 404,
                    Message = "Password is not match with repassword!",
                    IsSucceed = false,
                };
            }
            if (!Regex.IsMatch(model.Password, regexPassword))
            {
                isValid = new APIResponeModel()
                {
                    Code = 400,
                    Message = "Password must contain both number and character, at least 8 character!",
                    IsSucceed = false,
                };
            }
            if (!Regex.IsMatch(model.Email, regexEmail))
            {
                isValid = new APIResponeModel()
                {
                    Code = 400,
                    Message = "Email is invalid!",
                    IsSucceed = false,
                };
            }
            if (!Regex.IsMatch(model.PhoneNumber, regexPhone))
            {
                isValid = new APIResponeModel()
                {
                    Code = 400,
                    Message = "Phone number must is 10 number and start with 0!",
                    IsSucceed = false,
                };
            }
            return isValid;
        }
        #endregion
    }
}
