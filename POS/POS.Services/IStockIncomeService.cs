using POS.Domain.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Services
{
    public interface IStockIncomeService
    {
        string Create(StockIncomeViewModel stkIncomeVM);
        IEnumerable<StockIncomeViewModel> GetAll();
        IEnumerable<StockIncomeViewModel> GetById(string id);
        string Update(StockIncomeViewModel stkIncomeVM);
        string DeleteRec(string id);
        string DeleteItem(string id, int line_id);
    }
}
