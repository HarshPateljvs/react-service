using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using React.DAL.Implementation.Common;
using React.DAL.Interface.Common;
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
                    ImageProp = StaticResource.ImageObject(StaticResource.Temp + StaticResource.FileUniqueSept + input.Folder, fileName, 0, 0)
                },
                StatusCode = 200,
                ResponseCode = ResponseCodes.SUCCESS
            };
            return response;
        }

        #region Image Resizer
        private readonly IFileProvider _fileProvider;
        private readonly IGenericRepository<Domain.Models.File.ImageInfo> _imageRepo;

        public FileService(IFileProvider fileProvider, IGenericRepository<Domain.Models.File.ImageInfo> imageRepo)
        {
            _fileProvider = fileProvider;
            _imageRepo = imageRepo;
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
            if (resizedInfo.Exists)
            {
                var cachedStream = new FileStream(resizedInfo.PhysicalPath, FileMode.Open, FileAccess.Read, FileShare.Read);
                return new FileStreamResult(cachedStream, "image/jpeg");
            }

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

        public async Task<APIBaseResponse<ThumbImageResult>> GetThumbImage(string url, int width, int height)
        {
            var response = new APIBaseResponse<ThumbImageResult>
            {
                Data = new ThumbImageResult { ImageURL = StaticResource.CheckFileExist(url, width, height) },
                StatusCode = 200,
                ResponseCode = ResponseCodes.SUCCESS
            };
            return response;
        }


        #region Images
        public async Task ManageImagesAsync(string tableName, string moduleName, int moduleId, ImageInfoRequest imageRequest)
        {
            var existingImages = await _imageRepo.GetAllAsync(new FilterDto
            {
                Predicates = {
            { nameof(Domain.Models.File.ImageInfo.TableName), tableName },
            { nameof(Domain.Models.File.ImageInfo.ModuleName), moduleName },
            { nameof(Domain.Models.File.ImageInfo.ModuleId), moduleId }
        }
            });

            var maxSrNo = existingImages.Data?.Any() == true
                ? existingImages.Data.Max(x => x.SrNo)
                : 0;

            // Define temp and final folder path using StaticResource
            string tempFolder = $"{StaticResource.Temp}{StaticResource.FileUniqueSept}{moduleName}";
            string finalFolder = $"{StaticResource.Files}{StaticResource.FileUniqueSept}{moduleName}{StaticResource.FileUniqueSept}{moduleId}";

            // 1. Add New Images
            foreach (var img in imageRequest.AddImages ?? new())
            {
                var fromPath = StaticResource.GetFilePath(tempFolder, img.ImageName);
                var toPath = StaticResource.GetFilePath(finalFolder, img.ImageName);

                // Move image file from temp to final path
                StaticResource.MoveFile(fromPath, toPath);

                await _imageRepo.AddAsync(new Domain.Models.File.ImageInfo
                {
                    TableName = tableName,
                    ModuleName = moduleName,
                    ModuleId = moduleId,
                    SrNo = ++maxSrNo,
                    ImageName = img.ImageName,
                    AddDate = DateTime.UtcNow,
                    DataVersion = 1
                });
            }

            // 2. Update Existing Images (by SrNo)
            foreach (var img in imageRequest.UpdateImages ?? new())
            {
                var entity = existingImages.Data?.FirstOrDefault(x => x.SrNo == img.SrNo);
                if (entity != null)
                {
                    var oldPath = StaticResource.GetFilePath(finalFolder, entity.ImageName);
                    var newPath = StaticResource.GetFilePath(finalFolder, img.ImageName);

                    // Move (rename) the file if name changed
                    if (!string.Equals(entity.ImageName, img.ImageName, StringComparison.OrdinalIgnoreCase))
                    {
                        StaticResource.MoveFile(oldPath, newPath);
                    }

                    entity.ImageName = img.ImageName;
                    entity.EditDate = DateTime.UtcNow;
                    entity.DataVersion += 1;
                    await _imageRepo.UpdateAsync(entity);
                }
            }

            // 3. Delete Images (by SrNo)
            foreach (var img in imageRequest.DeleteImages ?? new())
            {
                var entity = existingImages.Data?.FirstOrDefault(x => x.SrNo == img.SrNo);
                if (entity != null)
                {
                    var filePath = StaticResource.GetFilePath(finalFolder, entity.ImageName);

                    // Delete physical file
                    StaticResource.DeleteFile(filePath);

                    await _imageRepo.DeleteAsync(entity);
                }
            }
        }

        public async Task<ImageInfoRequest> GetImagesByModuleAsync(string tableName, string moduleName, int moduleId)
        {
            var images = await _imageRepo.GetAllAsync(new FilterDto
            {
                Predicates = new Dictionary<string, object>
        {
            { nameof(Domain.Models.File.ImageInfo.TableName), tableName },
            { nameof(Domain.Models.File.ImageInfo.ModuleName), moduleName },
            { nameof(Domain.Models.File.ImageInfo.ModuleId), moduleId }
        }
            });

            if (images.TotalCount > 0)
            {
                var imageList = images.Data?
               .OrderBy(x => x.SrNo)
               .Select(x =>
               {
                   var folderPath = $"{StaticResource.Files}{StaticResource.FileUniqueSept}{moduleName}";
                   var filePath = $"{folderPath}{StaticResource.FileUniqueSept}{moduleId}";
                   var imageProp = StaticResource.ImageObject(filePath, x.ImageName, 50, 50);

                   return new React.Domain.DTOs.Request.File.ImageInfo
                   {
                       SrNo = x.SrNo,
                       ImageName = imageProp?.CustomImageURL ?? "" // ⬅️ Return 50x50 image URL
                   };
               })
               .ToList() ?? new();
                return new ImageInfoRequest
                {
                    AddImages = imageList, // All loaded in AddImages by default
                    UpdateImages = new List<React.Domain.DTOs.Request.File.ImageInfo>(),
                    DeleteImages = new List<React.Domain.DTOs.Request.File.ImageInfo>()
                };
            }
            else
            {
                return new ImageInfoRequest();
            }
        }

        #endregion
    }

}
