using POS.Domain.DAO;
using POS.Domain.Models.DataModels;
using POS.Repositories.Common;
using System.Linq.Expressions;

namespace POS.Repositories.Domain
{
    public class SaleOrderDetailRepository : BaseRepository<SaleOrderDetailEntity>, ISaleOrderDetailRepository
    {
        private readonly POSDbContext _dbContext;

        public SaleOrderDetailRepository(POSDbContext dbContext) : base(dbContext)
        {
            this._dbContext = dbContext;
        }

        public bool AddAsync(SaleOrderDetailEntity entity)
        {
            throw new NotImplementedException();
        }

        public bool DeleteAsync(SaleOrderDetailEntity entity)
        {
            throw new NotImplementedException();
        }

        public bool UpdateAsync(SaleOrderDetailEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
