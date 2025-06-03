
namespace POS.Domain.Models.ViewModels
{    
    public class PaymentViewModel : BaseViewModel
    {
        public string PAY_METHOD { get; set; }
        public decimal TOT_AMT { get; set; }
        public string? CARD_NUM { get; set; }
    }
}
