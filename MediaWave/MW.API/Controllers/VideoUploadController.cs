using Microsoft.AspNetCore.Mvc;
using MuseWave.API.Controllers;

namespace MW.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class VideoUploadController : ControllerBase
    {
        [HttpPost("upload-video")]
        public IActionResult UploadVideo([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            // Process the file here

            return Ok("File uploaded successfully.");
        }
    }
}
