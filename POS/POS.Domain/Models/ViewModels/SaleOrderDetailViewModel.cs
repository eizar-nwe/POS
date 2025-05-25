
namespace POS.Domain.Models.ViewModels
{    
    public class SaleOrderDetailViewModel : BaseViewModel
    {
        public int LINE_ID { get; set; }
        public string STOCKGRP_ID { get; set; }
        public string STOCKITEM_ID { get; set; }        
        public decimal PRICE { get; set; }
        public decimal QUANTITY { get; set; }
        public string DISC_TYPE { get; set; }                      
        public decimal DISC_AMOUNT { get; set; }
        public decimal TOT_DISC_AMT { get; set; }
        public decimal TOT_AMT { get; set; }
        public decimal NET_AMT { get; set; }

        public IList<StockGroupViewModel> StkGrpVM { get; set; }
        public IList<StockItemViewModel> StkItemVM { get; set; }        
    }
}
