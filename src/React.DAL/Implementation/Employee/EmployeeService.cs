using React.DAL.Interface.Employee;
using React.Domain.Common;
using React.Domain.Models.Employee;
using React.DAL.Interface.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace React.DAL.Implementation.Employee
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IGenericRepository<React.Domain.Models.Employee.Employee> _employeeRepo;

        public EmployeeService(IGenericRepository<React.Domain.Models.Employee.Employee> employeeRepo)
        {
            _employeeRepo = employeeRepo;
        }

        public async Task<APIBaseResponse<IEnumerable<React.Domain.Models.Employee.Employee>>> GetAllEmployeesAsync(FilterDto? filter)
        {
            return await _employeeRepo.GetAllAsync(filter);
        }

        public async Task<APIBaseResponse<React.Domain.Models.Employee.Employee>> GetEmployeeByIdAsync(FilterDto? filter)
        {
            return await _employeeRepo.GetByIdAsync(filter);
        }

        public async Task<APIBaseResponse<React.Domain.Models.Employee.Employee>> AddEmployeeAsync(React.Domain.Models.Employee.Employee employee)
        {
            return await _employeeRepo.AddAsync(employee);
        }

        public async Task<APIBaseResponse<React.Domain.Models.Employee.Employee>> UpdateEmployeeAsync(React.Domain.Models.Employee.Employee employee)
        {
            return await _employeeRepo.UpdateAsync(employee);
        }

        public async Task<APIBaseResponse<React.Domain.Models.Employee.Employee>> DeleteEmployeeAsync(FilterDto? filter)
        {
            var existing = await _employeeRepo.GetByIdAsync(filter);
            if (existing.Data == null)
            {
                return new APIBaseResponse<React.Domain.Models.Employee.Employee>
                {
                    ResponseCode = ResponseCodes.NOT_FOUND,
                    ErrorMessage = new List<string> { "Employee not found" }
                };
            }

            return await _employeeRepo.DeleteAsync(existing.Data);
        }
    }
}
