using Microsoft.AspNetCore.Mvc;
using React.Api.Filter;
using React.DAL.Interface.User;
using React.Domain.Common;
using React.Domain.Models.User;
using System.Threading.Tasks;

namespace React.Api.Controllers
{
    [ApiController] 
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            FilterDto? dto = new FilterDto();
            var result = await _userService.GetAllUsersAsync(dto);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            FilterDto? dto = new FilterDto();
            Domain.Models.User.User obj = new User();
            dto.Predicates.Add(nameof(obj.Id), id);
            var result = await _userService.GetUserByIdAsync(dto);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            var result = await _userService.AddUserAsync(user);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] User user)
        {
            var result = await _userService.UpdateUserAsync(user);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            FilterDto? dto = null;
            Domain.Models.User.User obj = new User();
            dto.Predicates.Add(nameof(obj.Id), id);
            var result = await _userService.DeleteUserAsync(dto);
            return Ok(result);
        }
    }
}
