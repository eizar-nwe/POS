using Microsoft.EntityFrameworkCore;
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
        private IMemberRepository memberRepository;
        public IMemberRepository MemberRepository => memberRepository ?? new MemberRepository(_dbContext);
        private ICashierRepository cashierRepository;
        public ICashierRepository CashierRepository => cashierRepository ?? new CashierRepository(_dbContext);
        private IStockIncomeRepository stockIncomeRepository;
        public IStockIncomeRepository StockIncomeRepository => stockIncomeRepository?? new StockIncomeRepository(_dbContext);
        private IMemberDiscountRepository memberDiscountRepository;
        public IMemberDiscountRepository MemberDiscountRepository => memberDiscountRepository?? new MemberDiscountRepository(_dbContext);
        private ISaleOrderHaderRepository saleOrderHaderRepository;
        public ISaleOrderHaderRepository SaleOrderHaderRepository => saleOrderHaderRepository ?? new SaleOrderHaderRepository(_dbContext);
        private ISaleOrderDetailRepository saleOrderDetailRepository;
        public ISaleOrderDetailRepository SaleOrderDetailRepository => saleOrderDetailRepository?? new SaleOrderDetailRepository(_dbContext);
        private IPaymentRepository paymentRepository;
        public IPaymentRepository PaymentRepository => paymentRepository?? new PaymentRepository(_dbContext);

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
