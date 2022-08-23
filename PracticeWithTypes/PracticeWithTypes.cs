using Models;

namespace PracticeWithTypes
{
    public class PracticeWithTypesMethods
    {

        static void Main(string[] args)
        {

        }

        // Неправильный метод обновления служащего
        public static void wrongUpateContract(Employee employee)
        {
            employee.contract = $"Сотрудник {employee.name} {employee.lastName} был принят на работу на следующие 3 года.";
        }

        // Правильный метод обновления служащего
        public static string updateContract(Employee employee)
        {
            return $"Сотрудник {employee.name} {employee.lastName} был принят на работу на следующие 3 года.";
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