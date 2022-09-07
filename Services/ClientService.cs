using Models;
using Services.Exeptions;

namespace Services
{
    public class ClientService
    {
        private Dictionary<Client, List<Account>> _clients = new Dictionary<Client, List<Account>>();

        public void AddClient(Client client)
        {
            if (client.Birthday > new DateTime(2004, 1, 1))
            {
                throw new AgeLimitException("Клиент должен быть страрше 18 лет!");
            }

            if (client.PassportId == 0) throw new ArgumentNullException("У клиента нет паспортных данных!");

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
            var accounts = new List<Account>{account};

            _clients.Add(client, accounts);
        }
    }
}
