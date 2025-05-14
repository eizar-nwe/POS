using POS.Domain.DAO;
using POS.Repositories.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly POSDbContext _dbContext;

        public UnitOfWork(POSDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        private IStockGroupRepository stockGroupRepository;
        public IStockGroupRepository StockGroupRepository => stockGroupRepository?? new StockGroupRepository(_dbContext);
        
        private IStockItemRepository stockItemRepository;
        public IStockItemRepository StockItemRepository => stockItemRepository?? new StockItemRepository(_dbContext);
        private ISupplierRepository supplierRepository;
        public ISupplierRepository SupplierRepository => supplierRepository?? new SupplierRepository(_dbContext);

        public void Commit()
        {
            _dbContext.SaveChanges();
        }

        public void Rollback()
        {
            _dbContext.Dispose();
        }
    }
}
