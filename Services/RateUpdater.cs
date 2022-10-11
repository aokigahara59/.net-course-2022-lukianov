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
                        // TODO: client update account
                    }

                    Task.Delay(5000);

                }
            });
        }
    }
}
