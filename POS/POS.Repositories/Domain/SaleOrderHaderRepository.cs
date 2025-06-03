using POS.Domain.DAO;
using POS.Domain.Models.DataModels;
using POS.Repositories.Common;

namespace POS.Repositories.Domain
{
    public class SaleOrderHaderRepository : BaseRepository<SaleOrderHaderEntity>, ISaleOrderHaderRepository
    {
        private readonly POSDbContext _dbContext;

        public SaleOrderHaderRepository(POSDbContext dbContext) : base(dbContext)
        {
            this._dbContext = dbContext;
        }
    }
}
