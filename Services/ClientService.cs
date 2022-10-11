using Models;
using Services.Exeptions;
using Services.Filters;
using ModelsDb;

namespace Services
{
    public class ClientService
    {

        private ApplicationContext _dbContext;

        public ClientService()
        {
            _dbContext = new ApplicationContext();
        }

        private ClientDb ConvertClientToClientDb(Client client)
        {
            return new ClientDb
            {
                Name = client.Name,
                LastName = client.LastName,
                Birthday = client.Birthday.ToUniversalTime(),
                PassportId = client.PassportId,
                Bonus = client.Bonus,
                PhoneNumber = client.PhoneNumber
            };
        }



        private Client ConvertClientDbToClient(ClientDb client)
        {
            return new Client
            {
                Name = client.Name,
                LastName = client.LastName,
                Birthday = client.Birthday.ToUniversalTime(),
                PassportId = client.PassportId,
                Bonus = client.Bonus,
                PhoneNumber = client.PhoneNumber,
            };
        }

        public List<Account> GetClientAccounts(Client client)
        {
            var clientId = _dbContext.Clients.FirstOrDefault(x => x.Name == client.Name
                                                                       && x.LastName == client.LastName
                                                                       && x.PhoneNumber == client.PhoneNumber).Id;

            var accountsDb = _dbContext.Accounts.Where(x => x.ClientId == clientId).ToList();

            var accountList = new List<Account>();

            foreach (var accountDb in accountsDb)
            {
                accountList.Add(new Account
                {
                    Amount = accountDb.Amount,
                    Currency = new Currency{ Name = accountDb.CurrencyName}
                });
            }

            return accountList;
        }

        public void UpdateAccount(Client client, Account oldAccount, Account newAccount)
        {
            var clientId = _dbContext.Clients.FirstOrDefault(x => x.Name == client.Name
                                                                  && x.LastName == client.LastName
                                                                  && x.PhoneNumber == client.PhoneNumber).Id;

            AccountDb account = _dbContext.Accounts.FirstOrDefault(x => x.ClientId == clientId 
                                                                        && x.CurrencyName == oldAccount.Currency.Name);

            _dbContext.Accounts.Update(account).Entity.Amount = newAccount.Amount;
            _dbContext.SaveChanges();

        }

        public Client GetClient(Guid id)
        {
            ClientDb clientDb = _dbContext.Clients.FirstOrDefault(x => x.Id == id);

            if (clientDb == null) throw new NullReferenceException("Такого клиента нет!");

            return ConvertClientDbToClient(clientDb);
        }

        private ClientDb GetClientDb(Guid id)
        {
            return _dbContext.Clients.FirstOrDefault(x => x.Id == id);
        }

        public void AddClient(Client client)
        {
            if (client.Birthday > new DateTime(2004, 1, 1))
            {
                throw new AgeLimitException("Клиент должен быть страрше 18 лет!");
            }

            if (client.PassportId == 0) throw new ArgumentNullException("У клиента нет паспортных данных!");

            ClientDb clientDb = ConvertClientToClientDb(client);

            _dbContext.Clients.Add(clientDb);

            _dbContext.Accounts.Add(new AccountDb
            {
                AccountId = new Guid(),
                Amount = 0,
                Client = clientDb,
                CurrencyName = "USD"
            });

            _dbContext.SaveChanges();
        }

        public void AddAccount(Guid clientId, Account account)
        {
            ClientDb client = GetClientDb(clientId);

            if (client == null) throw new NullReferenceException("Нет такого клиента!");

            _dbContext.Accounts.Add(new AccountDb
            {
                AccountId = new Guid(),
                Amount = account.Amount,
                CurrencyName = account.Currency.Name,
                Client = client
            });

            _dbContext.SaveChanges();
        }

        public void UpdateClient(Guid id, Client client)
        {
            ClientDb oldClient = GetClientDb(id);

            if (client.Name != null)
            {
                _dbContext.Clients.Update(oldClient).Entity.Name = client.Name;
                _dbContext.SaveChanges();
            }

            if (client.LastName != null)
            {
                _dbContext.Clients.Update(oldClient).Entity.LastName = client.LastName;
                _dbContext.SaveChanges();
            }

            if (client.Birthday != default(DateTime))
            {
                _dbContext.Clients.Update(oldClient).Entity.Birthday = client.Birthday.ToUniversalTime();
                _dbContext.SaveChanges();
            }

            if (client.PassportId != 0)
            {
                _dbContext.Clients.Update(oldClient).Entity.PassportId = client.PassportId;
                _dbContext.SaveChanges();
            }

            if (client.PhoneNumber != null)
            {
                _dbContext.Clients.Update(oldClient).Entity.PhoneNumber = client.PhoneNumber;
                _dbContext.SaveChanges();
            }

        }

        public void DeleteClient(Guid id)
        {
            _dbContext.Clients.Remove(GetClientDb(id));
            _dbContext.SaveChanges();
        }

        public void DeleteAccount(Guid id)
        {
            _dbContext.Accounts.Remove(_dbContext.Accounts.FirstOrDefault(x => x.AccountId == id));
            _dbContext.SaveChanges();
        }

        public List<Client> GetClients(ClientFilter filter)
        {
            var request = _dbContext.Clients.AsQueryable();

            if (filter.PassportId != 0)
            {
                request = request.Where(x => x.PassportId == filter.PassportId);
            }

            if (filter.LastName != null)
            {
                request = request.Where(x => x.LastName == filter.LastName);
            }

            if (filter.PhoneNumber != null)
            {
                request = request.Where(x => x.PhoneNumber == filter.PhoneNumber);
            }

            if (filter.MinBirthday != default(DateTime))
            {
                request = request.Where(x => x.Birthday.ToUniversalTime() <= filter.MinBirthday.ToUniversalTime());
            }

            if (filter.MaxBirthday != default(DateTime))
            {
                request = request.Where(x => x.Birthday.ToUniversalTime() >= filter.MaxBirthday.ToUniversalTime());
            }

            if (filter.Offset != 0)
            {
                request.Skip(filter.Offset);
            }

            if (filter.Limit != 0)
            {
                request.Take(filter.Limit);
            }

            List<Client> clients = new List<Client>();
            foreach (ClientDb clientDb in request)
            {
                clients.Add(ConvertClientDbToClient(clientDb));
            }

            return clients;
        }
    }
}