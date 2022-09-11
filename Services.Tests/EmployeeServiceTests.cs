using Models;
using Services.Exeptions;
using Services.Filters;
using Xunit;
using Xunit.Abstractions;

namespace Services.Tests
{
    public class EmployeeServiceTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public EmployeeServiceTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void AddEmployeeAgeLimitException()
        {
            // Arrange
            var employeeStorage = new EmployeeStorage();
            var employeeService = new EmployeeService(employeeStorage);
            var employee = new Employee
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
            var employeeStorage = new EmployeeStorage();
            var employeeService = new EmployeeService(employeeStorage);
            var employee = new Employee
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
            var employeeStorage = new EmployeeStorage();
            var employeeService = new EmployeeService(employeeStorage);
            var employee = new Employee
            {
                Birthday = new DateTime(2002, 12, 15),
                PassportId = 225546
            };

            // Act
            employeeService.AddEmployee(employee);

            // Assert
            Assert.Contains(employee, employeeService.GetEmployees(new EmployeeFilter()));
        }


        [Fact]
        public void GetClientsByFilterTest()
        {
            // Arrange
            var employeeStorage = new EmployeeStorage();
            var employeeService = new EmployeeService(employeeStorage);

            var dataGenerator = new TestDataGenerator();
            var filter = new EmployeeFilter
            {
                MinBirthday = new DateTime(1999, 1, 1),
                MaxBirthday = new DateTime(1980, 1, 1),
                LastName = "Donnelly"
            };

            // Act
            var employees = dataGenerator.GenerateTestEmployeeList();

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
