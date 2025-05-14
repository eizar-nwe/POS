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
    public class SupplierService : ISupplierServices
    {        
        private readonly IUnitOfWork _unitOfWork;

        public SupplierService(IUnitOfWork unitOfWork)
        {            
            this._unitOfWork = unitOfWork;
        }
        private async Task<string> GetIpAsync()
        {
            return await NetworkHelper.GetIpAddressAsnyc();
        }
        public bool Create(SupplierViewModel supplierVM)
        {
            try
            {
                SupplierEntity supplierEntity = new SupplierEntity()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = supplierVM.Name,
                    Email = supplierVM.Email,
                    Phone = supplierVM.Phone,
                    Address = supplierVM.Address,
                    Remarks = supplierVM.Remarks,
                    CreatedAt = DateTime.Now,
                    CreatedBy = "System",
                    IsActive = true,
                    Ip = GetIpAsync().Result
                };
                _unitOfWork.SupplierRepository.Create(supplierEntity);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                return false; 
            }

        }

        public bool Delete(string id)
        {
            try
            {
                SupplierEntity suprEntity = _unitOfWork.SupplierRepository.GetAll(w => w.IsActive && w.Id == id).FirstOrDefault();
                if (suprEntity is not null)
                {
                    suprEntity.IsActive = false;
                    _unitOfWork.SupplierRepository.Delete(suprEntity);
                    _unitOfWork.Commit();
                }
                return true;
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                return false;                
            }
        }

        public IEnumerable<SupplierViewModel> GetAll()
        {
            return _unitOfWork.SupplierRepository.GetAll(w => w.IsActive).Select(s => new SupplierViewModel
            {
                Id=s.Id,
                Name=s.Name,
                Email=s.Email,
                Phone=s.Phone,
                Address=s.Address,
                Remarks=s.Remarks,
                Ip = s.Ip,
                CreatedAt = s.CreatedAt,
                CreatedBy = s.CreatedBy,
                UpdatedAt = s.UpdatedAt,
                UpdatedBy = s.UpdatedBy
            }).OrderByDescending(o=>o.CreatedAt).ToList();
        }

        public SupplierViewModel GetById(string id)
        {
            return _unitOfWork.SupplierRepository.GetAll(w => w.IsActive && w.Id == id).Select(s => new SupplierViewModel
            {
                Id = s.Id,
                Name = s.Name,
                Email = s.Email,
                Phone = s.Phone,
                Address = s.Address,
                Remarks = s.Remarks,
                Ip = s.Ip,
                CreatedAt = s.CreatedAt,
                CreatedBy = s.CreatedBy,
                UpdatedAt = s.UpdatedAt,
                UpdatedBy = s.UpdatedBy
            }).FirstOrDefault();
        }

        public bool Update(SupplierViewModel supplierVM)
        {
            try
            {
                SupplierEntity suprEntity = _unitOfWork.SupplierRepository.GetAll(w => w.IsActive && w.Id == supplierVM.Id).FirstOrDefault();
                if (suprEntity is not null)
                {
                    suprEntity.Email = supplierVM.Email;
                    suprEntity.Phone = supplierVM.Phone;
                    suprEntity.Address = supplierVM.Address;
                    suprEntity.Remarks = supplierVM.Remarks;
                    suprEntity.UpdatedAt = DateTime.Now;
                    suprEntity.UpdatedBy = "System";
                    suprEntity.IsActive = true;
                    suprEntity.Ip = GetIpAsync().Result;

                    _unitOfWork.SupplierRepository.Update(suprEntity);
                    _unitOfWork.Commit();
                }
                return true;
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                return false;
            }
        }
    }
}
