using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Bogus;

namespace Services
{
    public class TestDataGenerator
    {       
        private Faker<Client> _сlientFaker = new Faker<Client>()
                    .RuleFor(x => x.Name, x => x.Person.FirstName)
                    .RuleFor(x => x.LastName, x => x.Person.LastName)
                    .RuleFor(x => x.PassportId, x => x.UniqueIndex)
                    .RuleFor(x => x.Birthday, x => x.Person.DateOfBirth)
                    .RuleFor(x => x.PhoneNumber, x => x.Person.Phone);



        public List<Client> GenerateTestClientsList()
        {
            return _сlientFaker.Generate(1000);
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
            var employees = new List<Employee>();
            Faker<Employee> employeeFaker = new Faker<Employee>()
                    .RuleFor(x => x.Name, x => x.Person.FirstName)
                    .RuleFor(x => x.LastName, x => x.Person.LastName)
                    .RuleFor(x => x.PassportId, x => x.UniqueIndex)
                    .RuleFor(x => x.Birthday, x => x.Person.DateOfBirth);
            for (int i = 0; i < 1000; i++)
            {
                var employee = employeeFaker.Generate();
                employee.Salary = 10000 - i;
                employees.Add(employee);             
            }
            return employees;
        }
    }
}
