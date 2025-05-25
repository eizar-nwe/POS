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
    public class SaleOrderHaderService : ISaleOrderHaderService
    {
        private readonly IUnitOfWork _unitOfWork;
        public SaleOrderHaderService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public string Create(SaleOrderHaderViewModel SaleHdrVM)
        {
            throw new NotImplementedException();
        }

        public string Delete(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SaleOrderHaderViewModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SaleOrderHaderViewModel> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public string Update(SaleOrderHaderViewModel SaleHdrVM)
        {
            throw new NotImplementedException();
        }
    }
}
