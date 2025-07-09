using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using React.DAL.Interface.UserRole;

[ApiController]
[Route("api/[controller]")]
public class UserRoleController : ControllerBase
{
    private readonly IUserRoleService _userRoleService;

    public UserRoleController(IUserRoleService userRoleService)
    {
        _userRoleService = userRoleService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUserRoles()
    {
        var result = await _userRoleService.GetAllAsync();
        return Ok(result);
    }
}
