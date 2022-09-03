﻿using System;
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
            Faker<Employee> employeeFaker = new Faker<Employee>()
                    .RuleFor(x => x.Name, x => x.Person.FirstName)
                    .RuleFor(x => x.LastName, x => x.Person.LastName)
                    .RuleFor(x => x.PassportId, x => x.UniqueIndex)
                    .RuleFor(x => x.Birthday, x => x.Person.DateOfBirth)
                    .RuleFor(x => x.Salary, x=> x.Random.Int(1000, 10000));
            return employeeFaker.Generate(1000);       
        }


        public Dictionary<Client, Account> GenerateClientAccountDictionary()
        {
            Dictionary<Client, Account> accountDictionary = new Dictionary<Client, Account>();
            Random random = new Random();
            Currency currency = new Currency
            {
                Name = "USD",
                Code = 840
            };

            for (int i = 0; i < 1000; i++)
            {
                accountDictionary.Add(_сlientFaker.Generate(),
                    new Account { Currency = currency, Amount = random.Next(100, 10000) });
            }
            return accountDictionary;
        }
    }
}
