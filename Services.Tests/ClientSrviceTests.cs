using Models;
using Services.Exeptions;
using Xunit;

namespace Services.Tests
{
    public class ClientSrviceTests
    {

        [Fact]
        public void AddClientAgeLimitException()
        {
            // Arrange
            var clientService = new ClientService();
            var client = new Client
            {
                Birthday = new DateTime(2005, 12, 15)
            };

            // Act and Assert
            Assert.Throws<AgeLimitException>(() => clientService.AddClient(client));
        }

        [Fact]
        public void AddClientArgumentNullException()
        {
            // Arrange
            var clientService = new ClientService();
            var client = new Client
            {
                Birthday = new DateTime(2002, 12, 15)
            };

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => clientService.AddClient(client));
        }
    }
}
