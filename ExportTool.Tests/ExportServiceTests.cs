using Models;
using Services;
using Services.Filters;
using Xunit;

namespace ExportTool.Tests
{
    public class ExportServiceTests
    {

        [Fact]
        public async void ImportExportPositiveTest()
        {
            // Arrange
            TestDataGenerator dataGenerator = new TestDataGenerator();
            ExportService exportService = new ExportService();
            ClientService clientService = new ClientService();

            string directory = Path.Combine("F:", "Курсы", ".net-course-2022-lukianov", "Tools", "Data");
            string fileName = "clientdata.csv";

            // Act
            var generatedClients = dataGenerator.GenerateTestClientsList(20);

            await exportService.ExportClientData(generatedClients, directory, fileName);

            var importedClients = exportService.ImportClients(directory, fileName);

            foreach (var client in importedClients)
            {
                await clientService.AddClientAsync(client);
            }

            var clientsFromDb = clientService.GetClients(new ClientFilter());

            // Assert
            Assert.Multiple(() => generatedClients.ForEach(x => clientsFromDb.Contains(x)));

        }


        [Fact]
        public async void JsonSerializationsTest()
        {
            // Arrange
            var exportService = new ExportService();

            var client = new Client
            {
                Name = "Stepan",
                LastName = "Chibby",
                PhoneNumber = "+373(777)7-14-15",
                Bonus = 3
            };

            string directory = Path.Combine("F:", "Курсы", ".net-course-2022-lukianov", "Tools", "Data");
            string fileName = "jsonclient.json";

            // Act

            await exportService.ExportPersonToJson<Client>(client, directory, fileName);

            var importedClient = await exportService.ImportPersonFromJson<Client>(directory, fileName);

            // Assert
            Assert.Equal(client, importedClient);

        }

    }
}