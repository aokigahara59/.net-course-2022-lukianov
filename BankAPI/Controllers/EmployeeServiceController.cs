using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace BankAPI.Controllers
{
    [ApiController]
    [Route("EmployeeServiceController")]
    public class EmployeeServiceController
    {
        private EmployeeService _employeeService;

        public EmployeeServiceController()
        {
            _employeeService = new EmployeeService();
        }


        [HttpPost(Name = "AddEmployee")]
        public async void AddEmployee(Employee employee)
        {
            await _employeeService.AddEmployeeAsync(employee);
        }


        [HttpGet(Name = "GetEmployee")]
        public Employee GetEmployee(Guid id)
        {
            return _employeeService.GetEmployee(id);
        }


        [HttpPut(Name = "UpdateEmployee")]
        public async void UpdateEmployee(Guid id, Employee employee)
        {
            await _employeeService.UpdateEmployeeAsync(id, employee);
        }


        [HttpDelete(Name = "DeleteEmployee")]
        public async void DeleteEmployee(Guid id)
        {
            await _employeeService.DeleteEmployeeAsync(id);
        }
    }
}
