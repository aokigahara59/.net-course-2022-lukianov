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
            throw new NotImplementedException();
        }

        public void Update(Employee item)
        {
            throw new NotImplementedException();
        }

    }
}