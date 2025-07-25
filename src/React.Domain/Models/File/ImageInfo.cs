using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace React.Domain.Models.File
{
    [Table("ImageInfo")]
    public class ImageInfo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int SrNo { get; set; }

        [Required]
        [MaxLength(200)]
        public string TableName { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        public string ModuleName { get; set; } = string.Empty;

        [Required]
        public int ModuleId { get; set; }

        [Required]
        [MaxLength(1000)]
        public string ImageName { get; set; } = string.Empty;

        [Required]
        public DateTime AddDate { get; set; } = DateTime.UtcNow;

        public DateTime? EditDate { get; set; }

        [Required]
        public int DataVersion { get; set; } = 1;
    }
}
