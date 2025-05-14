using System.ComponentModel.DataAnnotations.Schema;

namespace POS.Domain.Models.DataModels
{
    [Table("Supplier")]
    public class SupplierEntity:BaseEntity
    {
        public required string Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
    }
}
