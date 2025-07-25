using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace React.Domain.DTOs.Request.File
{
    public class ImageInfoRequest
    {
        public List<ImageInfo> AddImages { get; set; }
        public List<ImageInfo> UpdateImages { get; set; }
        public List<ImageInfo> DeleteImages { get; set; }
    }
}
