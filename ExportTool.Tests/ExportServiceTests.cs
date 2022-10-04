﻿using Services;
using Services.Filters;
using Xunit;

namespace ExportTool.Tests
{
    public class ExportServiceTests
    {

        [Fact]
        public void ImportExportPositiveTest()
        {
            // Arrange
            TestDataGenerator dataGenerator = new TestDataGenerator();
            ExportService exportService = new ExportService();
            ClientService clientService = new ClientService();

            string directory = Path.Combine("F:", "Курсы", ".net-course-2022-lukianov", "Tools", "Data");
            string fileName = "clientdata.csv";

            // Act
            var generatedClients = dataGenerator.GenerateTestClientsList(20);

            exportService.ExportClientData(generatedClients, directory, fileName);

            var importedClients = exportService.ImportClients(directory, fileName);

            foreach (var client in importedClients)
            {
                clientService.AddClient(client);
            }

            var clientsFromDb = clientService.GetClients(new ClientFilter());

            // Assert
            Assert.Multiple(() => generatedClients.ForEach(x => clientsFromDb.Contains(x)));

        }

    }
}