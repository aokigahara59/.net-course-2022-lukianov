using Models;

namespace Services.Storages
{
    public interface IClientStorage : IStorage<Client>
    {
        Dictionary<Client, List<Account>> Data { get; }
        void AddAccount(Client client, Account account);
        void RemoveAccount(Client client, Account account);
        void UpdateAccount(Client client, Account account);
    }
}
