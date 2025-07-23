using React.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace React.Domain.DTOs.Response.File
{
    public class FileUploadResponseDto
    {
        public string FileName { get; set; }
        public string FileUrl { get; set; }
        public ImageProperty? ImageProp { get; set; }
    }
}
