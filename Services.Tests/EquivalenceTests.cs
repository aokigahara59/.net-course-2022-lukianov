using Models;
using Xunit;

namespace Services.Tests
{
    public class EquivalenceTests
    {

        [Fact]
        public void GetHashCodeNecessityPositivTest()
        {
            // Arrange
            TestDataGenerator dataGenerator = new TestDataGenerator();
            var accountDictionary = dataGenerator.GenerateClientAccountDictionary();
            Client lastClient = accountDictionary.Keys.Last();
            Client newClient = new Client
            {
                Name = lastClient.Name,
                LastName = lastClient.LastName,
                Birthday = lastClient.Birthday,
                PhoneNumber = lastClient.PhoneNumber,
                PassportId = lastClient.PassportId
            };

            // Act
            Account account = accountDictionary[newClient].Last();           

            // Assert
            Assert.NotNull(account);
        }

        [Fact]
        public void EqualsPositivTest()
        {
            // Arrange
            TestDataGenerator dataGenerator = new TestDataGenerator();
            var employeeList = dataGenerator.GenerateTestEmployeeList();
            Employee employeeInList = employeeList.Last();
            Employee employee = new Employee
            {
                Name = employeeInList.Name,
                LastName = employeeInList.LastName,
                PassportId = employeeInList.PassportId,
                Birthday = employeeInList.Birthday,
                Salary = employeeInList.Salary
            };

            // Assert
            Assert.Contains(employee, employeeList);
        }
    }
}