using React.DAL.Implementation.Common;
using React.DAL.Implementation.Jwt;
using React.DAL.Interface.AppUser;
using React.DAL.Interface.Common;
using React.Domain.Common;
using React.Domain.DTOs.Response.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace React.DAL.Implementation.AppUser
{
    public class AppUserService : IAppUserService
    {
        private readonly IGenericRepository<React.Domain.Models.AppUser.AppUser> _appUserRepository;
        private readonly JwtTokenGenerator _jwtTokenGenerator;
        public AppUserService(IGenericRepository<React.Domain.Models.AppUser.AppUser> appUserRepository, JwtTokenGenerator jwtTokenGenerator)
        {
            _appUserRepository = appUserRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<APIBaseResponse<React.Domain.Models.AppUser.AppUser>> AddAppUserAsync(React.Domain.Models.AppUser.AppUser user)
        {
            var response = await _appUserRepository.AddAsync(user);

            if (response.ResponseCode == ResponseCodes.CREATED)
            {
                response.AddInfo("User created successfully.");
            }
            return response;
        }

        public async Task<APIBaseResponse<LoginResponseDto>> LoginAsync(string email, string password)
        {

            FilterDto filter = new FilterDto();
            filter.AddPredicateIfNotNull(nameof(React.Domain.Models.AppUser.AppUser.Email), email);
            filter.AddPredicateIfNotNull(nameof(React.Domain.Models.AppUser.AppUser.Password), password);

            var userResponse = await _appUserRepository.GetByIdAsync(filter);

            if (userResponse.ResponseCode != ResponseCodes.SUCCESS || userResponse.Data == null)
            {
                return new APIBaseResponse<LoginResponseDto>
                {
                    ResponseCode = ResponseCodes.UNAUTHORIZED,
                    ErrorMessage = new List<string> { "Invalid credentials" }
                };
            }

            var token = _jwtTokenGenerator.GenerateToken(userResponse.Data); // optionally generate token

            return new APIBaseResponse<LoginResponseDto>
            {
                ResponseCode = ResponseCodes.SUCCESS,
                Data = new LoginResponseDto
                {
                    Token = token,
                    Email = userResponse.Data.Email,
                    Name = userResponse.Data.FirstName + " " + userResponse.Data.LastName,
                    AppUser = userResponse.Data
                },
                SuccessMessage = new List<string> { "Login Succesfully." }
            };
        }

    }
}
