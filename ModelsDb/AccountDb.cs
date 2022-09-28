using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsDb
{
    [Table("accounts")]
    public class AccountDb
    {
        [Key]
        [Column("id")]
        public Guid AccountId { get; set; }

        [Column("currency_name")]
        public string CurrencyName { get; set; }

        [Column("amount")]
        public int Amount { get; set; }

        [Column("client_id")]
        public Guid ClientId { get; set; }

        public ClientDb Client { get; set; }
    }
}
