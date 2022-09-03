namespace Models
{
    public class Employee : Person
    {
        public string Contract { get; set; }
        public int Salary { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (!(obj is Employee)) return false;
            Employee other = (Employee)obj;

            return Contract == other.Contract
                && Salary == other.Salary
                && Name == other.Name
                && LastName == other.LastName
                && PassportId == other.PassportId;
            
        }
    }
}