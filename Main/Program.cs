using Models;
using PracticeWithTypes;

namespace main
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Employee employee = new Employee { name = "Григорий", lastName = "Пихта" };
            employee.contract = PracticeWithType.updateContract(employee);
            //wrongUpatecontract(employee);
            Console.WriteLine(employee.contract);

            BankService.BankService bankService = new BankService.BankService();
            Client client = new Client() { name = "Павел", lastName = "Техник", birthday = new DateTime(19801212), passport_id = 5646465 };
            Employee newEmployee = bankService.turnClientOntoEmployee(client);
            Console.WriteLine("---------------------");
            Console.WriteLine("Превращение клиента в работника");
            Console.WriteLine(newEmployee.name);
            Console.WriteLine(newEmployee.lastName);
            Console.WriteLine(newEmployee.contract);
            Console.WriteLine("---------------------");

            Currency currency = new Currency { name = "USD", id = 1 };
            //wrondChangeCurrency(currency);
            currency = PracticeWithType.changeCurrency(currency);
            Console.WriteLine(currency.name + " " + currency.id);
            Console.ReadLine();

        }
    }
}