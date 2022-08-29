using Models;

namespace BankService
{
    public class BankService
    {

        public float SalaryCalculationForOwner(float BankProfit, float Expenses, int OwnersCount)
        {
            return (BankProfit - Expenses) / (float)OwnersCount;
        }


        public Employee ConvertClientInEmployee(Client client)
        {
            return new Employee
            {
                Name = client.Name,
                LastName = client.LastName,
                Birthday = client.Birthday,
                PassportId = client.PassportId
            };
        }

    }
}