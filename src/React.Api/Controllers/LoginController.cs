using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using React.DAL.Interface.User;

namespace React.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserService _userService;

        public LoginController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] React.Domain.DTOs.Request.Login.LoginRequest loginDto)
        {
            var response = await _userService.LoginAsync(loginDto.Email, loginDto.Password);
            return Ok(response);
        }
    }
}
