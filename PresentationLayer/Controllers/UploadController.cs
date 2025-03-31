using BussinessLayer.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;
using Shared.Constants;

namespace PresentationLayer.Controllers
{
    public class UploadController : Controller
    {
        private readonly ICloudinaryService _cloudinaryService;

        public UploadController(ICloudinaryService cloudinaryService)
        {
            _cloudinaryService = cloudinaryService;
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile file, string pathName)
        {
            if (file == null || file.Length == 0)
            {
                return Json(new { success = false, message = "Please select an image to upload." });
            }

            using var stream = file.OpenReadStream();
            var imageUrl = await _cloudinaryService.UploadImage(stream, pathName);

            return Json(new { success = true, imageUrl });
        }
    }
}