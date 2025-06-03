using POS.Domain.Models.DataModels;
using POS.Repositories.Common;

namespace POS.Repositories.Domain
{
    public interface ISaleOrderDetailRepository:IBaseRepository<SaleOrderDetailEntity>
    {
        Task<int> Execute_SaleDetl_Procedure(string cmd, string id, int lineId, string grpId, string itemId, decimal qnty, string rmk, string crdBy);
    }
}
