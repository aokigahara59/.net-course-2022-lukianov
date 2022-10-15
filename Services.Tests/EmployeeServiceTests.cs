using Models;
using ModelsDb;
using Services.Exeptions;
using Services.Filters;
using Xunit;

namespace Services.Tests
{
    public class EmployeeServiceTests
    {
        [Fact]
        public void AddEmployeeAgeLimitException()
        {
            // Arrange
            var employeeService = new EmployeeService();
            var employee = new Employee
            {
                Birthday = new DateTime(2005, 12, 15)
            };

            // Act and Assert
            Assert.ThrowsAsync<AgeLimitException>(() => employeeService.AddEmployee(employee));
        }

        [Fact]
        public void AddEmployeeArgumentNullException()
        {
            // Arrange
            var employeeService = new EmployeeService();
            var employee = new Employee
            {
                Birthday = new DateTime(2002, 12, 15)
            };

            // Act and Assert
            Assert.ThrowsAsync<ArgumentNullException>(() => employeeService.AddEmployee(employee));
        }

        [Fact]
        public async void AddEmployeePositivTest()
        {
            // Arrange
            var employeeService = new EmployeeService();
            var employee = new Employee
            {
                Name = "John",
                LastName = "Loye",
                Contract = "Jonh Loye is hired for next 3 years",
                Salary = 555,
                Bonus = 0,
                Birthday = new DateTime(2002, 12, 15).ToUniversalTime(),
                PassportId = 225546
            };

            // Act
            await employeeService.AddEmployee(employee);

            // Assert
            Assert.Contains(employee, employeeService.GetEmployees(new EmployeeFilter()));
        }

        [Fact]
        public void GetEmployeeByIdPositivTest()
        {
            // Arrange
            Guid id = Guid.Parse("67e63271-06cc-9d5f-2026-55521a86c012");
            var employeeService = new EmployeeService();

            // Act
            var employee = employeeService.GetEmployee(id);

            // Assert
            Assert.NotNull(employee);
        }

        [Fact]
        public async void DeleteEmployeePositivTest()
        {
            // Arrange
            Guid id = Guid.Parse("c477f923-3a2f-473c-8d40-e93736b36f6c");
            var employeeService = new EmployeeService();

            // Act
            await employeeService.DeleteEmployee(id);

            // Assert
            Assert.Throws<NullReferenceException>(() => employeeService.GetEmployee(id));
        }

        [Fact]
        public async void UpdateEmployeePositivTest()
        {
            // Arrange
            Guid id = Guid.Parse("67e63271-06cc-9d5f-2026-55521a86c012");
            var employeeService = new EmployeeService();
            var oldEmployee = employeeService.GetEmployee(id);

            // Act
            var newEmployeeData = new Employee
            {
                Name = "Stepan",
                LastName = "Igorev",
                Bonus = 5,
            };

            await employeeService.UpdateEmployee(id, newEmployeeData);

            // Assert
            Assert.Multiple(() => oldEmployee.Name.Equals(newEmployeeData.Name),
                () => oldEmployee.LastName.Equals(newEmployeeData.LastName),
                () => oldEmployee.Bonus.Equals(newEmployeeData.Bonus));
        }


        [Fact]
        public async void GetEmployeesByFilterTest()
        {
            // Arrange
            var employeeService = new EmployeeService();
            var dataGenerator = new TestDataGenerator();
            var filter = new EmployeeFilter
            {
                MinBirthday = new DateTime(1999, 1, 1).ToUniversalTime(),
                MaxBirthday = new DateTime(1960, 1, 1).ToUniversalTime(),
                LastName = "Kutch"
            };

            // Act
            var employees = dataGenerator.GenerateTestEmployeeList();

            foreach (var employee in employees)
            {
                try
                {
                    await employeeService.AddEmployee(employee);
                }
                catch (AgeLimitException ex)
                {
                    Assert.Fail("Есть сотрудники младше 18 лет!");
                }
                catch (Exception ex)
                {
                    Assert.Fail("Неверные данные сотрудкиков!");
                }
            }

            var filteredEmployees = employeeService.GetEmployees(filter);

            // Assert
            Assert.Multiple(
                () => filteredEmployees.All(x => x.Birthday <= filter.MinBirthday),
                () => filteredEmployees.All(x => x.Birthday >= filter.MaxBirthday),
                () => filteredEmployees.All(x => x.LastName == filter.LastName));
        }

    }
}
