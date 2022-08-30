using Models;
using Services;
using System.Diagnostics;

namespace PracticeWithTypes
{
    public class Program
    {

        
        static void Main(string[] args)
        {
            TestDataGenerator testDataGenerator = new TestDataGenerator();
            var clientsList = testDataGenerator.GenerateTestClientsList();
            var clientsDictionary = testDataGenerator.GenerateTestClientsDictionary();
            var employees = testDataGenerator.GenerateTestEmployeeList();

            Stopwatch stopwatch = new Stopwatch();

            
            stopwatch.Start();
            Console.WriteLine("Searching through List: "
                + clientsList.FirstOrDefault(client => client.PhoneNumber == 500).Name);
            stopwatch.Stop();
            Console.WriteLine($"ticks = {stopwatch.ElapsedTicks}");
            Console.WriteLine();
            stopwatch.Reset();


            stopwatch.Restart();
            Console.WriteLine("Searching through Dictionary: "
                + clientsDictionary.GetValueOrDefault(500).Name);
            stopwatch.Stop();
            Console.WriteLine($"ticks = {stopwatch.ElapsedTicks}");
            Console.WriteLine();
            stopwatch.Reset();


            
            var ageSortedClients = clientsList.FindAll(client => client.Birthday < new DateTime(50) ).ToList();
            Console.WriteLine($"Clients younger than 50 - {ageSortedClients.Count}");
            Console.WriteLine();

            Employee lowestSalaeyEmployee = employees[0];
            foreach (var employee in employees)
            {
                if (employee.Salary < lowestSalaeyEmployee.Salary) lowestSalaeyEmployee=employee;
            }
            Console.WriteLine($"Employee with lowest salary is " +
                $"{lowestSalaeyEmployee.Name} with salary {lowestSalaeyEmployee.Salary}");
            Console.WriteLine();

            // сравнение поиска 
            stopwatch.Restart();
            Console.WriteLine("Searching last in List FirstOrDefault: "
                + clientsList.FirstOrDefault(client => client.PhoneNumber == 999).Name);
            stopwatch.Stop();
            Console.WriteLine($"ticks = {stopwatch.ElapsedTicks}");
            Console.WriteLine();
            stopwatch.Reset();

            stopwatch.Restart();
            Console.WriteLine("Searching last in List 999 key: "
                + clientsList[999].Name);
            stopwatch.Stop();
            Console.WriteLine($"ticks = {stopwatch.ElapsedTicks}");
            Console.WriteLine();
            stopwatch.Reset();



            Console.ReadKey();

        }
            

        // Правильный метод обновления служащего
        public static string UpdateContract(in Employee employee)
        {
            return $"Сотрудник {employee.Name} {employee.LastName} был принят на работу на следующие 3 года.";
        }

        // Рабочий метод
        public static Currency ChangeCurrency(Currency currency)
        {
            currency.Name = "Рубль";
            currency.Code = 810;
            return currency;
        }
    }

    
}