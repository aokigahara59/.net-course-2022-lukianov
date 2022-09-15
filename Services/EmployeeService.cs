using Models;
using Services.Exeptions;
using Services.Filters;
using Services.Storages;

namespace Services
{
    public class EmployeeService
    {
        private EmployeeStorage _storage;

        public EmployeeService(EmployeeStorage storage)
        {
            _storage = storage;
        }

        public void AddEmployee(Employee employee)
        {
            if (employee.Birthday > new DateTime(2004, 1, 1))
            {
                throw new AgeLimitException("Сотрудник должен быть старше 18 лет!");
            }

            if (employee.PassportId == 0) throw new ArgumentNullException("employee сотрудника нет паспортных данных!");

            _storage.Add(employee);
        }

        public List<Employee> GetEmployees(EmployeeFilter filter)
        {
            var request = _storage.Employees.AsEnumerable();

            if (filter.PassportId != 0)
            {
                request = request.Where(x => x.PassportId == filter.PassportId);
            }

            if (filter.LastName != null)
            {
                request = request.Where(x => x.LastName == filter.LastName);
            }

            if (filter.MinBirthday != default(DateTime))
            {
                request = request.Where(x => x.Birthday <= filter.MinBirthday);
            }

            if (filter.MaxBirthday != default(DateTime))
            {
                request = request.Where(x => x.Birthday >= filter.MaxBirthday);
            }

            if (filter.Contract != null)
            {
                request = request.Where(x => x.Contract == filter.Contract);
            }

            return request.ToList();
        }
    }
}
