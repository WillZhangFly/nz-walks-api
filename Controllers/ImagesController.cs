using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.Models.Domain;
using NZWalks.Models.DTO;

namespace NZWalks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        // POST: /api/Images/Upload
        [HttpPost("Upload")]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto request)
        {
            ValidateFileUpload(request);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                //user repository to save the image
            }

        }


        private void ValidateFileUpload(ImageUploadRequestDto request)
        {

            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png", ".gif" };

            if (allowedExtensions.Contains(Path.GetExtension(request.File.FileName).ToLower()) == false)
            {
                ModelState.AddModelError("file", "Invalid file type. Allowed types are: " + string.Join(", ", allowedExtensions));
            }

            if (request.File == null || request.File.Length == 0)
            {
                ModelState.AddModelError("file", "File is required.");
            }

            if (request.File.Length > 10 * 1024 * 1024) // 10 MB limit
            {
                ModelState.AddModelError("file", "File size exceeds the limit of 10 MB.");
            }

        }
    }
}
