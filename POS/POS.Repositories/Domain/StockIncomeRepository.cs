using Microsoft.EntityFrameworkCore;
using POS.Domain.DAO;
using POS.Domain.Models.DataModels;
using POS.Repositories.Common;

namespace POS.Repositories.Domain
{
    public class StockIncomeRepository : BaseRepository<StockIncomeEntity>, IStockIncomeRepository
    {
        private readonly POSDbContext _dbContext;

        public StockIncomeRepository(POSDbContext dbContext) : base(dbContext)
        {
            this._dbContext = dbContext;
        }

        //to filter composit primary key
        public bool UpdateAsync(StockIncomeEntity entity)
        {
            try
            {                
                _dbContext.StockIncome.Update(entity);
                return true;
            }
            catch (Exception)
            {                
                return false;
            }            
        }
        public bool DeleteAsync(StockIncomeEntity entity)
        {
            try
            {
                _dbContext.StockIncome.Remove(entity);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
