using Microsoft.AspNetCore.Http;
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
        public static string Employee => Path.Combine(Files, "Employee");
        public static string Thumb => "thumb";
        public static string Wwwroot => "wwwroot";
        public static string Slash => "/";
        public static string DoubleSlash => "\\";

        #endregion

        #region Root Paths

        public static string GetRootDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..");

        public static string GetWwwRootPath = Path.GetFullPath(Path.Combine(GetRootDirectory, StaticResource.Wwwroot));

        #endregion

        #region General File Access
        public static string GetFolder(string FolderPath)
        {
            string folderPath = Path.Combine(GetWwwRootPath, FolderPath);
            Directory.CreateDirectory(folderPath);
            return folderPath;
        }

        public static string GetFilePath(string FolderPath, string fileName)
        {
            string folderPath = Path.Combine(GetWwwRootPath, FolderPath);
            Directory.CreateDirectory(folderPath);
            return Path.Combine(folderPath, fileName);
        }
        public static string GetFileUrl(string FolderPath, string fileName)
        {
            var scheme = HttpContext_Current.Request.Scheme;
            var host = HttpContext_Current.Request.Host.Value;
            return $"{scheme}://{host}/{FolderPath}/{fileName}";
        }

        public static string CheckFileExist(string Filepath, int width = 600, int height = 600)
        {
            string fullPath = Path.Combine(GetWwwRootPath, Filepath.TrimStart('/', '\\'));

            if (System.IO.File.Exists(fullPath))
            {
                if (width == -1)
                {
                    return Filepath.Replace("\\", "/");
                }

                FileInfo finfo = new FileInfo(fullPath);
                return $"/thumb/{width}x{height}/{finfo.LastWriteTime.Ticks}/{Filepath.Replace("\\", "/")}";
            }

            return ""; // File not found
        }

        public static ImageProperty? ImageObject(string FilePath, string fileName, int width = 0, int height = 0)
        {
            ImageProperty objImageProperty = new ImageProperty();
            if (string.IsNullOrWhiteSpace(fileName + StaticResource.DoubleSlash + fileName))
                return null;

            if (width > 0 && height > 0)
            {
                objImageProperty.CustomImage = CheckFileExist(FilePath + StaticResource.DoubleSlash + fileName, width, height);

            }
            else
            {
                objImageProperty.SmallImage = CheckFileExist(FilePath + StaticResource.DoubleSlash + fileName, 500, 500);
                objImageProperty.MediumImage = CheckFileExist(FilePath + StaticResource.DoubleSlash + fileName, 1000, 1000);
                objImageProperty.LargeImage = CheckFileExist(FilePath + StaticResource.DoubleSlash + fileName, 1920, 1080);
            }
            return objImageProperty;
        }
        #endregion

    }
}
