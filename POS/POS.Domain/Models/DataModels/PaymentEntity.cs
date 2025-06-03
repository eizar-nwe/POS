using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS.Domain.Models.DataModels
{
    [Table("PAYMENT")]
    public class PaymentEntity : BaseEntity
    {        
        public required string PAY_METHOD { get; set; }        
        public required decimal TOT_AMT { get; set; }
        public string? CARD_NUM { get; set; }
    }
}
