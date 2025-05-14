using POS.Domain.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Services
{
    public interface IStockGroupService
    {
        bool Create(StockGroupViewModel stockGroupVM);
        IEnumerable<StockGroupViewModel> GetAll();
        StockGroupViewModel GetById(string id);
        bool Update(StockGroupViewModel stockGroupVM);
        bool Delete(string id);
    }
}
