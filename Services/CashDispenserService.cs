using Models;

namespace Services
{
    public class CashDispenserService
    {
        public Task CashOut(Client client)
        {
            return Task.Run(() =>
            {
                var clientService = new ClientService();

                var account = clientService.GetClientAccounts(client).FirstOrDefault();

                for (int i = 1; i <= 10; i++)
                {
                    clientService.UpdateAccount(client, account, new Account{Amount = account.Amount - 10});

                    Task.Delay(1000);
                }

            });
        }
    }
}
