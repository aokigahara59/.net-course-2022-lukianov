using Models;

namespace Services
{
    public class ClientStorage
    {
        public readonly Dictionary<Client, List<Account>> Clients = new Dictionary<Client, List<Account>>();

        public void Add(Client client)
        {
            Currency currency = new Currency
            {
                Name = "USD",
                Code = 879
            };
            Account account = new Account
            {
                Amount = 0,
                Currency = currency
            };
            var accounts = new List<Account> { account };

            Clients.Add(client, accounts);
        }
    }
}
