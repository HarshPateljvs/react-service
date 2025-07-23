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
            var response = await _fileService.UploadFileAsync(dto, HttpContext);
            return Ok(response);
        }

    }
}
