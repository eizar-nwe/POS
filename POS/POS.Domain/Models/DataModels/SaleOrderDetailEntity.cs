using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS.Domain.Models.DataModels
{
    [Table("SALE_ORDER_DETL")]
    public class SaleOrderDetailEntity : BaseEntity
    {
        public required int LINE_ID { get; set; }
        public required string STOCKGRP_ID { get; set; }
        public required string STOCKITEM_ID { get; set; }        
        public decimal? PRICE { get; set; }
        public decimal? QUANTITY { get; set; }
        public string? DISC_TYPE { get; set; }                      
        public decimal? DISC_AMOUNT { get; set; }
        public decimal? TOT_DISC_AMT { get; set; }
        public decimal? TOT_AMT { get; set; }
        public decimal? NET_AMT { get; set; }
    }
}
