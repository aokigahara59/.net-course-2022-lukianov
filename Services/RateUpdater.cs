using Models;
using Services.Filters;

namespace Services
{
    public class RateUpdater
    {

        public Task Start(CancellationToken cancelToken)
        {
            return Task.Run(() =>
            {
                var clientService = new ClientService();
                var clients = clientService.GetClients(new ClientFilter { Limit = 20});

                while (!cancelToken.IsCancellationRequested)
                {
                    foreach (var client in clients)
                    {
                        var accounts = clientService.GetClientAccounts(client);

                        foreach (var account in accounts)
                        {
                            clientService.UpdateAccount(client, account, new Account { Amount = account.Amount + 100 });    
                        }
                    }

                    Task.Delay(5000);

                }
            });
        }
    }
}
