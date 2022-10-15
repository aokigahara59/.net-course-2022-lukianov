using Microsoft.EntityFrameworkCore;
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

        public async Task UpdateAccount(Client client, Account oldAccount, Account newAccount)
        {
            var clientDb = await _dbContext.Clients.FirstOrDefaultAsync(x => x.Name == client.Name
                                                                  && x.LastName == client.LastName
                                                                  && x.PhoneNumber == client.PhoneNumber);
            var clientId = clientDb.Id;

            var account = await _dbContext.Accounts.FirstOrDefaultAsync(x => x.ClientId == clientId 
                                                                        && x.CurrencyName == oldAccount.Currency.Name);

            _dbContext.Accounts.Update(account).Entity.Amount = newAccount.Amount;
            await _dbContext.SaveChangesAsync();

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

        public async Task AddClient(Client client)
        {
            if (client.Birthday > new DateTime(2004, 1, 1))
            {
                throw new AgeLimitException("Клиент должен быть страрше 18 лет!");
            }

            if (client.PassportId == 0) throw new ArgumentNullException("У клиента нет паспортных данных!");

            ClientDb clientDb = ConvertClientToClientDb(client);

            var addClientTask = _dbContext.Clients.AddAsync(clientDb);

            var addAccountTask = _dbContext.Accounts.AddAsync(new AccountDb
            {
                AccountId = new Guid(),
                Amount = 0,
                Client = clientDb,
                CurrencyName = "USD"
            });

            await addClientTask;
            await addAccountTask;

            await _dbContext.SaveChangesAsync();
        }

        public async Task AddAccount(Guid clientId, Account account)
        {
            ClientDb client = GetClientDb(clientId);

            if (client == null) throw new NullReferenceException("Нет такого клиента!");

            await _dbContext.Accounts.AddAsync(new AccountDb
            {
                AccountId = new Guid(),
                Amount = account.Amount,
                CurrencyName = account.Currency.Name,
                Client = client
            });

            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateClient(Guid id, Client client)
        {
            ClientDb oldClient = GetClientDb(id);

            if (client.Name != null)
            {
                _dbContext.Clients.Update(oldClient).Entity.Name = client.Name;
                await _dbContext.SaveChangesAsync();
            }

            if (client.LastName != null)
            {
                _dbContext.Clients.Update(oldClient).Entity.LastName = client.LastName;
                await _dbContext.SaveChangesAsync();
            }

            if (client.Birthday != default(DateTime))
            {
                _dbContext.Clients.Update(oldClient).Entity.Birthday = client.Birthday.ToUniversalTime();
                await _dbContext.SaveChangesAsync();
            }

            if (client.PassportId != 0)
            {
                _dbContext.Clients.Update(oldClient).Entity.PassportId = client.PassportId;
                await _dbContext.SaveChangesAsync();
            }

            if (client.PhoneNumber != null)
            {
                _dbContext.Clients.Update(oldClient).Entity.PhoneNumber = client.PhoneNumber;
                await _dbContext.SaveChangesAsync();
            }

        }

        public async Task DeleteClient(Guid id)
        {
            _dbContext.Clients.Remove(GetClientDb(id));
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAccount(Guid id)
        {
            _dbContext.Accounts.Remove(await _dbContext.Accounts.FirstOrDefaultAsync(x => x.AccountId == id));
            await _dbContext.SaveChangesAsync();
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