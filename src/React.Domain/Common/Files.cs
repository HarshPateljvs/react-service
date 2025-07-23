using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace React.Domain.Common
{
    public class Files
    {
        public string FileName { get; set; } = "";
        public string OriginalImage { get; set; } = "";
        public string OriginalImageURL { get; set; } = "";

        public string SmallImage { get; set; } = "";
        public string SmallImageURL { get; set; } = "";

        public string MediumImage { get; set; } = "";
        public string MediumImageURL { get; set; } = "";

        public string LargeImage { get; set; } = "";
        public string LargeImageURL { get; set; } = "";

        public string CustomImage { get; set; } = "";
        public string CustomImageURL { get; set; } = "";

        public string Alt { get; set; } = "";
    }

}
