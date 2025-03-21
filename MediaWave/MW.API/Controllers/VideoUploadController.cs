using Microsoft.AspNetCore.Mvc;

namespace MW.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class VideoUploadController : ApiControllerBase
{
    private readonly IWebHostEnvironment _environment;
    private readonly string[] _allowedExtensions = { ".mp4", ".avi", ".mov", ".mkv" };
    private const long _maxFileSize = 500 * 1024 * 1024; 

    public VideoUploadController(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadVideo(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("No file uploaded.");
        }

        var extension = Path.GetExtension(file.FileName).ToLower();
        if (!_allowedExtensions.Contains(extension))
        {
            return BadRequest("Invalid file type. Only MP4, AVI, MOV, and MKV are allowed.");
        }

        if (file.Length > _maxFileSize)
        {
            return BadRequest($"File size exceeds the limit of {_maxFileSize / (1024 * 1024)}MB.");
        }

        var uploadPath = Path.Combine(_environment.WebRootPath ?? Directory.GetCurrentDirectory(), "wwwroot", "uploads", "videos");
        if (!Directory.Exists(uploadPath))
        {
            Directory.CreateDirectory(uploadPath);
        }

        var uniqueFileName = $"{Guid.NewGuid()}{extension}";
        var filePath = Path.Combine(uploadPath, uniqueFileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        var fileUrl = $"{Request.Scheme}://{Request.Host}/uploads/videos/{uniqueFileName}";

        return Ok(new { FileName = uniqueFileName, FileUrl = fileUrl });
    }
    
}
