using Models;
using Services.Exeptions;

namespace Services
{
    public class EmployeeService
    {
        private List<Employee> _employees = new List<Employee>();

        public void AddEmployee(Employee employee)
        {
            if (employee.Birthday > new DateTime(2004, 1, 1))
            {
                throw new AgeLimitException("Сотрудник должен быть старше 18 лет!");
            }

            if (employee.PassportId == 0) throw new ArgumentNullException("employee сотрудника нет паспортных данных!");

            _employees.Add(employee);
        }
    }
}
