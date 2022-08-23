using Models;

namespace PracticeWithTypes
{
    internal class Program
    {

        static void Main(string[] args)
        {
            Employee employee = new Employee { name = "Григорий", lastName = "Пихта"};
            employee.contract = updateContract(employee);
            //wrongUpatecontract(employee);
            Console.WriteLine(employee.contract);
            


            Currency currency = new Currency { name = "USD", id = 1};
            //wrondChangeCurrency(currency);
            currency = changeCurrency(currency);
            Console.WriteLine(currency.name + " " + currency.id);
            Console.ReadLine();
        }



        // Неправильный метод обновления служащего
        public static void wrongUpatecontract(Employee employee)
        {
            employee.contract = $"Сотрудник {employee.name} {employee.lastName} был принят на работу на следующие 3 года.";
        }

        // Правильный метод обновления служащего
        public static string updateContract(Employee employee)
        {
            return $"Сотрудник {employee.name}, {employee.lastName} был принят на работу на следующие 3 года.";
        }




        // Нерабочий метод
        public static void wrondChangeCurrency(Currency currency)
        {
            currency.name = "Рубль";
            currency.id = 810;
        }


        // Рабочий метод
        public static Currency changeCurrency(Currency currency)
        {
            currency.name = "Рубль";
            currency.id = 810;

            return currency;
        }
    }

    
}