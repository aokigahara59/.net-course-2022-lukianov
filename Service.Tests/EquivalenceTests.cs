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

            Account accounts = accountDictionary[newClient].Last();           
            if (accounts != null) indicator = true;


            // Assert
            Assert.True(indicator);
        }
    }
}