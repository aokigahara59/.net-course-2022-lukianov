using Models;
using BankService;

namespace PracticeWithTypes
{
    public class PracticeWithTypesMethods
    {

        
        static void Main(string[] args)
        {

            Employee employee = new Employee { Name = "Григорий", LastName = "Пихта" };
            employee.Contract = PracticeWithTypesMethods.UpdateContract(in employee);
            Console.WriteLine(employee.Contract);

            BankService.BankService bankService = new BankService.BankService();
            Client client = new Client() { Name = "Павел", LastName = "Техник", Birthday = new DateTime(19801212), Passport_id = 5646465 };
            Employee newEmployee = bankService.ConvertClientInEmployee(client);
            newEmployee.Contract = UpdateContract(in newEmployee);
            Console.WriteLine("---------------------");
            Console.WriteLine("Превращение клиента в работника");
            Console.WriteLine(newEmployee.Name);
            Console.WriteLine(newEmployee.LastName);
            Console.WriteLine(newEmployee.Contract);
            Console.WriteLine("---------------------");

            Currency currency = new Currency { Name = "USD", Id = 1 };
            currency = PracticeWithTypesMethods.ChangeCurrency(currency);
            Console.WriteLine(currency.Name + " " + currency.Id);
            Console.ReadLine();

        }
        

        // Неправильный метод обновления служащего
        public static void WrongUpateContract(Employee employee)
        {
            employee.Contract = $"Сотрудник {employee.Name} {employee.LastName} был принят на работу на следующие 3 года.";
        }

        // Правильный метод обновления служащего
        public static string UpdateContract(in Employee employee)
        {
            return $"Сотрудник {employee.Name} {employee.LastName} был принят на работу на следующие 3 года.";
        }




        // Нерабочий метод
        public static void WrondChangeCurrency(Currency currency)
        {
            currency.Name = "Рубль";
            currency.Id = 810;
        }


        // Рабочий метод
        public static Currency ChangeCurrency(Currency currency)
        {
            currency.Name = "Рубль";
            currency.Id = 810;
            return currency;
        }
    }

    
}