using ExportTool;
using Models;
using Services.Filters;
using Xunit;
using Xunit.Abstractions;

namespace Services.Tests
{
    public class ThreadAndTaskTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public ThreadAndTaskTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void ParralelAddMoneyPositivTest()
        {
            // Arrange
            Account account = new Account
            {
                Amount = 0
            };

            // Act
            Object locker = new();

            for (int i = 1; i <= 2; i++)
            {
                var addMoneyThread = new Thread(() =>
                {
                    for (int i = 0; i < 10; i++)
                    {
                        lock (locker)
                        {
                            account.Amount += 100;
                            _testOutputHelper.WriteLine($"{Thread.CurrentThread.Name}: добавление 100$; на счету {account.Amount}");
                        }
                        Thread.Sleep(200);
                    }
                });
                addMoneyThread.Name = $"Thread {i}";
                addMoneyThread.Start();
            }

            Thread.Sleep(10000);

            // Assert
            Assert.Equal(2000, account.Amount);
        }


        [Fact]
        public void ParallelImportExportClientsFromDb()
        {
            // Arrange
            var exportService = new ExportService();
            var clientService = new ClientService();
            var dataGenerator = new TestDataGenerator();

            string directory = Path.Combine("F:", "Курсы", ".net-course-2022-lukianov", "Tools", "Data");
            string fileForImport = "clientsToImport.csv";
            string fileFromExport = "exportedClients.csv";

           


            // Act
            var clientsForExport = clientService.GetClients(new ClientFilter { Limit = 10 });

            var clientsForImport = dataGenerator.GenerateTestClientsList(10);
            exportService.ExportClientData(clientsForImport, directory, fileForImport);

            Thread exportThread = new Thread(() =>
            {

                exportService.ExportClientData(clientsForExport, directory, fileFromExport);

            });

            exportThread.Start();

            Thread importThread = new Thread(() =>
            {

                var importedClients = exportService.ImportClients(directory, fileForImport);

                importedClients.ForEach(x => clientService.AddClient(x));
                
            });
            importThread.Start();

            Thread.Sleep(20000);

            var clientsFromDb = clientService.GetClients(new ClientFilter());

            // Assert
            Assert.Equal(clientsForExport, exportService.ImportClients(directory, fileFromExport));
            Assert.Multiple( () => exportService.ImportClients(directory, fileForImport).ForEach(x => clientsFromDb.Contains(x)) );
            
        }

    }
}
