using Microsoft.AspNetCore.Http;
using React.DAL.Interface.File;
using React.DAL.Utils;
using React.Domain.Common;
using React.Domain.DTOs.Request.File;
using React.Domain.DTOs.Response.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace React.DAL.Implementation.File
{
    public class FileService : IFileService
    {
        public async Task<APIBaseResponse<FileUploadResponseDto>> UploadFileAsync(FileUploadInput dto, HttpContext context)
        {
            if (dto.File == null || string.IsNullOrWhiteSpace(dto.Folder))
                throw new ArgumentException("File and Folder are required.");

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(dto.File.FileName)}";

            var fullPath = StaticResource.GetFilePath(StaticResource.Temp, dto.Folder, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await dto.File.CopyToAsync(stream);
            }

            var response = new APIBaseResponse<FileUploadResponseDto>
            {
                Data = new FileUploadResponseDto
                {
                    FileName = fileName,
                    FileUrl = StaticResource.GetFilePathWithURL(StaticResource.Temp, dto.Folder, fileName, context)
                },
                StatusCode = 200,
                ResponseCode = ResponseCodes.SUCCESS
            };
            return response;
        }
    }

}
