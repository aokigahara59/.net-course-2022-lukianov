using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsDb
{
    [Table("clients")]
    public class ClientDb
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

        [Column("phone_number")]
        public string PhoneNumber { get; set; }

        [Column("bonus")]
        public int Bonus { get; set; }

        public ICollection<AccountDb> Accounts { get; set; }
    }
}