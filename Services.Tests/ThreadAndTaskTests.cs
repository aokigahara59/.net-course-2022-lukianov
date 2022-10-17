using System.ComponentModel;
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
        public async void AsyncThreadPoolUseTest()
        {
            // Arrange
            var clientService = new ClientService();
            var extraClientService = new ClientService();


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
            ThreadPool.SetMaxThreads(7, 7);
            
            ThreadPool.GetMaxThreads(out var worker, out var completions);

            _testOutputHelper.WriteLine("Max threads: " + worker);


            async Task CreateTask()
            {
                await Task.Run(() =>
                {
                    var clients = clientService.GetClients(new ClientFilter());
                    Task.Delay(1000).Wait();
                });
            }


            for (int i = 0; i < 10; i++)
            {
                await CreateTask();
                ThreadPool.GetAvailableThreads(out var currentThreads, out _);
                
                _testOutputHelper.WriteLine("Current threads: " + currentThreads);
            }

            await Task.Run(async () =>
            {
                 await extraClientService.AddClientAsync(client);
            });


            // Assert
            Assert.Contains(client, extraClientService.GetClients(new ClientFilter()));
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

                importedClients.ForEach(x => clientService.AddClientAsync(x));
                
            });
            importThread.Start();

            Thread.Sleep(20000);

            var clientsFromDb = clientService.GetClients(new ClientFilter());

            // Assert
            Assert.Equal(clientsForExport, exportService.ImportClients(directory, fileFromExport));
            Assert.Multiple( () => exportService.ImportClients(directory, fileForImport).ForEach(x => clientsFromDb.Contains(x)) );
            
        }


        [Fact]
        public void RateUpdaterPositivTest()
        {
            // Arrange
            var cancellationSource = new CancellationTokenSource();
            var cancelToken = cancellationSource.Token;

            var rateUpdater = new RateUpdater();


            // Act
            Task task = rateUpdater.Start(cancelToken);

            task.Wait(20000);
            cancellationSource.Cancel();


            // Assert
        }

        [Fact]
        public void CashDispenserServicePositivTest()
        {
            // Arrange
            var cashdispencer = new CashDispenserService();
            var clientService = new ClientService();

            // Act
            var clietns = clientService.GetClients(new ClientFilter { Limit = 15 });

            clietns.ForEach(x => cashdispencer.CashOut(x));

            Thread.Sleep(30000);
        }

    }
}
