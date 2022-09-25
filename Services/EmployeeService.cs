using ModelsDb;
using Services.Exeptions;
using Services.Filters;

namespace Services
{
    public class EmployeeService
    {
        private ApplicationContext _dbContext;

        public EmployeeService()
        {
            _dbContext = new ApplicationContext();
        }

        public EmployeeDb GetEmployee(Guid id)
        {
            return _dbContext.Employees.FirstOrDefault(x => x.Id == id);
        }

        public void AddEmployee(EmployeeDb employee)
        {
            if (employee.Birthday > new DateTime(2004, 1, 1))
            {
                throw new AgeLimitException("Сотрудник должен быть старше 18 лет!");
            }

            if (employee.PassportId == 0) throw new ArgumentNullException("employee сотрудника нет паспортных данных!");

            _dbContext.Employees.Add(employee);
            _dbContext.SaveChanges();
        }

        public void UpdateEmployee(Guid id, EmployeeDb employee)
        {
            var oldEmployee = GetEmployee(id);

            if (employee.Name != null)
            {
                oldEmployee.Name = employee.Name;
            }

            if (employee.LastName != null)
            {
                oldEmployee.LastName = employee.LastName;
            }

            if (employee.Birthday != default(DateTime))
            {
                oldEmployee.Birthday = employee.Birthday;
            }

            if (employee.PassportId != 0)
            {
                oldEmployee.PassportId = employee.PassportId;
            }

            if (employee.Salary != 0)
            {
                oldEmployee.Salary = employee.Salary;
            }

            if (employee.Contract != null)
            {
                oldEmployee.Contract = employee.Contract;
            }

            _dbContext.SaveChanges();
        }

        public void DeleteEmployee(Guid id)
        {
            _dbContext.Employees.Remove(GetEmployee(id));
            _dbContext.SaveChanges();
        }


        public List<EmployeeDb> GetEmployees(EmployeeFilter filter)
        {
            var request = _dbContext.Employees.AsQueryable();

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

            if (filter.Offset != 0)
            {
                request.Skip(filter.Offset);
            }

            if (filter.Limit != 0)
            {
                request.Take(filter.Limit);
            }

            return request.ToList();
        }
    }
}
