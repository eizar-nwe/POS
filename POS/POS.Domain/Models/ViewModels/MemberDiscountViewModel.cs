
namespace POS.Domain.Models.ViewModels
{
    public class MemberDiscountViewModel : BaseViewModel
    {
        public int LINE_ID { get; set; }
        public string MEMBER_ID { get; set; }
        public string STOCKGRP_ID { get; set; }
        public string STOCKITEM_ID { get; set; }
        public decimal ORIGIN_PRICE { get; set; }        
        public decimal OFFER_PRICE { get; set; }        
        public string? Disc_Type { get; set; }
        public decimal? Disc_Amount { get; set; }
        public decimal? Disc_Percent { get; set; }
        public DateTime? From_Date { get; set; }
        public DateTime? To_Date { get; set; }

        public string MemberInfo { get; set; }
        public string StkGrpInfo { get; set; }
        public string StkItemInfo { get; set; }

        public IList<MemberViewModel> MemberVM { get; set; }
        public IList<StockGroupViewModel> StkGrpVM { get; set; }
        public IList<StockItemViewModel> StkItemVM { get; set; }        
    }
}
