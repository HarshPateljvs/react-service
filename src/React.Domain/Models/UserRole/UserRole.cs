using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace React.Domain.Models.UserRole
{
    [Table("UserRole")]
    public class UserRole
    {
        [Key]
        public int Id { get; set; }
        public string RoleName { get; set; } = "";
        [JsonIgnore]
        public virtual ICollection<React.Domain.Models.AppUser.AppUser> AppUsers { get; set; }
    }
}
