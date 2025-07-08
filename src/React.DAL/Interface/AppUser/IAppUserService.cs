using React.Domain.Common;
using React.Domain.DTOs.Response.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace React.DAL.Interface.AppUser
{
    public interface IAppUserService
    {
        Task<APIBaseResponse<React.Domain.Models.AppUser.AppUser>> AddAppUserAsync(React.Domain.Models.AppUser.AppUser user);
        Task<APIBaseResponse<LoginResponseDto>> LoginAsync(string email, string password);

    }
}
