using React.DAL.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace React.DAL.Paths
{
    public static class ExtenstionMethods
    {
        public static string ReplaceExtension(string path)
        {
            return Path.ChangeExtension(path, ".jpg")!;
        }

        /// <summary>
        /// Normalizes a path by replacing one separator with another (e.g., from FileUniqueSept to Slash or DoubleSlash).
        /// </summary>
        /// <param name="input">The raw path to normalize</param>
        /// <param name="fromSeparator">What to replace (e.g., "__")</param>
        /// <param name="toSeparator">What to use instead (e.g., "/" or "\\")</param>
        /// <returns>Normalized path</returns>
        public static string NormalizePath(string input, string toSeparator)
        {
            return input.Replace(StaticResource.FileUniqueSept, toSeparator);
        }
    }
}
