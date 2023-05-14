﻿using ElectronicMedia.Core.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicMedia.Core.Services.Interfaces
{
    public interface IUserService
    {
        Task<APIResponeModel> Login(UserLoginModel model);
        Task<APIResponeModel> Register(UserRegisterModel model);
        Task<string> GenerateToken();
    }
}