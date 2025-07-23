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
        public async Task<APIBaseResponse<FileUploadResponseDto>> UploadFileAsync(FileUploadInput input)
        {
            if (input.File == null || string.IsNullOrWhiteSpace(input.Folder))
                throw new ArgumentException("File and Folder are required.");

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(input.File.FileName)}";

            var fullPath = StaticResource.GetFilePath(StaticResource.Temp + StaticResource.DoubleSlash + input.Folder, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await input.File.CopyToAsync(stream);
            }

            var response = new APIBaseResponse<FileUploadResponseDto>
            {
                Data = new FileUploadResponseDto
                {
                    FileName = StaticResource.Slash + StaticResource.Temp + StaticResource.Slash + input.Folder +fileName,
                    FileUrl = StaticResource.GetFileUrl(StaticResource.Temp + StaticResource.Slash + input.Folder, fileName),
                    ImageProp = StaticResource.ImageObject(StaticResource.Temp + StaticResource.DoubleSlash + input.Folder, fileName, 0, 0)
                },
                StatusCode = 200,
                ResponseCode = ResponseCodes.SUCCESS
            };
            return response;
        }
    }

}
