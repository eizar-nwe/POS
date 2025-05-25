using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Domain.Models.DataModels
{
    [Table("MEMBER_DISCOUNT")]
    public class MemberDiscountEntity : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LINE_ID { get; set; }
        public required string MEMBER_ID { get; set; }
        public required string STOCKGRP_ID { get; set; }
        public required string STOCKITEM_ID { get; set; }        
        public required decimal ORIGIN_PRICE { get; set; }
        public required decimal OFFER_PRICE { get; set; }
        [MaxLength(1)]
        public string? Disc_Type { get; set; }
        public decimal? Disc_Amount { get; set; }
        public decimal? Disc_Percent { get; set; }
        public DateTime? From_Date { get; set; }
        public DateTime? To_Date { get; set; }

    }
}
