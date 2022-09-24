using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsDb
{
    [Table(("employees"))]
    public class EmployeeDb
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("last_name")]
        public string LastName { get; set; }

        [Column("birthday")]
        public DateTime Birthday { get; set; }

        [Column("passport_id")]
        public int PassportId { get; set; }

        [Column("bonus")]
        public int Bonus { get; set; }

        [Column("contract")]
        public string Contract { get; set; }
        
        [Column("salary")]
        public int Salary { get; set; }
    }
}
