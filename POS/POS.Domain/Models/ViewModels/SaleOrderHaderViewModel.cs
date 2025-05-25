
namespace POS.Domain.Models.ViewModels
{    
    public class SaleOrderHaderViewModel : BaseViewModel
    {
        public DateOnly TRNS_DATE { get; set; }
        public string CASHIER_ID { get; set; }
        public string IsMEMBER { get; set; }        
        public string MEMBER_ID { get; set; }                
        public decimal TOT_DISC_AMT { get; set; }
        public decimal TOT_AMT { get; set; }
        public decimal NET_AMT { get; set; }
        
        public IList<MemberViewModel> MemberVM { get; set; }
        public IList<CashierViewModel> CashierVM { get; set; }
    }
}
