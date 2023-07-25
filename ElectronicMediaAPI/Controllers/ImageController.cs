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
using System.IO;
using Microsoft.AspNetCore.Mvc;
using ElectronicMedia.Core.Services.Interfaces;

namespace ElectronicMediaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
        private readonly IFileStorageService _fileStorageService;

        public ImageController(IFileStorageService fileStorageService)
        {
            _fileStorageService = fileStorageService;
        }
        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile image)
        {
            try
            {
                if (image == null || !IsImageFile(image))
                {
                    return BadRequest("Invalid or unsupported image file.");
                }
                var url = await _fileStorageService.SaveImageFile(image);
                return Ok(url);
            }
            catch (Exception)
            {
                return StatusCode(500, new { success = false, url = "" });
            }
        }
        [HttpPost("delete")]
        public async Task<IActionResult> Delete(IFormFile image)
        {
            try
            {
                if (image == null || !IsImageFile(image))
                {
                    return BadRequest("Invalid or unsupported image file.");
                }
                var url = _fileStorageService.DeleteImageFileApi(image);
                return Ok(url);

            }
            catch (Exception)
            {
                return StatusCode(500, new { success = false, url = "" });
            }
        }
        #region private func
        private bool IsImageFile(IFormFile file)
        {
            var fileExtension = Path.GetExtension(file.FileName).ToLower();
            return allowedExtensions.Contains(fileExtension);
        }
        #endregion
    }
}
