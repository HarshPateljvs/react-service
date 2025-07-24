using Microsoft.AspNetCore.Http;
using React.DAL.Paths;
using React.Domain.Common;
using System;
using System.IO;
using static System.Net.WebRequestMethods;

namespace React.DAL.Utils
{
    public static class StaticResource
    {
        static IServiceProvider services = null;
        public static IServiceProvider Services
        {
            get { return services; }
            set
            {
                if (services != null)
                {
                    throw new Exception("Can't set once a value has already been set.");
                }
                services = value;
            }
        }
        public static HttpContext HttpContext_Current
        {
            get
            {
                IHttpContextAccessor httpContextAccessor = services.GetService(typeof(IHttpContextAccessor)) as IHttpContextAccessor;
                return httpContextAccessor?.HttpContext;
            }
        }


        #region Constants

        public static string Temp => "temp";
        public static string Errorlog => "Errorlogs";
        public static string Logs => "logs";
        public static string Files => "Files";
        public static string Thumb => "thumb";
        public static string Wwwroot => "wwwroot";
        private static string Slash => "/";
        private static string DoubleSlash => "\\";
        public static string FileUniqueSept => "__";

        #endregion

        #region Paths

        public static string GetRootDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..");

        public static string GetWwwRootPath = Path.GetFullPath(Path.Combine(GetRootDirectory, StaticResource.Wwwroot));

        #endregion

        #region General File Access
        public static string GetFolder(string FolderPath)
        {
            string normalized = ExtenstionMethods.NormalizePath(FolderPath, DoubleSlash);
            string folderPath = Path.Combine(GetWwwRootPath, normalized);
            Directory.CreateDirectory(folderPath);
            return folderPath;
        }

        public static string GetFilePath(string FolderPath, string fileName)
        {
            string normalized = ExtenstionMethods.NormalizePath(FolderPath, DoubleSlash);
            string folderPath = Path.Combine(GetWwwRootPath, normalized);
            Directory.CreateDirectory(folderPath);
            return Path.Combine(folderPath, fileName);
        }
        public static string GetFileUrl(string BaseURL)
        {
            var scheme = HttpContext_Current.Request.Scheme;
            var host = HttpContext_Current.Request.Host.Value;
            string normalized = ExtenstionMethods.NormalizePath(BaseURL, Slash);
            return $"{scheme}://{host}/{normalized}";
        }

        public static string CheckFileExist(string Filepath, int width = 600, int height = 600)
        {
            string normalized = ExtenstionMethods.NormalizePath(Filepath, DoubleSlash);
            string fullPath = Path.Combine(GetWwwRootPath, normalized.TrimStart('/', '\\'));

            if (System.IO.File.Exists(fullPath))
            {
                if (width == -1)
                {
                    return normalized.Replace("\\", "/");
                }

                FileInfo finfo = new FileInfo(fullPath);
                return $"{StaticResource.Thumb}/{width}x{height}/{finfo.LastWriteTime.Ticks}/{normalized.Replace("\\", "/")}";
            }

            return ""; // File not found
        }

        public static ImageProperty? ImageObject(string filePath, string fileName, int width = 0, int height = 0)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return null;

            ImageProperty obj = new ImageProperty();
            string relativePath = Path.Combine(filePath, fileName).Replace("\\", "/");

            obj.ImageName = ExtenstionMethods.NormalizePath(fileName, Slash);
            obj.OriginalImage = ExtenstionMethods.NormalizePath(relativePath, Slash);
            obj.OriginalImageURL = GetFileUrl(filePath + StaticResource.FileUniqueSept + fileName);

            if (width > 0 && height > 0)
            {
                obj.CustomImage = CheckFileExist(filePath + StaticResource.FileUniqueSept + fileName, width, height);
                obj.CustomImageURL = GetFileUrl(obj.CustomImage);
            }
            else
            {
                obj.SmallImage = CheckFileExist(filePath + StaticResource.FileUniqueSept + fileName, 500, 500);
                obj.SmallImageURL = GetFileUrl(obj.SmallImage);

                obj.MediumImage = CheckFileExist(filePath + StaticResource.FileUniqueSept + fileName, 1000, 1000);
                obj.MediumImageURL = GetFileUrl(obj.MediumImage);

                obj.LargeImage = CheckFileExist(filePath + StaticResource.FileUniqueSept + fileName, 1920, 1080);
                obj.LargeImageURL = GetFileUrl(obj.LargeImage);
            }

            return obj;
        }

        #endregion

    }
}
