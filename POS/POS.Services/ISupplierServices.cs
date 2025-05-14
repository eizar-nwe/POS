using POS.Domain.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Services
{
    public interface ISupplierServices
    {
        bool Create(SupplierViewModel supplierVM);
        IEnumerable<SupplierViewModel> GetAll();
        SupplierViewModel GetById(string id);
        bool Update(SupplierViewModel supplierVM);
        bool Delete(String id);
    }
}
