using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace React.Domain.Models.AppUser
{
    [Table("User")]
    public class AppUser
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [StringLength(100, ErrorMessage = "First Name can't exceed 100 characters")]
        public string FirstName { get; set; } = "";

        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(100, ErrorMessage = "Last Name can't exceed 100 characters")]
        public string LastName { get; set; } = "";

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [StringLength(255, ErrorMessage = "Email can't exceed 255 characters")]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid Phone Number")]
        [StringLength(20, ErrorMessage = "Phone number can't exceed 20 characters")]
        public string PhoneNumber { get; set; } = "";

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, ErrorMessage = "Password must be between 6 and 100 characters", MinimumLength = 6)]
        public string Password { get; set; } = "";

        public int? RoleId { get; set; }
        [ForeignKey(nameof(RoleId))]
        [JsonIgnore ]
        public virtual React.Domain.Models.UserRole.UserRole UserRole { get; set; } // Navigation property
    }
}
