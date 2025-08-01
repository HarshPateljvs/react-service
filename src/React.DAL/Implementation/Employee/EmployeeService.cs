using React.DAL.Implementation.File;
using React.DAL.Interface.Common;
using React.DAL.Interface.Employee;
using React.DAL.Interface.File;
using React.Domain.Common;
using React.Domain.Models.Employee;
using React.Domain.Models.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace React.DAL.Implementation.Employee
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IGenericRepository<React.Domain.Models.Employee.Employee> _employeeRepo;
        private readonly IFileService _fileRepo;
        public EmployeeService(IGenericRepository<React.Domain.Models.Employee.Employee> employeeRepo, IFileService fileService)
        {
            _employeeRepo = employeeRepo;
            _fileRepo = fileService;
        }

        public async Task<APIBaseResponse<IEnumerable<React.Domain.Models.Employee.Employee>>> GetAllEmployeesAsync(FilterDto? filter)
        {
            var response = await _employeeRepo.GetAllAsync(filter);

            if (response?.Data != null)
            {
                foreach (var emp in response.Data)
                {
                    emp.EmployeeImages = await _fileRepo.GetImagesByModuleAsync("Employee", "Employee", emp.Id);
                }
            }

            return response;
        }

        public async Task<APIBaseResponse<React.Domain.Models.Employee.Employee>> GetEmployeeByIdAsync(FilterDto? filter)
        {

            var response = await _employeeRepo.GetByIdAsync(filter);
            response.Data.EmployeeImages = await _fileRepo.GetImagesByModuleAsync("Employee", "Employee", response.Data.Id);
            return response;
        }

        public async Task<APIBaseResponse<React.Domain.Models.Employee.Employee>> AddEmployeeAsync(React.Domain.Models.Employee.Employee employee)
        {
            var response = await _employeeRepo.AddAsync(employee);

            if (response.ResponseCode == ResponseCodes.CREATED)
            {
                await _fileRepo.ManageImagesAsync("Employee", "Employee", response.Data.Id, employee.EmployeeImages);
                response.AddInfo("Employee created successfully.");
            }
            return response;
        }

        public async Task<APIBaseResponse<React.Domain.Models.Employee.Employee>> UpdateEmployeeAsync(React.Domain.Models.Employee.Employee employee)
        {

            var response = await _employeeRepo.UpdateAsync(employee);
            if (response.ResponseCode == ResponseCodes.UPDATED)
            {
                await _fileRepo.ManageImagesAsync("Employee", "Employee", response.Data.Id, employee.EmployeeImages);
                response.AddInfo("Employee created successfully.");
            }
            return response;
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
            var response = await _employeeRepo.DeleteAsync(existing.Data);
            if (response.ResponseCode == ResponseCodes.SUCCESS)
            {
                response.AddInfo("Employee Deleted successfully.");
            }
            return response;
        }
    }
}
