using POS.Domain.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Services
{
    public interface IStockItemService
    {
        bool Create(StockItemViewModel stockItemVM);
        IEnumerable<StockItemViewModel> GetAll();
        StockItemViewModel GetByID(string id);
        bool Update(StockItemViewModel stockItemVM);
        bool Delete(string id, string WebRootPath);
    }
}
