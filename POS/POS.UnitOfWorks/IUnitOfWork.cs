using POS.Repositories.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.UnitOfWorks
{
    public interface IUnitOfWork
    {
        void Commit();
        void Rollback();

        IStockGroupRepository StockGroupRepository { get; }
        IStockItemRepository StockItemRepository { get; }
        ISupplierRepository SupplierRepository { get; }
        IMemberRepository MemberRepository { get; }
        ICashierRepository CashierRepository { get; }
        IStockIncomeRepository StockIncomeRepository { get; }
        IMemberDiscountRepository MemberDiscountRepository { get; }
        ISaleOrderHaderRepository SaleOrderHaderRepository { get; }
        ISaleOrderDetailRepository SaleOrderDetailRepository { get; }
        IPaymentRepository PaymentRepository { get; }
    }
}
