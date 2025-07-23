using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace React.Domain.DTOs.Request.File
{
    public class FileUploadInput
    {
        [Required]
        public IFormFile File { get; set; }
        [Required]
        public string Folder { get; set; }  // Example: "Employee", "User", etc.
    }
}
