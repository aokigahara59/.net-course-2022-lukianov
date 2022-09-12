using Models;

namespace Services
{
    public class EmployeeStorage
    {
        public readonly List<Employee> Employees = new List<Employee>();

        public void Add(Employee employee)
        {
            Employees.Add(employee);
        }
    }
}
