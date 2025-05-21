using POS.Domain.Models.DataModels;
using POS.Domain.Models.ViewModels;
using POS.Domain.Utilities;
using POS.UnitOfWorks;

namespace POS.Services
{
    public class CashierService : ICashierService
    {        
        private readonly IUnitOfWork _unitOfWork;

        public CashierService(IUnitOfWork unitOfWork)
        {            
            this._unitOfWork = unitOfWork;
        }
        private async Task<string> GetIpAsync()
        {
            return await NetworkHelper.GetIpAddressAsnyc();
        }
        public bool Create(CashierViewModel CashierVM)
        {
            try
            {
                CashierEntity CashierEntity = new CashierEntity()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = CashierVM.Name,
                    Email = CashierVM.Email,
                    Phone = CashierVM.Phone,
                    Address = CashierVM.Address,
                    Gender= CashierVM.Gender,
                    Remarks = CashierVM.Remarks,
                    CreatedAt = DateTime.Now,
                    CreatedBy = "System",
                    IsActive = true,
                    Ip = GetIpAsync().Result
                };
                _unitOfWork.CashierRepository.Create(CashierEntity);
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
                CashierEntity cashierEntity = _unitOfWork.CashierRepository.GetAll(w => w.IsActive && w.Id == id).FirstOrDefault();
                if (cashierEntity is not null)
                {
                    cashierEntity.IsActive = false;
                    _unitOfWork.CashierRepository.Delete(cashierEntity);
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
        public IEnumerable<CashierViewModel> GetAll()
        {
            return _unitOfWork.CashierRepository.GetAll(w => w.IsActive).Select(s => new CashierViewModel
            {
                Id=s.Id,
                Name=s.Name,
                Email=s.Email,
                Phone=s.Phone,
                Address=s.Address,
                Gender=s.Gender,
                Remarks=s.Remarks,
                Ip = s.Ip,
                CreatedAt = s.CreatedAt,
                CreatedBy = s.CreatedBy,
                UpdatedAt = s.UpdatedAt,
                UpdatedBy = s.UpdatedBy
            }).OrderByDescending(o=>o.CreatedAt).ToList();
        }
        public CashierViewModel GetById(string id)
        {
            return _unitOfWork.CashierRepository.GetAll(w => w.IsActive && w.Id == id).Select(s => new CashierViewModel
            {
                Id = s.Id,
                Name = s.Name,
                Email = s.Email,
                Phone = s.Phone,
                Address = s.Address,
                Gender=s.Gender,
                Remarks = s.Remarks,
                Ip = s.Ip,
                CreatedAt = s.CreatedAt,
                CreatedBy = s.CreatedBy,
                UpdatedAt = s.UpdatedAt,
                UpdatedBy = s.UpdatedBy
            }).FirstOrDefault();
        }
        public bool Update(CashierViewModel CashierVM)
        {
            try
            {
                CashierEntity cashierEntity = _unitOfWork.CashierRepository.GetAll(w => w.IsActive && w.Id == CashierVM.Id).FirstOrDefault();
                if (cashierEntity is not null)
                {
                    cashierEntity.Name = CashierVM.Name;
                    cashierEntity.Email = CashierVM.Email;
                    cashierEntity.Phone = CashierVM.Phone;
                    cashierEntity.Address = CashierVM.Address;
                    cashierEntity.Gender = CashierVM.Gender;
                    cashierEntity.Remarks = CashierVM.Remarks;
                    cashierEntity.UpdatedAt = DateTime.Now;
                    cashierEntity.UpdatedBy = "System";
                    cashierEntity.IsActive = true;
                    cashierEntity.Ip = GetIpAsync().Result;

                    _unitOfWork.CashierRepository.Update(cashierEntity);
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
