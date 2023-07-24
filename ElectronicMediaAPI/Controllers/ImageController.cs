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
