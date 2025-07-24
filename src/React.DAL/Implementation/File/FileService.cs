using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using React.DAL.Interface.File;
using React.DAL.Utils;
using React.Domain.Common;
using React.Domain.DTOs.Request.File;
using React.Domain.DTOs.Response.File;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
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

            var fullPath = StaticResource.GetFilePath(StaticResource.Temp + StaticResource.FileUniqueSept + input.Folder, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await input.File.CopyToAsync(stream);
            }

            var response = new APIBaseResponse<FileUploadResponseDto>
            {
                Data = new FileUploadResponseDto
                {
                    //FileName = StaticResource.FileUniqueSept + StaticResource.Temp + StaticResource.FileUniqueSept + input.Folder + fileName,
                    //FileUrl = StaticResource.GetFileUrl(StaticResource.Temp + StaticResource.FileUniqueSept + input.Folder + StaticResource.FileUniqueSept + fileName),
                    ImageProp = StaticResource.ImageObject( StaticResource.Temp + StaticResource.FileUniqueSept + input.Folder, fileName, 0, 0)
                },
                StatusCode = 200,
                ResponseCode = ResponseCodes.SUCCESS
            };
            return response;
        }

        #region Image Resizer
        private readonly IFileProvider _fileProvider;

        public FileService(IFileProvider fileProvider)
        {
            _fileProvider = fileProvider;
        }

        public async Task<FileStreamResult> GetResizedImageAsync(string url, long timestamp, int width, int height)
        {

            url = "/" + url.TrimStart('/');
            var originalPath = PathString.FromUriComponent(url);
            var fileInfo = _fileProvider.GetFileInfo(originalPath);

            if (!fileInfo.Exists)
                throw new FileNotFoundException("Image not found", fileInfo.PhysicalPath);

            var resizedRelativePath = React.DAL.Paths.ExtenstionMethods.ReplaceExtension($"/{StaticResource.Thumb}/{width}x{height}/{timestamp}/{url}");
            var resizedInfo = _fileProvider.GetFileInfo(resizedRelativePath);

            // Ensure directory exists
            Directory.CreateDirectory(Path.GetDirectoryName(resizedInfo.PhysicalPath)!);

            using (var stream = new FileStream(fileInfo.PhysicalPath, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (SixLabors.ImageSharp.Image image = SixLabors.ImageSharp.Image.Load(stream))
            {
                image.Mutate(x => x.Resize(width, height));
                await image.SaveAsync(resizedInfo.PhysicalPath);
            }

            var resultStream = new FileStream(resizedInfo.PhysicalPath, FileMode.Open, FileAccess.Read);
            return new FileStreamResult(resultStream, "image/jpeg");

        }
        #endregion
    }

}
