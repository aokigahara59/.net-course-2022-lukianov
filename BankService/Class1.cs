using Models;

namespace BankService
{
    public class BankService
    {

        public int SalaryCalculationForOwner()
        {
            int BankProfit = 1000000;
            int Expenses = 730000;
            int OwnersCount = 5;
            return (BankProfit - Expenses) / OwnersCount;
        }


        public Employee ConvertClientInEmployee(Client client)
        {
            Employee employee = new Employee
            {
                Name = client.Name,
                LastName = client.LastName,
                Birthday = client.Birthday,
                Passport_id = client.Passport_id
            };

           
            return employee;
        }

    }
}