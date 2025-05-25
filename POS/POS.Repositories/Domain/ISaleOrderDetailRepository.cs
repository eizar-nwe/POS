using POS.Domain.Models.DataModels;
using POS.Repositories.Common;

namespace POS.Repositories.Domain
{
    public interface ISaleOrderDetailRepository:IBaseRepository<SaleOrderDetailEntity>
    {
        bool AddAsync(SaleOrderDetailEntity entity);
        bool UpdateAsync(SaleOrderDetailEntity entity);
        bool DeleteAsync(SaleOrderDetailEntity entity);
    }
}
