using React.Domain.Common;
using React.Domain.DTOs.Response.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace React.DAL.Interface.User
{
    public interface IUserService
    {
        Task<APIBaseResponse<IEnumerable<Domain.Models.User.User>>> GetAllUsersAsync(FilterDto? dto);
        Task<APIBaseResponse<Domain.Models.User.User>> GetUserByIdAsync(FilterDto? dto);
        Task<APIBaseResponse<Domain.Models.User.User>> AddUserAsync(Domain.Models.User.User user);
        Task<APIBaseResponse<Domain.Models.User.User>> UpdateUserAsync(Domain.Models.User.User user);
        Task<APIBaseResponse<Domain.Models.User.User>> DeleteUserAsync(FilterDto? dto);
        Task<APIBaseResponse<LoginResponseDto>> LoginAsync(string email, string password);

    }
}
