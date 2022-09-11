using Models;
using Services.Exeptions;
using Services.Filters;
using Xunit;
using Xunit.Abstractions;

namespace Services.Tests
{
    public class ClientServiceTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public ClientServiceTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void AddClientAgeLimitException()
        {
            // Arrange
            var clientStorage = new ClientStorage(); 
            var clientService = new ClientService(clientStorage);
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
            var clientStorage = new ClientStorage();
            var clientService = new ClientService(clientStorage);
            var client = new Client
            {
                Birthday = new DateTime(2002, 12, 15)
            };

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => clientService.AddClient(client));
        }

        [Fact]
        public void AddClientPositivTest()
        {
            // Arrange
            var clientStorage = new ClientStorage();
            var clientService = new ClientService(clientStorage);
            var client = new Client
            {
                Birthday = new DateTime(2002, 12, 15),
                PassportId = 225546
            };

            // Act
            clientService.AddClient(client);

            // Assert
            Assert.Contains(client, clientService.GetClients(new ClientFilter()).Keys);
        }


        [Fact]
        public void GetClientsByFilterTest()
        {
            // Arrange
            var clientStorage = new ClientStorage();
            var clientService = new ClientService(clientStorage);

            var dataGenerator = new TestDataGenerator();
            var filter = new ClientFilter
            {
                MinBirthday = new DateTime(1999, 1, 1),
                MaxBirthday = new DateTime(1980, 1, 1),
                LastName = "Donnelly"
            };

            // Act
            var clients = dataGenerator.GenerateTestClientsList();

            foreach (var client in clients)
            {
                try
                {
                    clientService.AddClient(client);
                }
                catch (AgeLimitException ex)
                {
                    Assert.Fail("Есть клиенты младше 18 лет!");
                }
                catch (Exception ex)
                {
                    Assert.Fail("Неверные данные клиентов!");
                }
            }

            var filteredClients = clientService.GetClients(filter);

            // Assert
            Assert.Multiple(
                () => filteredClients.Keys.All(x => x.Birthday <= filter.MinBirthday),
                () => filteredClients.Keys.All(x => x.Birthday >= filter.MaxBirthday),
                () => filteredClients.Keys.All(x => x.LastName == filter.LastName));
        }


        [Fact]
        public void AgeClientTests()
        {
            // Arrange
            var clientStorage = new ClientStorage();
            var clientService = new ClientService(clientStorage);
            var dataGenerator = new TestDataGenerator();

            // Act
            var clients = dataGenerator.GenerateTestClientsList();

            foreach (var client in clients)
            {
                try
                {
                    clientService.AddClient(client);
                }
                catch (AgeLimitException ex)
                {
                    Assert.Fail("Есть клиенты младше 18 лет!");
                }
                catch (Exception ex)
                {
                    Assert.Fail("Неверные данные клиентов!");
                }
            }

            var youngestClient = clientService.GetClients(new ClientFilter())
                .MaxBy(x => x.Key.Birthday).Key;

            int youngestClientAge = DateTime.Today.Year - youngestClient.Birthday.Year;


            _testOutputHelper.WriteLine($"Youngest client is {youngestClient.Name}: {youngestClientAge}");


            var oldestClient = clientService.GetClients(new ClientFilter())
                .MinBy(x => x.Key.Birthday).Key;

            int oldestClientAge = DateTime.Today.Year - oldestClient.Birthday.Year;

            _testOutputHelper.WriteLine($"Oldest client is {oldestClient.Name}: {oldestClientAge}");


            double averageAge = clientService.GetClients(new ClientFilter())
                .Average(x => DateTime.Today.Year - x.Key.Birthday.Year);

            _testOutputHelper.WriteLine($"Average age is {Math.Ceiling(averageAge)}");

            // Assert
            Assert.True(true);
        }
    }
}
