using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;


namespace Services
{
    public class TestDataGenerator
    {

        public List<Client> GenerateTestClientsList()
        {
            var clients = new List<Client>();
            Random random = new Random();
            for (int i = 0; i < 1000; i++)
            {
                clients.Add(new Client
                {
                    Name = "Name" + i,
                    Birthday = new DateTime(random.Next(20, 100)),
                    PhoneNumber = i
                });
            }
            return clients;
        }

        public Dictionary<int, Client> GenerateTestClientsDictionary()
        {
            var clients = new Dictionary<int, Client>();
            Random random = new Random();
            for(int i = 0; i < 1000; i++)
            {
                Client client = new Client
                {
                    Name = "Name" + i,
                    Birthday = new DateTime(random.Next(20, 100)),
                    PhoneNumber = i
                };
                clients.Add(client.PhoneNumber, client);
            }
            return clients;
        }


        public List<Employee> GenerateTestEmployeeList()
        {
            var employees = new List<Employee>();
            for(int i = 0; i < 1000; i++)
            {
                employees.Add(new Employee
                {
                    Name = "Name" + i,
                    Salary = 10000-i,
                });
            }

            return employees;
        }
    }
}
