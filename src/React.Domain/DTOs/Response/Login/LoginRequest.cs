using React.Domain.Models.AppUser;
using React.Domain.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace React.Domain.DTOs.Response.Login
{
    public class LoginResponseDto
    {
        public string Token { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public User User { get; set; }
        public AppUser AppUser { get; set; }

    }

}
