using Models;
using Services.Exeptions;
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
            Assert.Throws<AgeLimitException>(() => employeeService.AddEmployee(employee));
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
            Assert.Throws<ArgumentNullException>(() => employeeService.AddEmployee(employee));
        }
    }
}
