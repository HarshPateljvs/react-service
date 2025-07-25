using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using React.DAL.Interface.File;
using React.DAL.Utils;
using React.Domain.Common;
using React.Domain.DTOs.Request.File;

namespace React.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;

        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }
        [HttpPost("Upload")]
        public async Task<IActionResult> UploadTempImage([FromForm] FileUploadInput dto)
        {
            var response = await _fileService.UploadFileAsync(dto);
            return Ok(response);
        }
        [Route("/{StaticResource.Thumb}/{width}x{height}/{timestamp}/{*url}")]
        public async Task<IActionResult> Resize(int width, int height, long timestamp, string url)
        {
            var result = await _fileService.GetResizedImageAsync(url, timestamp, width, height);
            return result;
        }

        [Route("/{width}x{height}/{*url}")]
        public async Task<IActionResult> ThumbImage(int width, int height, string url)
        {
            var result = await _fileService.GetThumbImage(url, width, height);
            return Ok(result);
        }
    }
}
