using React.Domain.Common;
using React.Domain.Models.UserRole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace React.DAL.Interface.UserRole
{
    public interface IUserRoleService
    {
        Task<APIBaseResponse<IEnumerable<Domain.Models.UserRole.UserRole>>> GetAllAsync();
    }
}
