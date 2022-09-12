using Models;
using Services.Exeptions;
using Services.Filters;

namespace Services
{
    public class ClientService
    {

        private ClientStorage _clientStorage;
        public ClientService(ClientStorage storage)
        {
            _clientStorage = storage;
        }

        public void AddClient(Client client)
        {
            if (client.Birthday > new DateTime(2004, 1, 1))
            {
                throw new AgeLimitException("Клиент должен быть страрше 18 лет!");
            }

            if (client.PassportId == 0) throw new ArgumentNullException("У клиента нет паспортных данных!");

            _clientStorage.Add(client);
        }


        public Dictionary<Client, List<Account>> GetClients(ClientFilter filter)
        {
            var request = _clientStorage.Clients.AsEnumerable();

            if (filter.PassportId != 0 )
            {
                request = request.Where(x => x.Key.PassportId == filter.PassportId);
            }

            if (filter.LastName != null)
            {
                request = request.Where(x => x.Key.LastName == filter.LastName);
            }

            if (filter.PhoneNumber != null)
            {
                request = request.Where(x => x.Key.PhoneNumber == filter.PhoneNumber);
            }

            if (filter.MinBirthday != default(DateTime))
            {
                request = request.Where(x => x.Key.Birthday <= filter.MinBirthday);
            }

            if (filter.MaxBirthday != default(DateTime))
            {
                request = request.Where(x => x.Key.Birthday >= filter.MaxBirthday);
            }

            return request.ToDictionary(x => x.Key, x => x.Value);
        }
    }
}
