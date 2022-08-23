namespace Models
{
    public class Person
    {
        public string name { get; set; }
        public string lastName { get; set; }
        public int passport_id { get; set; }
        public DateTime birthday { get; set; }

    }

    public class Employee : Person
    {
        public string contract { get; set; }
    }

    public class Client : Person
    {

    }


    public struct Currency
    {
        public string name { get; set; }
        public int id { get; set; }

    }
}