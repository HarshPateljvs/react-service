using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using React.Api.Utils;
using React.DAL.Interface.AppUser;
using React.Domain.DTOs.Request.Login;
using React.Domain.Models.AppUser;

namespace React.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAppUserService _appUserService;

        public AuthController(IAppUserService appUserService)
        {
            _appUserService = appUserService;
        }
        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAppUser([FromBody] AppUser user)
        {
            user.Password = Crypto.Encrypt(user.Password);
            var result = await _appUserService.AddAppUserAsync(user);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            request.Password = Crypto.Encrypt(request.Password);
            var result = await _appUserService.LoginAsync(request.Email, request.Password);
            return Ok(result);
        }
    }
}
