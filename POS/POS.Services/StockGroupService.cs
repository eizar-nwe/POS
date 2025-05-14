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
    public class StockGroupService : IStockGroupService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StockGroupService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        private async Task<string> GetIpAsync()
        {
            return await NetworkHelper.GetIpAddressAsnyc();
        } 
        public bool Create(StockGroupViewModel stockGroupVM)
        {
            try
            {
                StockGroupEntity stockGroupEntity = new StockGroupEntity()
                {
                    Id = Guid.NewGuid().ToString(),
                    Code=stockGroupVM.Code,
                    Description= stockGroupVM.Description,
                    CreatedAt=DateTime.Now,
                    CreatedBy="System",
                    IsActive=true,
                    Remarks= stockGroupVM.Remarks,
                    Ip = GetIpAsync().Result
                };
                _unitOfWork.StockGroupRepository.Create(stockGroupEntity);
                _unitOfWork.Commit();
                return true;
            }
            catch(Exception ex)
            {
                _unitOfWork.Rollback();
                return false;
            }
        }

        public bool Delete(string id)
        {
            try
            {
                StockGroupEntity stockGroupEntity = _unitOfWork.StockGroupRepository.GetAll(w => w.IsActive && w.Id == id).FirstOrDefault();

                if (stockGroupEntity is not null)
                {
                    stockGroupEntity.IsActive = false;
                    _unitOfWork.StockGroupRepository.Delete(stockGroupEntity);
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

        public IEnumerable<StockGroupViewModel> GetAll()
        {
            return _unitOfWork.StockGroupRepository.GetAll(w => w.IsActive).Select(s => new StockGroupViewModel
            {
                Id=s.Id,
                Code=s.Code,
                Description=s.Description,
                Remarks=s.Remarks,
                Ip=s.Ip,
                CreatedAt=s.CreatedAt,
                CreatedBy=s.CreatedBy,
                UpdatedAt=s.UpdatedAt,
                UpdatedBy=s.UpdatedBy
            }).OrderByDescending(x => x.CreatedAt).ToList();
        }

        public StockGroupViewModel GetById(string id)
        {
            return _unitOfWork.StockGroupRepository.GetAll(w => w.IsActive && w.Id==id).Select(s => new StockGroupViewModel
            {
                Id = s.Id,
                Code = s.Code,
                Description = s.Description,
                Remarks = s.Remarks,
                Ip = s.Ip,
                CreatedAt = s.CreatedAt,
                CreatedBy = s.CreatedBy,
                UpdatedAt = s.UpdatedAt,
                UpdatedBy = s.UpdatedBy
            }).FirstOrDefault();
        }

        public bool Update(StockGroupViewModel stockGroupVM)
        {
            try
            {
                StockGroupEntity stockGroupEntity = _unitOfWork.StockGroupRepository.GetAll(w => w.IsActive && w.Id == stockGroupVM.Id).FirstOrDefault();

                if (stockGroupEntity is not null)
                {
                    stockGroupEntity.Description = stockGroupVM.Description;
                    stockGroupEntity.Remarks = stockGroupVM.Remarks;
                    stockGroupEntity.UpdatedAt = DateTime.Now;
                    stockGroupEntity.UpdatedBy = "System";
                    stockGroupEntity.IsActive = true;                    
                    stockGroupEntity.Ip = GetIpAsync().Result;

                    _unitOfWork.StockGroupRepository.Update(stockGroupEntity);
                    _unitOfWork.Commit();
                }
                return true;
            }
            catch(Exception ex)
            {
                _unitOfWork.Rollback();
                return false;
            }
        }
    }
}
