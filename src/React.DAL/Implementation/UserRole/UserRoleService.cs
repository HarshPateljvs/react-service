using React.DAL.Interface.UserRole;
using React.Domain.Common;
using React.Domain.Models.UserRole;
using React.DAL.Interface.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace React.DAL.Implementation.UserRole
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IGenericRepository<Domain.Models.UserRole.UserRole> _userRoleRepo;

        public UserRoleService(IGenericRepository<Domain.Models.UserRole.UserRole> userRoleRepo)
        {
            _userRoleRepo = userRoleRepo;
        }

        public async Task<APIBaseResponse<IEnumerable<Domain.Models.UserRole.UserRole>>> GetAllAsync()
        {
            return await _userRoleRepo.GetAllAsync();
        }
    }
}
