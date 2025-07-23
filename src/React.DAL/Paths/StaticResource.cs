using Microsoft.AspNetCore.Http;
using React.Domain.Common;
using System.Globalization;

namespace React.DAL.Utils
{
    public static class StaticResource
    {
        #region FilePathNames
        public static string Temp => "temp"; 
        public static string Files => "Files"; 
        public static string Employee => Files + "Employee";
        public static string Thumb => "thumb";
        #endregion

        #region Root Paths  
        public static string GetRootDirectory()
        {
            // Go from /bin/Debug/net8.0 to project root
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..");
        }
        public static string GetWwwRootPath()
        {
            return Path.GetFullPath(Path.Combine(GetRootDirectory(), "wwwroot"));
        } 
        #endregion

        #region Log Methods
        public static string GetRootLogDirectory()
        {
            var patlogpath = Path.Combine(GetWwwRootPath(), "logs");
            Directory.CreateDirectory(patlogpath);
            return patlogpath;
        }
        public static string GetLogFileUrl(string controllerName, string fileName, HttpContext context)
        {
            var scheme = context.Request.Scheme;
            var host = context.Request.Host.Value;
            return $"{scheme}://{host}/logs/log/{controllerName}/{fileName}";
        }
        public static string GetLogFolderForController(string controllerName)
        {
            // This points to: D:\Projects\DotNet\ReactAPIs\Code\logs\log\UserRole
            var path = Path.Combine(GetRootLogDirectory(), "log", controllerName);
            Directory.CreateDirectory(path); // Ensure folder exists
            return path;
        }
        #endregion


        /// <summary>
        /// Returns full physical path to the file (and creates directory if not exists).
        /// Example: GetFilePath("logs", "log/User", "2025-07-23.txt")
        /// </summary>
        public static string GetFilePath(string rootFolder, string subFolder, string fileName)
        {
            var folderPath = Path.Combine(GetWwwRootPath(), rootFolder, subFolder);
            Directory.CreateDirectory(folderPath);
            return Path.Combine(folderPath, fileName);
        }

        /// <summary>
        /// Returns full URL to the file via HttpContext.
        /// </summary>
        public static string GetFilePathWithURL(string rootFolder, string subFolder, string fileName, HttpContext context)
        {
            var scheme = context.Request.Scheme;
            var host = context.Request.Host.Value;
            return $"{scheme}://{host}/{rootFolder}/{subFolder}/{fileName}";
        }

        public static Files? GetFile(string basePath, string controllerSubPath, string? fileName, HttpContext context, string? customSize = null)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return null;

            string defaultNotFound = "ImageNotFound.jpg";

            string subFolder = Path.Combine(DateTime.Now.Ticks.ToString(), controllerSubPath).Replace("\\", "/");

            string finalFileName = fileName!;
            string altText = "Loaded image";

            string originalPath = GetFilePath(basePath, subFolder, fileName!);
            if (!File.Exists(originalPath))
            {
                finalFileName = defaultNotFound;
                altText = "Image not found";
            }

            var file = new Files
            {
                FileName = finalFileName,
                Alt = altText,
                OriginalImage = GetFilePath(basePath, subFolder, finalFileName),
                OriginalImageURL = GetFilePathWithURL(basePath, subFolder, finalFileName, context)
            };

            if (!string.IsNullOrWhiteSpace(customSize))
            {
                string customPath = $"{customSize}/{subFolder}";

                file.CustomImage = GetFilePath(Thumb, customPath, finalFileName);
                file.CustomImageURL = GetFilePathWithURL(Thumb, customPath, finalFileName, context);
            }
            else
            {
                file.SmallImage = GetFilePath(Thumb, $"500x500/{subFolder}", finalFileName);
                file.SmallImageURL = GetFilePathWithURL(Thumb, $"500x500/{subFolder}", finalFileName, context);

                file.MediumImage = GetFilePath(Thumb, $"1000x1000/{subFolder}", finalFileName);
                file.MediumImageURL = GetFilePathWithURL(Thumb, $"1000x1000/{subFolder}", finalFileName, context);

                file.LargeImage = GetFilePath(Thumb, $"1920x1080/{subFolder}", finalFileName);
                file.LargeImageURL = GetFilePathWithURL(Thumb, $"1920x1080/{subFolder}", finalFileName, context);
            }

            return file;
        }

    }
}
