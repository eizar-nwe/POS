using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS.Domain.Models.DataModels
{
    [Table("STOCKGROUP")]
    public class StockGroupEntity:BaseEntity
    {
        [MaxLength(20)]
        public required string Code { get; set; }
        public string? Description { get; set; }
    }
}
