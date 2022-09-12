using Models;

namespace Services.Storages
{
    public class ClientStorage : IClientStorage
    {
        public Dictionary<Client, List<Account>> Data { get; }
        public ClientStorage()
        {
            Data = new Dictionary<Client, List<Account>>();
        }

        public void Add(Client item)
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

            Data.Add(item, accounts);
        }

        public void AddAccount(Client client, Account account)
        {
            List<Account> accounts = Data[client];
            accounts.Add(account);
            Data.Add(client, accounts);
        }

        public void Delete(Client item)
        {
            Data.Remove(item);
        }

        public void RemoveAccount(Client client, Account account)
        {
            List<Account> accounts = Data[client];
            accounts.Remove(account);
            Data.Add(client, accounts);
        }

        public void Update(Client item)
        {
            if (Data[item] == null) throw new NullReferenceException("Такого клиента нет в списке!");
        }

        public void UpdateAccount(Client client, Account account)
        {
            throw new NotImplementedException();
        }
    }
}

