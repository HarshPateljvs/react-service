using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace React.Domain.Models.mstError
{
    [Table("mstErrorLog")]
    public class ExceptionLog
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(200)]
        public string ExceptionType { get; set; }

        public string ErrorMessage { get; set; }

        public string StackTrace { get; set; }

        [MaxLength(500)]
        public string RequestPath { get; set; }

        [MaxLength(100)]
        public string ControllerName { get; set; }

        [MaxLength(300)]
        public string RouteTemplate { get; set; }

        public string InputParameters { get; set; }

        public string ResponseBody { get; set; }
        public string LogFileUrl { get; set; }
        public string RequestMethod { get; set; }

        [MaxLength(50)]
        public string ElapsedTime { get; set; }

        [MaxLength(50)]
        public string IpAddress { get; set; }

        public DateTime ErrorDateTime { get; set; }

        [MaxLength(100)]
        public string UserId { get; set; }

        [MaxLength(100)]
        public string UserName { get; set; }

        public string Token { get; set; }

        public bool IsSent { get; set; } = false; // Optional flag, as in your legacy code
    }
}
