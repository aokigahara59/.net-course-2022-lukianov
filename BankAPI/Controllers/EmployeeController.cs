using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace BankAPI.Controllers
{
    [ApiController]
    [Route("EmployeeController")]
    public class EmployeeController
    {
        private EmployeeService _employeeService;

        public EmployeeController()
        {
            _employeeService = new EmployeeService();
        }


        [HttpPost]
        public async void AddEmployee(Employee employee)
        {
            await _employeeService.AddEmployeeAsync(employee);
        }


        [HttpGet]
        public Employee GetEmployee(Guid id)
        {
            return _employeeService.GetEmployee(id);
        }


        [HttpPut]
        public async void UpdateEmployee(Guid id, Employee employee)
        {
            await _employeeService.UpdateEmployeeAsync(id, employee);
        }


        [HttpDelete]
        public async void DeleteEmployee(Guid id)
        {
            await _employeeService.DeleteEmployeeAsync(id);
        }
    }
}
