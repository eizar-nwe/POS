using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Domain.Models.ViewModels
{
    public class StockIncomeViewModel:BaseViewModel
    {
        public int LINE_ID { get; set; }
        public string STOCKGRP_ID { get; set; }
        public string STOCKITEM_ID { get; set; }
        public string SUPPLIER_ID { get; set; }
        public decimal PRICE { get; set; }
        public decimal QUANTITY { get; set; }

        public string StkGrpInfo { get; set; }
        public string StkItemInfo { get; set; }
        public string SupplierInfo { get; set; }

        public IList<StockGroupViewModel> StkGrpVM { get; set; }
        public IList<StockItemViewModel> StkItemVM { get; set; }
        public IList<SupplierViewModel> SupplierVM { get; set; }
    }
}
