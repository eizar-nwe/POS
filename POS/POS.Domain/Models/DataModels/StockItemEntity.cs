using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS.Domain.Models.DataModels
{
    [Table("STOCKITEM")]
    public class StockItemEntity:BaseEntity
    {
        [MaxLength(20)]
        public required string Code { get; set; }
        public string? Description { get; set; }
        public required string StockGrp_Id { get; set; }
        [MaxLength(20)]
        public string? BarCode { get; set; }
        [MaxLength(1)]
        public string? Disc_Type { get; set; }
        public decimal? Disc_Amount { get; set; }
        public decimal? Disc_Percent { get; set; }
        public  DateTime? From_Date { get; set; }
        public DateTime? To_Date { get; set; }
    }
}
