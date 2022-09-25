using Models;
using Bogus;
using ModelsDb;

namespace Services
{
    public class TestDataGenerator
    {       
        private Faker<Client> _сlientFaker = new Faker<Client>()
                    .RuleFor(x => x.Name, x => x.Person.FirstName)
                    .RuleFor(x => x.LastName, x => x.Person.LastName)
                    .RuleFor(x => x.PassportId, x => x.GetHashCode())
                    .RuleFor(x => x.Birthday, x => x.Person.DateOfBirth)
                    .RuleFor(x => x.PhoneNumber, x => x.Person.Phone);

        private Faker<ClientDb> _clientDbFaker = new Faker<ClientDb>()
                    .RuleFor(x => x.Id, x => x.Random.Guid())
                    .RuleFor(x => x.Name, x => x.Person.FirstName)
                    .RuleFor(x => x.LastName, x => x.Person.LastName)
                    .RuleFor(x => x.PassportId, x => x.GetHashCode())
                    .RuleFor(x => x.Birthday, x => x.Person.DateOfBirth.ToUniversalTime())
                    .RuleFor(x => x.PhoneNumber, x => x.Person.Phone);

        public List<Client> GenerateTestClientsList()
        {
            return _сlientFaker.Generate(1000);
        }

        public List<ClientDb> GenerateTestClientsDbList(int count)
        {
            return _clientDbFaker.Generate(count);
        }

        public Dictionary<string, Client> GenerateTestClientsDictionary()
        {
            var clients = new Dictionary<string, Client>();         
            for (int i = 0; i < 1000; i++)
            {
                var client = _сlientFaker.Generate();              
                clients.Add(client.PhoneNumber, client);
            }
            return clients;
        }


        public List<Employee> GenerateTestEmployeeList()
        {
            Faker<Employee> employeeFaker = new Faker<Employee>()
                    .RuleFor(x => x.Name, x => x.Person.FirstName)
                    .RuleFor(x => x.LastName, x => x.Person.LastName)
                    .RuleFor(x => x.PassportId, x => x.GetHashCode())
                    .RuleFor(x => x.Birthday, x => x.Person.DateOfBirth)
                    .RuleFor(x => x.Salary, x=> x.Random.Int(1000, 10000));
            return employeeFaker.Generate(1000);       
        }

        public List<EmployeeDb> GenerateTestEmployeeDbList()
        {
            Faker<EmployeeDb> employeeFaker = new Faker<EmployeeDb>()
                .RuleFor(x => x.Id, x => x.Random.Guid())
                .RuleFor(x => x.Name, x => x.Person.FirstName)
                .RuleFor(x => x.LastName, x => x.Person.LastName)
                .RuleFor(x => x.PassportId, x => x.GetHashCode())
                .RuleFor(x => x.Birthday, x => x.Person.DateOfBirth.ToUniversalTime())
                .RuleFor(x => x.Salary, x => x.Random.Int(1000, 10000))
                .RuleFor(x => x.Contract, x => $"{x.Person.FullName} was hired on 3 years");
            return employeeFaker.Generate(100);
        }

        public Dictionary<Client, List<Account>> GenerateClientAccountDictionary()
        {
            var accountDictionary = new Dictionary<Client, List<Account>>();
            Random random = new Random();
            Currency currency = new Currency
            {
                Name = "USD",
                Code = 840
            };
            Faker<Account> accountFaker = new Faker<Account>()
                .RuleFor(x => x.Currency, x => currency)
                .RuleFor(x => x.Amount, x => x.Random.Int(1000, 10000));

            for (int i = 0; i < 1000; i++)
            {
                accountDictionary.Add(_сlientFaker.Generate(),
                    accountFaker.GenerateBetween(1, 3));
            }
            return accountDictionary;
        }
    }
}
