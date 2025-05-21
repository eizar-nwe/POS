using System.ComponentModel.DataAnnotations.Schema;

namespace POS.Domain.Models.DataModels
{
    [Table("Cashier")]
    public class CashierEntity:BaseEntity
    {
        public required string Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string Gender { get; set; }
    }
}
