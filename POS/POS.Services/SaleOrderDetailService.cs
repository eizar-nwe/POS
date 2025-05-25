using Microsoft.IdentityModel.Tokens;
using POS.Domain.Models.DataModels;
using POS.Domain.Models.ViewModels;
using POS.Domain.Utilities;
using POS.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Services
{
    public class SaleOrderDetailService : ISaleOrderDetailService
    {
        private readonly IUnitOfWork _unitOfWork;
        public SaleOrderDetailService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public string Create(SaleOrderDetailViewModel SaleDtlVM)
        {
            throw new NotImplementedException();
        }

        public string Delete(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SaleOrderDetailViewModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SaleOrderDetailViewModel> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public string Update(SaleOrderDetailViewModel SaleDtlVM)
        {
            throw new NotImplementedException();
        }
    }
}
