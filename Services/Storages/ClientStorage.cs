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
            Data[client].Add(account);
        }

        public void Delete(Client item)
        {
            Data.Remove(item);
        }

        public void DeleteAccount(Client client, Account account)
        {
            Data[client].Remove(account);
        }

        public void Update(Client item)
        {
            Client clientInData = Data.Keys.First(x => x.PassportId == item.PassportId);

            
            clientInData.Name = item.Name;
            clientInData.LastName = item.LastName;
            clientInData.PhoneNumber = item.PhoneNumber;
        }

        public void UpdateAccount(Client client, Account account)
        {
            Client clientInData = Data.Keys.First(x => x.PassportId == client.PassportId);

            Account accountInData = Data[clientInData].First(x => x.Currency.Equals(account.Currency));

            accountInData.Amount = account.Amount;
        }
    }
}

