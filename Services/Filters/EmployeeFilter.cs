namespace Services.Filters
{
    public class EmployeeFilter
    {
        public string LastName { get; set; }
        public int PassportId { get; set; }
        public DateTime MinBirthday { get; set; }
        public DateTime MaxBirthday { get; set; }
        public string Contract { get; set; }
        public int Offset { get; set; }
        public int Limit { get; set; }
    }
}
