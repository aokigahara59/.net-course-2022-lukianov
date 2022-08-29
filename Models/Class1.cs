namespace Models
{
    public class Person
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public int PassportId { get; set; }
        public DateTime Birthday { get; set; }

    }

    public class Employee : Person
    {
        public string Contract { get; set; }
    }

    public class Client : Person
    {

    }


    public struct Currency
    {
        public string Name { get; set; }
        public int Code { get; set; }
    }
}