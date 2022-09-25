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

        public ClientDb GetClient(Guid id)
        {
            return _dbContext.Clients.FirstOrDefault(x => x.Id == id);
        }

        public void AddClient(ClientDb client)
        {
            if (client.Birthday > new DateTime(2004, 1, 1))
            {
                throw new AgeLimitException("Клиент должен быть страрше 18 лет!");
            }

            if (client.PassportId == 0) throw new ArgumentNullException("У клиента нет паспортных данных!");

            _dbContext.Clients.Add(client);

            _dbContext.Accounts.Add(new AccountDb
            {
                AccountId = new Guid(),
                Amount = 0,
                Client = client,
                CurrencyName = "USD"
            });

            _dbContext.SaveChanges();
        }

        public void AddAccount(Guid clientId, AccountDb account)
        {
            ClientDb client = GetClient(clientId);

            if (client == null) throw new NullReferenceException("Нет такого клиента!");

            account.Client = client;

            _dbContext.Accounts.Add(account);

            _dbContext.SaveChanges();
        }

        public void UpdateClient(Guid id, ClientDb client)
        {
            var oldClient = GetClient(id);

            if (client.Name != null)
            {
                oldClient.Name = client.Name;
            }

            if (client.LastName != null)
            {
                oldClient.LastName = client.LastName;
            }

            if (client.Birthday != default(DateTime))
            {
                oldClient.Birthday = client.Birthday;
            }

            if (client.PassportId != 0)
            {
                oldClient.PassportId = client.PassportId;
            }

            if (client.PhoneNumber != null)
            {
                oldClient.PhoneNumber = client.PhoneNumber;
            }

            _dbContext.SaveChanges();
        }

        public void DeleteClient(Guid id)
        {
            _dbContext.Clients.Remove(GetClient(id));
            _dbContext.SaveChanges();
        }

        public void DeleteAccount(Guid id)
        {
            _dbContext.Accounts.Remove(_dbContext.Accounts.FirstOrDefault(x => x.AccountId == id));
            _dbContext.SaveChanges();
        }

        public List<ClientDb> GetClients(ClientFilter filter)
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

            return request.ToList();
        }
    }
}