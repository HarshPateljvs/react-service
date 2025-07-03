using React.DAL.Interface.Common;
using React.DAL.Interface.User;
using React.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace React.DAL.Implementation.User
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<Domain.User.User> _userRepository;

        public UserService(IGenericRepository<Domain.User.User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<APIBaseResponse<IEnumerable<Domain.User.User>>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<APIBaseResponse<Domain.User.User>> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<APIBaseResponse<Domain.User.User>> AddUserAsync(Domain.User.User user)
        {
            return await _userRepository.AddAsync(user);
        }

        public async Task<APIBaseResponse<Domain.User.User>> UpdateUserAsync(Domain.User.User user)
        {
            return await _userRepository.UpdateAsync(user);
        }

        public async Task<APIBaseResponse<Domain.User.User>> DeleteUserAsync(int id)
        {
            var existingUserResponse = await _userRepository.GetByIdAsync(id);

            if (existingUserResponse.HasError || existingUserResponse.Data == null)
            {
                return new APIBaseResponse<Domain.User.User>
                {
                    ResponseCode = ResponseCodes.NOT_FOUND,
                    ErrorMessage = new List<string> { "User not found" },
                    Data = null
                };
            }

            return await _userRepository.DeleteAsync(existingUserResponse.Data);
        }
    }
}
