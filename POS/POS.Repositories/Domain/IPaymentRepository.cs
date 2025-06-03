using POS.Domain.Models.DataModels;
using POS.Domain.Models.ViewModels;
using POS.Repositories.Common;

namespace POS.Repositories.Domain
{
    public interface IPaymentRepository : IBaseRepository<PaymentEntity>
    {
        Task<int> Execute_Payment_Procedure(string id, string paymethod, string cardNo, string crdBy, SaleOrderViewModel saleTotal,string ordrtype);
    }
}
