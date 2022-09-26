using Models;
using ModelsDb;
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

        [Fact]
        public void AddClientPositivTest()
        {
            // Arrange
            var clientService = new ClientService();
            var client = new Client
            {
                Name = "John",
                LastName = "Loye",
                PhoneNumber = "+37377775544",
                Bonus = 0,
                Birthday = new DateTime(2002, 12, 15).ToUniversalTime(),
                PassportId = 225546
            };

            // Act
            clientService.AddClient(client);

            // Assert
            Assert.Contains(client, clientService.GetClients(new ClientFilter()));
        }


        [Fact]
        public void GetClientByIdPositivTest()
        {
            // Arrange
            Guid id = Guid.Parse("8175b038-c57c-fa04-8f7c-155f0b531224");
            var clientService = new ClientService();

            // Act
            var client = clientService.GetClient(id);

            // Assert
            Assert.NotNull(client);
        }

        [Fact]
        public void DeleteClientPositivTest()
        {
            // Arrange
            Guid id = Guid.Parse("c93666d5-f92d-df9c-b432-f8ecf8fe0744");
            var clientService = new ClientService();

            // Act
            clientService.DeleteClient(id);

            // Assert
            Assert.Null(clientService.GetClient(id));
        }

        [Fact]
        public void AddAccountPositivTest()
        {
            // Arrange
            Guid id = Guid.Parse("5f804c52-e167-b2aa-d232-008f78067738");
            var clientService = new ClientService();

            // Act
            var account = new Account
            {
                Amount = 554,
                Currency = new Currency{ Code = 254, Name = "RUB"}
            };

            clientService.AddAccount(id, account);

            var accountDb = new AccountDb
            {
                Amount = 554,
                CurrencyName = "RUB",
                Client = clientService.GetClientDb(id)
            };

           

            // Assert
            Assert.Contains(accountDb, clientService.GetClientDb(id).Accounts);
        }

        [Fact]
        public void UpdateClientPositivTest()
        {
            // Arrange
            Guid id = Guid.Parse("6e6c9848-15e8-4f91-8b3c-f4e43832f4a2");
            var clientService = new ClientService();
            var oldClient = clientService.GetClient(id);

            // Act
            var newClientsData = new Client
            {
                Name = "Stepan",
                LastName = "Igorev",
                Bonus = 5,
            };

            clientService.UpdateClient(id, newClientsData);

            // Assert
            Assert.Multiple(() => oldClient.Name.Equals(newClientsData.Name),
                () => oldClient.LastName.Equals(newClientsData.LastName),
                () => oldClient.Bonus.Equals(newClientsData.Bonus));
        }


        [Fact]
        public void GetClientsByFilterTest()
        {
            // Arrange
            var clientService = new ClientService();

            var dataGenerator = new TestDataGenerator();
            var filter = new ClientFilter
            {
                MinBirthday = new DateTime(1999, 1, 1).ToUniversalTime(),
                MaxBirthday = new DateTime(1980, 1, 1).ToUniversalTime(),
                LastName = "Donnelly"
            };

            // Act
            var clients = dataGenerator.GenerateTestClientsList(100);


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
                () => filteredClients.All(x => x.Birthday <= filter.MinBirthday),
                () => filteredClients.All(x => x.Birthday >= filter.MaxBirthday),
                () => filteredClients.All(x => x.LastName == filter.LastName));
        }


        [Fact]
        public void AgeClientTests()
        {
            // Arrange
            var clientService = new ClientService();
            var dataGenerator = new TestDataGenerator();

            // Act
            var clients = dataGenerator.GenerateTestClientsList(100);

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
                .MaxBy(x => x.Birthday);

            int youngestClientAge = DateTime.Today.Year - youngestClient.Birthday.Year;


            _testOutputHelper.WriteLine($"Youngest client is {youngestClient.Name}: {youngestClientAge}");


            var oldestClient = clientService.GetClients(new ClientFilter())
                .MinBy(x => x.Birthday);

            int oldestClientAge = DateTime.Today.Year - oldestClient.Birthday.Year;

            _testOutputHelper.WriteLine($"Oldest client is {oldestClient.Name}: {oldestClientAge}");


            double averageAge = clientService.GetClients(new ClientFilter())
                .Average(x => DateTime.Today.Year - x.Birthday.Year);

            _testOutputHelper.WriteLine($"Average age is {Math.Ceiling(averageAge)}");

            // Assert
            Assert.True(true);
        }
    }
}
