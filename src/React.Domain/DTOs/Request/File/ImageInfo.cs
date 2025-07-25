using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace React.Domain.DTOs.Request.File
{
    public class ImageInfo
    {
        public int SrNo { get; set; }
        public string ImageName { get; set; } = string.Empty;
    }
}
