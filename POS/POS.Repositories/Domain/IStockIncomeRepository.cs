using POS.Domain.Models.DataModels;
using POS.Domain.Models.ViewModels;
using POS.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Repositories.Domain
{
    public interface IStockIncomeRepository:IBaseRepository<StockIncomeEntity>
    {
        bool UpdateAsync(StockIncomeEntity entity);
        bool DeleteAsync(StockIncomeEntity entity);
    }
}
