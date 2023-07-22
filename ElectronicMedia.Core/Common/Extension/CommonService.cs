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

using Konscious.Security.Cryptography;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicMedia.Core.Common.Extension
{
    public static class CommonService
    {
        public static int memorySize = 1024;
        public static int iterations = 10; 
        private static Random rng = new Random();

        public static byte[] InitAvatarUser()
        {
            try
            {
                string codeFolder = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                string path = Path.Combine(codeFolder, @"..\..\..\..\avt_default.jpg");
                byte[] data = File.ReadAllBytes(path);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception($"Cannot init avatar user with expection: {ex.ToString()}");
            }
        }
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
        public static byte[] ConvertFileToURL(IFormFile file)
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
                    return imageData = ms.ToArray();
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
        public static string GeneratePassword(int length)
        {
            const string lowercaseChars = "abcdefghijklmnopqrstuvwxyz";
            const string uppercaseChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string numericChars = "0123456789";
            const string allChars = uppercaseChars + lowercaseChars + numericChars;

            char[] password = new char[length];
            int charIndex;

            // Đảm bảo có ít nhất một chữ hoa, một chữ thường và một số trong mật khẩu
            password[0] = uppercaseChars[rng.Next(uppercaseChars.Length)];
            password[1] = lowercaseChars[rng.Next(lowercaseChars.Length)];
            password[2] = numericChars[rng.Next(numericChars.Length)];

            // Sinh các ký tự ngẫu nhiên cho các vị trí còn lại trong mật khẩu
            for (int i = 3; i < length; i++)
            {
                password[i] = allChars[rng.Next(allChars.Length)];
            }

            // Trộn ngẫu nhiên các ký tự trong mật khẩu
            for (int i = length - 1; i > 0; i--)
            {
                charIndex = rng.Next(i + 1);
                char temp = password[i];
                password[i] = password[charIndex];
                password[charIndex] = temp;
            }

            return new string(password);
        }
        public static void GrantDirectoryAccess(string directoryPath)
        {
            try
            {
                // Lấy thông tin hiện tại của thư mục
                DirectoryInfo directoryInfo = new DirectoryInfo(directoryPath);
                DirectorySecurity directorySecurity = directoryInfo.GetAccessControl();

                // Thiết lập quyền truy cập cho mọi người (Everyone) với quyền FullControl
                string everyone = new System.Security.Principal.SecurityIdentifier(System.Security.Principal.WellKnownSidType.WorldSid, null).Translate(typeof(System.Security.Principal.NTAccount)).ToString();
                FileSystemAccessRule accessRule = new FileSystemAccessRule(everyone, FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.None, AccessControlType.Allow);

                // Thêm quyền truy cập vào thư mục
                directorySecurity.AddAccessRule(accessRule);

                // Cập nhật thông tin quyền truy cập cho thư mục
                directoryInfo.SetAccessControl(directorySecurity);

                Console.WriteLine($"Granted access to {directoryPath} for Everyone.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public static async Task<int> GetTotalCount<T>(DbContext dbContext) where T : class
        {
            // Your logic to fetch the total count of all items from the data source
            // You can use LINQ or any other data access methods here

            return await dbContext.Set<T>().CountAsync();
        }
    }
}
