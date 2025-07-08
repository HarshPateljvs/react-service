using React.Domain.Common;
using React.Domain.Models.Employee;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace React.DAL.Interface.Employee
{
    public interface IEmployeeService
    {
        Task<APIBaseResponse<IEnumerable<React.Domain.Models.Employee.Employee>>> GetAllEmployeesAsync(FilterDto? filter);
        Task<APIBaseResponse<React.Domain.Models.Employee.Employee>> GetEmployeeByIdAsync(FilterDto? filter);
        Task<APIBaseResponse<React.Domain.Models.Employee.Employee>> AddEmployeeAsync(React.Domain.Models.Employee.Employee employee);
        Task<APIBaseResponse<React.Domain.Models.Employee.Employee>> UpdateEmployeeAsync(React.Domain.Models.Employee.Employee employee);
        Task<APIBaseResponse<React.Domain.Models.Employee.Employee>> DeleteEmployeeAsync(FilterDto? filter);
    }
}
