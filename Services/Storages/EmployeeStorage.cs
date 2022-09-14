using Models;

namespace Services.Storages
{
    public class EmployeeStorage : IStorage<Employee>
    {
        public readonly List<Employee> Employees = new List<Employee>();

        public void Add(Employee item)
        {
            Employees.Add(item);
        }

        public void Delete(Employee item)
        {
            Employees.Remove(item);
        }

        public void Update(Employee item)
        {
            Employee employee = Employees.First(x => x.PassportId == item.PassportId);

            employee.Name = item.Name;
            employee.LastName = item.LastName;
            employee.Salary = item.Salary;
            employee.Contract = item.Contract;
        }

    }
}