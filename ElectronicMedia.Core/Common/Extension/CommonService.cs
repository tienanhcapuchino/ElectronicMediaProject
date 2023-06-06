﻿using Konscious.Security.Cryptography;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicMedia.Core.Common.Extension
{
    public static class CommonService
    {
        public static int memorySize = 1024;
        public static int iterations = 10;
        public static string EncodePassword(string password)
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
        public static string ConvertFileToURL(IFormFile file)
        {
            string urlBase = "";
            if (file.ContentType.Equals("image/jpeg"))
            {
                urlBase = "data:image/jpeg;base64,";
            }
            if (file.ContentType.Equals("image/png"))
            {
                urlBase = "data:image/png;base64,";
            }
            if (string.IsNullOrEmpty(urlBase))
            {
                throw new Exception("We only support jpeg and png for upload image!");
            }
            if (file != null && file.Length > 0)
            {
                byte[] imageData = null;
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    imageData = ms.ToArray();
                    var image = Convert.ToBase64String(imageData);
                    return urlBase + image;
                }
            }
            return null;
        }
        public static bool VerifyPassword(string password, string hashedPassword)
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
    }
}