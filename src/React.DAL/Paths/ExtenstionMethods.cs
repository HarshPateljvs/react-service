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
    }
}
