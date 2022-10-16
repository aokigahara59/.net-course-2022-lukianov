using Models;

namespace Services
{
    public class CashDispenserService
    {
        public async Task CashOut(Client client)
        {
            await Task.Run(async () =>
            {
                var clientService = new ClientService();

                var account = clientService.GetClientAccounts(client).FirstOrDefault();

                for (int i = 1; i <= 10; i++)
                {
                    await clientService.UpdateAccount(client, account, new Account{Amount = account.Amount - 10});

                    await Task.Delay(1000);
                }

            });
        }
    }
}
