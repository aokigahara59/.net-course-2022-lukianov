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
            Stopwatch stopwatch = new Stopwatch();
            long elapsedTicks = 0;

            var clientsList = testDataGenerator.GenerateTestClientsList();           
            var clientsDictionary = testDataGenerator.GenerateTestClientsDictionary();        
            List<Employee> employees = testDataGenerator.GenerateTestEmployeeList();

            string clientsListSearchNumber = clientsList.Last().PhoneNumber;
            string clientsDictionarySearchNumber = clientsDictionary.Last().Key;


            for (int i = 0; i < 5; i++)
            {
                stopwatch.Restart();
                _ = clientsList.FirstOrDefault(client => client.PhoneNumber == clientsListSearchNumber);
                stopwatch.Stop();
                elapsedTicks += stopwatch.ElapsedTicks;
            }
            elapsedTicks = elapsedTicks / 5;            
            Console.WriteLine($"Searching through List: ticks = {elapsedTicks}");
            elapsedTicks = 0;
            Console.WriteLine();
            stopwatch.Reset();


            for (int i = 0; i < 5; i++)
            {
                stopwatch.Restart();
                _ = clientsDictionary[clientsDictionarySearchNumber];
                stopwatch.Stop();
                elapsedTicks += stopwatch.ElapsedTicks;
            }
            elapsedTicks = elapsedTicks / 5;
            Console.WriteLine($"Searching through Dictionary: ticks = {elapsedTicks}");
            elapsedTicks = 0;
            Console.WriteLine();
            stopwatch.Reset();

            var ageSortedClients = clientsList.FindAll(client => client.Birthday < clientsList[500].Birthday).ToList();
            Console.WriteLine($"Clients younger than 50 - {ageSortedClients.Count}");
            Console.WriteLine();

         

            Employee lowestSalaryEmployee = employees.OrderBy(x => x.Salary).First();          
            Console.WriteLine($"Employee with lowest salary is " +
                $"{lowestSalaryEmployee.Name} with salary {lowestSalaryEmployee.Salary}");
            Console.WriteLine();



            // сравнение поиска 
            for (int i = 0; i < 5; i++)
            {
                stopwatch.Restart();
                _ = clientsList.FirstOrDefault(client => client.PhoneNumber == clientsListSearchNumber);
                stopwatch.Stop();
                elapsedTicks += stopwatch.ElapsedTicks;
            }
            Console.WriteLine($"Searching last in List FirstOrDefault: ticks = {elapsedTicks/5}");
            elapsedTicks = 0;
            Console.WriteLine();
            stopwatch.Reset();

            for (int i = 0; i < 5; i++)
            {
                stopwatch.Restart();
                _ = clientsList[999].Name;
                stopwatch.Stop();
                elapsedTicks += stopwatch.ElapsedTicks;
            }
            Console.WriteLine($"Searching last in List 999 key: ticks = {elapsedTicks / 5}");
            Console.WriteLine();
            elapsedTicks = 0;

            Console.ReadKey();

        }
            

        
        public static string UpdateContract(in Employee employee)
        {
            return $"Сотрудник {employee.Name} {employee.LastName} был принят на работу на следующие 3 года.";
        }

        
        public static Currency ChangeCurrency(Currency currency)
        {
            currency.Name = "Рубль";
            currency.Code = 810;
            return currency;
        }
    }

    
}