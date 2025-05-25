using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS.Domain.Models.DataModels
{
    [Table("SALE_ORDER_HDR")]
    public class SaleOrderHaderEntity : BaseEntity
    {
        public required DateOnly TRNS_DATE { get; set; }
        public required string CASHIER_ID { get; set; }
        public string? IsMEMBER { get; set; }        
        public required string MEMBER_ID { get; set; }                
        public required decimal TOT_DISC_AMT { get; set; }
        public required decimal TOT_AMT { get; set; }
        public required decimal NET_AMT { get; set; }
    }
}
