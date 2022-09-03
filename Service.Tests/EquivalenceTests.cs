using Xunit;
using Models;
using Services;

namespace Service.Tests
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
            bool indicator = false;

            Account account = accountDictionary[newClient].Last();           
            if (account != null) indicator = true;

            // Assert
            Assert.True(indicator);
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

            // Act
            bool indicator = false;
            if (employeeList.Contains(employee)) indicator = true;

            // Assert
            Assert.True(indicator);
        }
    }
}