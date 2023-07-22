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

using ElectronicMedia.Core.Common.Extension;
using ElectronicMedia.Core.Repository.Models;
using ElectronicMedia.Core.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ElectronicMedia.Core.Services.Service
{
    public class FileStorageService : IFileStorageService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public FileStorageService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public bool DeleteImageFile(string fileName)
        {
            var sharedImagesFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
            CommonService.GrantDirectoryAccess(sharedImagesFolder);
            string fullImagePath = Path.Combine(sharedImagesFolder, fileName);

            string directoryPath = Path.GetDirectoryName(sharedImagesFolder);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            if (File.Exists(fullImagePath))
            {
                File.Delete(fullImagePath);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteImageFileApi(IFormFile fileName)
        {
            return DeleteImageFile(fileName.FileName);
        }

        public async Task<string> SaveImageFile(IFormFile image)
        {
            if (image != null && image.Length > 0)
            {
                var sharedImagesFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                CommonService.GrantDirectoryAccess(sharedImagesFolder);
                var fileName = image.FileName;

                string directoryPath = Path.GetDirectoryName(sharedImagesFolder);
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                // Kết hợp đường dẫn thư mục và tên tập tin để tạo đường dẫn đầy đủ cho tập tin ảnh
                string fullImagePath = Path.Combine(sharedImagesFolder, fileName);

                // Ghi dữ liệu ảnh vào tập tin
                using (FileStream fileStream = new FileStream(fullImagePath, FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }

                var imageUrl = $"/images/{fileName}";
                return imageUrl;
            }
            else
            {
                return null;
            }
        }
    }
}
