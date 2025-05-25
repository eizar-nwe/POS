using POS.Domain.Models.ViewModels;

namespace POS.Services
{
    public interface ISaleOrderDetailService
    {
        string Create(SaleOrderDetailViewModel SaleDtlVM);
        IEnumerable<SaleOrderDetailViewModel> GetAll();
        IEnumerable<SaleOrderDetailViewModel> GetById(string id);
        string Update(SaleOrderDetailViewModel SaleDtlVM);
        string Delete(string id);
    }
}
