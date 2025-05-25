using POS.Domain.Models.DataModels;
using POS.Repositories.Common;

namespace POS.Repositories.Domain
{
    public interface ISaleOrderHaderRepository:IBaseRepository<SaleOrderHaderEntity>
    {
        bool AddAsync(SaleOrderHaderEntity entity);
        bool UpdateAsync(SaleOrderHaderEntity entity);
        bool DeleteAsync(SaleOrderHaderEntity entity);
    }
}
