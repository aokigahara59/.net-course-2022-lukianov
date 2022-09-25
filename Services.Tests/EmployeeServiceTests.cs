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
            var employee = new EmployeeDb()
            {
                Birthday = new DateTime(2005, 12, 15)
            };

            // Act and Assert
            Assert.Throws<AgeLimitException>(() => employeeService.AddEmployee(employee));
        }

        [Fact]
        public void AddEmployeeArgumentNullException()
        {
            // Arrange
            var employeeService = new EmployeeService();
            var employee = new EmployeeDb
            {
                Birthday = new DateTime(2002, 12, 15)
            };

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => employeeService.AddEmployee(employee));
        }

        [Fact]
        public void AddEmployeePositivTest()
        {
            // Arrange
            var employeeService = new EmployeeService();
            var employee = new EmployeeDb
            {
                Id = new Guid(),
                Name = "John",
                LastName = "Loye",
                Contract = "Jonh Loye is hired for next 3 years",
                Salary = 555,
                Bonus = 0,
                Birthday = new DateTime(2002, 12, 15).ToUniversalTime(),
                PassportId = 225546
            };

            // Act
            employeeService.AddEmployee(employee);

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
        public void DeleteEmployeePositivTest()
        {
            // Arrange
            Guid id = Guid.Parse("645135de-8bf5-2cec-e409-4932d4ebb587");
            var employeeService = new EmployeeService();

            // Act
            employeeService.DeleteEmployee(id);

            // Assert
            Assert.Null(employeeService.GetEmployee(id));
        }

        [Fact]
        public void UpdateEmployeePositivTest()
        {
            // Arrange
            Guid id = Guid.Parse("67e63271-06cc-9d5f-2026-55521a86c012");
            var employeeService = new EmployeeService();
            var oldEmployee = employeeService.GetEmployee(id);

            // Act
            var newEmployeeData = new EmployeeDb
            {
                Name = "Stepan",
                LastName = "Igorev",
                Bonus = 5,
            };

            employeeService.UpdateEmployee(id, newEmployeeData);

            // Assert
            Assert.Multiple(() => oldEmployee.Name.Equals(newEmployeeData.Name),
                () => oldEmployee.LastName.Equals(newEmployeeData.LastName),
                () => oldEmployee.Bonus.Equals(newEmployeeData.Bonus));
        }


        [Fact]
        public void GetClientsByFilterTest()
        {
            // Arrange
            var employeeService = new EmployeeService();
            var dataGenerator = new TestDataGenerator();
            var filter = new EmployeeFilter
            {
                MinBirthday = new DateTime(1999, 1, 1).ToUniversalTime(),
                MaxBirthday = new DateTime(1980, 1, 1).ToUniversalTime(),
                LastName = "Donnelly"
            };

            // Act
            var employees = dataGenerator.GenerateTestEmployeeDbList();

            foreach (var employee in employees)
            {
                try
                {
                    employeeService.AddEmployee(employee);
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
