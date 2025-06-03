
using POS.Domain.Models.ViewModels;

namespace POS.Services
{
    public interface ISaleOrderService
    {
        Task<(string hId, string Msg)> AddItem(string id, string GrpId, string ItemId,string memId, string ordrType, decimal qnty, string rmk);
        Task<string> UpdateItem(string cmd,string id,int LineId, string GrpId, string ItemId, decimal qnty, string rmk);
        Task<SaleOrderViewModel> GetTotalSumsAsync(string id);
        IEnumerable<SaleOrderDetailViewModel> GetDetlById(string id);
        Task<string> AddPayment(string id, string payMethod, string cardNo,string ordrtype);
    }
}
