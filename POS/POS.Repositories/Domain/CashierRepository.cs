using POS.Domain.DAO;
using POS.Domain.Models.DataModels;
using POS.Repositories.Common;

namespace POS.Repositories.Domain
{
    public class CashierRepository : BaseRepository<CashierEntity>, ICashierRepository
    {
        private readonly POSDbContext _dbContext;

        public CashierRepository(POSDbContext dbContext) : base(dbContext)
        {
            this._dbContext = dbContext;
        }
    }
}
