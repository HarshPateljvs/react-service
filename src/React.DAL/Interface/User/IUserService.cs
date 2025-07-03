using React.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace React.DAL.Interface.User
{
    public interface IUserService
    {
        Task<APIBaseResponse<IEnumerable<Domain.User.User>>> GetAllUsersAsync();
        Task<APIBaseResponse<Domain.User.User>> GetUserByIdAsync(int id);
        Task<APIBaseResponse<Domain.User.User>> AddUserAsync(Domain.User.User user);
        Task<APIBaseResponse<Domain.User.User>> UpdateUserAsync(Domain.User.User user);
        Task<APIBaseResponse<Domain.User.User>> DeleteUserAsync(int id);
    }
}
