using POS.Domain.Models.ViewModels;

namespace POS.Services
{
    public interface ICashierService
    {
        bool Create(CashierViewModel CashierVM);
        IEnumerable<CashierViewModel> GetAll();
        CashierViewModel GetById(string id);
        bool Update(CashierViewModel CashierVM);
        bool Delete(String id);
    }
}
