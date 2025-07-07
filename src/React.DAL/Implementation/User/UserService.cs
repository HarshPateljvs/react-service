using Microsoft.EntityFrameworkCore;
using React.DAL.Implementation.Common;
using React.DAL.Implementation.Jwt;
using React.DAL.Interface.Common;
using React.DAL.Interface.User;
using React.Domain.Common;
using React.Domain.DTOs.Response.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace React.DAL.Implementation.User
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<Domain.Models.User.User> _userRepository;
        private readonly JwtTokenGenerator _jwtTokenGenerator;

        public UserService(IGenericRepository<Domain.Models.User.User> userRepository,JwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<APIBaseResponse<IEnumerable<Domain.Models.User.User>>> GetAllUsersAsync(FilterDto? dto)
        {
            return await _userRepository.GetAllAsync(dto);
        }

        public async Task<APIBaseResponse<Domain.Models.User.User>> GetUserByIdAsync(FilterDto? dto)
        {
            return await _userRepository.GetByIdAsync(dto);
        }

        public async Task<APIBaseResponse<Domain.Models.User.User>> AddUserAsync(Domain.Models.User.User user)
        {
            return await _userRepository.AddAsync(user);
        }

        public async Task<APIBaseResponse<Domain.Models.User.User>> UpdateUserAsync(Domain.Models.User.User user)
        {
            return await _userRepository.UpdateAsync(user);
        }

        public async Task<APIBaseResponse<Domain.Models.User.User>> DeleteUserAsync(FilterDto? dto)
        {
            var existingUserResponse = await _userRepository.GetByIdAsync(dto);

            if (existingUserResponse.HasError || existingUserResponse.Data == null)
            {
                return new APIBaseResponse<Domain.Models.User.User>
                {
                    ResponseCode = ResponseCodes.NOT_FOUND,
                    ErrorMessage = new List<string> { "User not found" },
                    Data = null
                };
            }

            return await _userRepository.DeleteAsync(existingUserResponse.Data);
        }

        public async Task<APIBaseResponse<LoginResponseDto>> LoginAsync(string email, string password)
        {   
            FilterDto filter = new FilterDto();

            filter.AddPredicateIfNotNull(nameof(Domain.Models.User.User.Email), email);
            filter.AddPredicateIfNotNull(nameof(Domain.Models.User.User.Password), password);
            APIBaseResponse<Domain.Models.User.User> user = await _userRepository.GetByIdAsync(filter);

            if (user == null || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                return new APIBaseResponse<LoginResponseDto>
                {
                    ResponseCode = ResponseCodes.UNAUTHORIZED,
                    ErrorMessage = new List<string> { "Invalid email or password." }
                };
            }

            var token = _jwtTokenGenerator.GenerateToken(user.Data);

            return new APIBaseResponse<LoginResponseDto>
            {
                Data = new LoginResponseDto
                {
                    Token = token,
                    Email = user.Data.Email,
                    Name = user.Data.Name
                },
                ResponseCode = ResponseCodes.SUCCESS
            };
        }

    }
}
