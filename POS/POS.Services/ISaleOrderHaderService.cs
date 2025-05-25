
using POS.Domain.Models.ViewModels;

namespace POS.Services
{
    public interface ISaleOrderHaderService
    {
        string Create(SaleOrderHaderViewModel SaleHdrVM);
        IEnumerable<SaleOrderHaderViewModel> GetAll();
        IEnumerable<SaleOrderHaderViewModel> GetById(string id);
        string Update(SaleOrderHaderViewModel SaleHdrVM);
        string Delete(string id);
    }
}
