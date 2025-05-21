using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Domain.Models.DataModels
{
    [Table("StockIncome")]
    public class StockIncomeEntity:BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LINE_ID { get; set; }
        public required string STOCKGRP_ID { get; set; }
        public required string STOCKITEM_ID { get; set; }
        public required string SUPPLIER_ID { get; set; }
        public required decimal PRICE { get; set; }
        public required decimal QUANTITY { get; set; }
    }
}
