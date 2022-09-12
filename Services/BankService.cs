using Models;

namespace Services
{
    public class BankService
    {
        private List<Person> _blackList = new List<Person>();

        public void AddBonus<T>(T person) where T : Person
        {
            person.Bonus += 1;
        }

        public void AddToBlackList<T>(T person) where T : Person
        {
            _blackList.Add(person);
        }

        public bool IsPersonInBlackList<T>(T person) where T : Person
        {
            return _blackList.Contains(person);
        }

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