using Microsoft.AspNetCore.Mvc;
using Models;
using Services;
using Services.Exeptions;

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
        public async Task<ActionResult> AddEmployee(Employee employee)
        {
            try
            {
                await _employeeService.AddEmployeeAsync(employee);
                return new OkResult();
            }
            catch (AgeLimitException ex)
            {
                return new ForbidResult();
            }
            catch (ArgumentNullException ex)
            {
                return new ForbidResult();
            }
        }


        [HttpGet]
        public ActionResult<Employee> GetEmployee(Guid id)
        {
            try
            {
                return _employeeService.GetEmployee(id);
            }
            catch (NullReferenceException ex)
            {
                return new NotFoundResult();
            }
        }


        [HttpPut]
        public async Task<ActionResult> UpdateEmployee(Guid id, Employee employee)
        {
            try
            {
                await _employeeService.UpdateEmployeeAsync(id, employee);
                return new OkResult();
            }
            catch (NullReferenceException ex)
            {
                return new NotFoundResult();
            }
        }


        [HttpDelete]
        public async Task<ActionResult> DeleteEmployee(Guid id)
        {
            try
            {
                await _employeeService.DeleteEmployeeAsync(id);
                return new OkResult();
            }
            catch (NullReferenceException ex)
            {
                return new NotFoundResult();
            }
        }
    }
}
