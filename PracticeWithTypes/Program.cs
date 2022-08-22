using Models;

namespace PracticeWithTypes
{
    class Program
    {

        public void Main(string[] args)
        {

        }



        // Неправильный метод обновления служащего
        public void wrongUpatecontract(Employee employee)
        {
            employee.contract = $"Сотрудник {employee.name}, {employee.lastName} был принят на работу на следующие 3 года.";
        }


        // Правильный метод
        public string updateContract(Employee employee)
        {
            return $"Сотрудник {employee.name}, {employee.lastName} был принят на работу на следующие 3 года.";
        }


    }
}