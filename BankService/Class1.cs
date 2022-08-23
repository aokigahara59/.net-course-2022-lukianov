using Models;
using PracticeWithTypes;

namespace BankService
{
    public class BankService
    {

        public int salaryCalculationForOwner()
        {
            int bankProfit = 1000000;
            int expenses = 730000;
            int ownersCount = 5;
            return (bankProfit - expenses) / ownersCount;
        }


        public Employee turnClientOntoEmployee(Client client)
        {
            Employee employee = new Employee { name = client.name,
            lastName = client.lastName, birthday = client.birthday, passport_id = client.passport_id};
            employee.contract = PracticeWithTypesMethods.updateContract(employee);
            return employee;
        }

    }
}