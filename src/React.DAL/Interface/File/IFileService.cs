using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using React.Domain.Common;
using React.Domain.DTOs.Request.File;
using React.Domain.DTOs.Response.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace React.DAL.Interface.File
{
    public interface IFileService
    {
        Task<APIBaseResponse<FileUploadResponseDto>> UploadFileAsync(FileUploadInput dto);
        Task<FileStreamResult> GetResizedImageAsync(string url, long timestamp, int width, int height);

    }
}
