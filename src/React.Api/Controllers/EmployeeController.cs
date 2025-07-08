using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using React.DAL.Interface.Employee;
using React.Domain.Common;
using React.Domain.Models.Employee;
using System.Threading.Tasks;

namespace React.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost("GetAllEmployees")]
        public async Task<IActionResult> GetAllEmployees([FromBody] FilterDto? filter)
        {
            var result = await _employeeService.GetAllEmployeesAsync(filter);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var filter = new FilterDto();
            filter.Predicates.Add(nameof(Employee.Id), id);

            var result = await _employeeService.GetEmployeeByIdAsync(filter);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] Employee employee)
        {
            var result = await _employeeService.AddEmployeeAsync(employee);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEmployee([FromBody] Employee employee)
        {
            var result = await _employeeService.UpdateEmployeeAsync(employee);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var filter = new FilterDto();
            filter.Predicates.Add(nameof(Employee.Id), id);

            var result = await _employeeService.DeleteEmployeeAsync(filter);
            return Ok(result);
        }
    }
}
