using Models;
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

        private EmployeeDb ConvertEmployeeToEmployeeDb(Employee employee)
        {
            return new EmployeeDb
            {
                Name = employee.Name,
                LastName = employee.LastName,
                Birthday = employee.Birthday.ToUniversalTime(),
                PassportId = employee.PassportId,
                Bonus = employee.Bonus,
                Salary = employee.Salary,
                Contract = employee.Contract
            };
        }

        private Employee ConvertEmployeeDbToEmployee(EmployeeDb employee)
        {
            return new Employee
            {
                Name = employee.Name,
                LastName = employee.LastName,
                Birthday = employee.Birthday.ToUniversalTime(),
                PassportId = employee.PassportId,
                Bonus = employee.Bonus,
                Salary = employee.Salary,
                Contract = employee.Contract
            };
        }

        public Employee GetEmployee(Guid id)
        {
            EmployeeDb employeeDb = _dbContext.Employees.FirstOrDefault(x => x.Id == id);

            if (employeeDb == null) throw new NullReferenceException("Такого сотрудника нет!");

            return ConvertEmployeeDbToEmployee(employeeDb);
        }

        private EmployeeDb GetEmployeeDb(Guid id)
        {
            EmployeeDb employeeDb = _dbContext.Employees.FirstOrDefault(x => x.Id == id);

            if (employeeDb == null) throw new NullReferenceException("Такого сотрудника нет!");

            return employeeDb;
        }

        public void AddEmployee(Employee employee)
        {
            if (employee.Birthday > new DateTime(2004, 1, 1))
            {
                throw new AgeLimitException("Сотрудник должен быть старше 18 лет!");
            }

            if (employee.PassportId == 0) throw new ArgumentNullException("employee сотрудника нет паспортных данных!");

            _dbContext.Employees.Add(ConvertEmployeeToEmployeeDb(employee));
            _dbContext.SaveChanges();
        }

        public void UpdateEmployee(Guid id, Employee employee)
        {
            EmployeeDb oldEmployee = ConvertEmployeeToEmployeeDb(GetEmployee(id));

            if (employee.Name != null)
            {
                _dbContext.Employees.Update(oldEmployee).Entity.Name = employee.Name;
                _dbContext.SaveChanges();
            }

            if (employee.LastName != null)
            {
               _dbContext.Employees.Update(oldEmployee).Entity.LastName = employee.LastName;
               _dbContext.SaveChanges();
            }

            if (employee.Birthday != default(DateTime))
            {
                _dbContext.Employees.Update(oldEmployee).Entity.Birthday = employee.Birthday;
                _dbContext.SaveChanges();
            }

            if (employee.PassportId != 0)
            {
                _dbContext.Employees.Update(oldEmployee).Entity.PassportId = employee.PassportId;
                _dbContext.SaveChanges();
            }

            if (employee.Salary != 0)
            {
                _dbContext.Employees.Update(oldEmployee).Entity.Salary = employee.Salary;
                _dbContext.SaveChanges();
            }

            if (employee.Contract != null)
            {
                _dbContext.Employees.Update(oldEmployee).Entity.Contract = employee.Contract;
                _dbContext.SaveChanges();
            }

        }

        public void DeleteEmployee(Guid id)
        {
            _dbContext.Employees.Remove(GetEmployeeDb(id));
            _dbContext.SaveChanges();
        }


        public List<Employee> GetEmployees(EmployeeFilter filter)
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

            List<Employee> employees = new List<Employee>();
            foreach (var employeeDb in request)
            {
                employees.Add(ConvertEmployeeDbToEmployee(employeeDb));
            }

            return employees;
        }
    }
}
