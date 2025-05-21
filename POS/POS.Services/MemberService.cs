using POS.Domain.Models.DataModels;
using POS.Domain.Models.ViewModels;
using POS.Domain.Utilities;
using POS.UnitOfWorks;

namespace POS.Services
{
    public class MemberService : IMemberService
    {        
        private readonly IUnitOfWork _unitOfWork;

        public MemberService(IUnitOfWork unitOfWork)
        {            
            this._unitOfWork = unitOfWork;
        }
        private async Task<string> GetIpAsync()
        {
            return await NetworkHelper.GetIpAddressAsnyc();
        }
        public bool Create(MemberViewModel memberVM)
        {
            try
            {
                MemberEntity memberEntity = new MemberEntity()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = memberVM.Name,
                    Email = memberVM.Email,
                    Phone = memberVM.Phone,
                    Address = memberVM.Address,
                    MemberDate= memberVM.MemberDate,
                    Remarks = memberVM.Remarks,
                    CreatedAt = DateTime.Now,
                    CreatedBy = "System",
                    IsActive = true,
                    Ip = GetIpAsync().Result
                };
                _unitOfWork.MemberRepository.Create(memberEntity);
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
                MemberEntity memEntity = _unitOfWork.MemberRepository.GetAll(w => w.IsActive && w.Id == id).FirstOrDefault();
                if (memEntity is not null)
                {
                    memEntity.IsActive = false;
                    _unitOfWork.MemberRepository.Delete(memEntity);
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

        public IEnumerable<MemberViewModel> GetAll()
        {
            return _unitOfWork.MemberRepository.GetAll(w => w.IsActive).Select(s => new MemberViewModel
            {
                Id=s.Id,
                Name=s.Name,
                Email=s.Email,
                Phone=s.Phone,
                Address=s.Address,
                MemberDate=s.MemberDate,
                Remarks=s.Remarks,
                Ip = s.Ip,
                CreatedAt = s.CreatedAt,
                CreatedBy = s.CreatedBy,
                UpdatedAt = s.UpdatedAt,
                UpdatedBy = s.UpdatedBy
            }).OrderByDescending(o=>o.CreatedAt).ToList();
        }

        public MemberViewModel GetById(string id)
        {
            return _unitOfWork.MemberRepository.GetAll(w => w.IsActive && w.Id == id).Select(s => new MemberViewModel
            {
                Id = s.Id,
                Name = s.Name,
                Email = s.Email,
                Phone = s.Phone,
                Address = s.Address,
                MemberDate=s.MemberDate,
                Remarks = s.Remarks,
                Ip = s.Ip,
                CreatedAt = s.CreatedAt,
                CreatedBy = s.CreatedBy,
                UpdatedAt = s.UpdatedAt,
                UpdatedBy = s.UpdatedBy
            }).FirstOrDefault();
        }

        public bool Update(MemberViewModel memberVM)
        {
            try
            {
                MemberEntity memEntity = _unitOfWork.MemberRepository.GetAll(w => w.IsActive && w.Id == memberVM.Id).FirstOrDefault();
                if (memEntity is not null)
                {
                    memEntity.Name = memberVM.Name;
                    memEntity.Email = memberVM.Email;
                    memEntity.Phone = memberVM.Phone;
                    memEntity.Address = memberVM.Address;
                    memEntity.MemberDate = memberVM.MemberDate;
                    memEntity.Remarks = memberVM.Remarks;
                    memEntity.UpdatedAt = DateTime.Now;
                    memEntity.UpdatedBy = "System";
                    memEntity.IsActive = true;
                    memEntity.Ip = GetIpAsync().Result;

                    _unitOfWork.MemberRepository.Update(memEntity);
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
