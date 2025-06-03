using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
    public class StockItemService : IStockItemService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StockItemService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        private async Task<string> GetIpAsync()
        {
            return await NetworkHelper.GetIpAddressAsnyc();
        }
        public bool Create(StockItemViewModel stockItemVM)
        {
            try
            {                
                StockItemEntity stockItemEntity = new StockItemEntity()
                {
                    Id = Guid.NewGuid().ToString(),
                    Code = stockItemVM.Code,
                    Description = stockItemVM.Description,
                    StockGrp_Id = stockItemVM.StockGrp_Id,
                    Sell_Price = stockItemVM.Sell_Price,
                    Offer_price = stockItemVM.Offer_price,
                    BarCode = stockItemVM.BarCode,
                    img_path = stockItemVM.img_path,
                    Disc_Type = stockItemVM.Disc_Type,
                    Disc_Amount = stockItemVM.Disc_Amount,
                    Disc_Percent = stockItemVM.Disc_Percent,
                    From_Date = stockItemVM.From_Date,
                    To_Date = stockItemVM.To_Date,
                    CreatedAt = DateTime.Now,
                    CreatedBy = "System",
                    Ip = GetIpAsync().Result,
                    IsActive = true,
                    Remarks = stockItemVM.Remarks
                };
                _unitOfWork.StockItemRepository.Create(stockItemEntity);
                _unitOfWork.Commit();

                return true;                                  
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                return false;
            }
        }
        public bool Delete(string id, string WebRootPath)
        {
            try
            {
                StockItemEntity stockItemEntity = _unitOfWork.StockItemRepository.GetAll(w => w.IsActive && w.Id == id).FirstOrDefault();
                if (stockItemEntity is not null)
                {
                    stockItemEntity.IsActive = false;
                    _unitOfWork.StockItemRepository.Delete(stockItemEntity);
                    _unitOfWork.Commit();

                    // Delete image file
                    if (!string.IsNullOrEmpty(stockItemEntity.img_path))
                    {
                        string imagePath = WebRootPath + stockItemEntity.img_path;

                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }

                }
                return true;
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                return false;
            }
        }

        public IEnumerable<StockItemViewModel> GetAll()
        {
            return (from s in _unitOfWork.StockItemRepository.GetAll(w => w.IsActive)
                    join g in _unitOfWork.StockGroupRepository.GetAll(w => w.IsActive)
                    on s.StockGrp_Id equals g.Id

                    select new StockItemViewModel
                    {
                        Id = s.Id,
                        Code = s.Code,
                        Description = s.Description,
                        StockGrp_Id = s.StockGrp_Id,
                        Sell_Price = s.Sell_Price,
                        Offer_price = s.Offer_price,
                        BarCode = s.BarCode,
                        img_path = string.IsNullOrEmpty(s.img_path)? "/images/Default_Item.jpg" : s.img_path,
                        //img_path = s.img_path,
                        Disc_Type = s.Disc_Type,
                        Disc_Amount = s.Disc_Amount,
                        Disc_Percent = s.Disc_Percent,
                        From_Date = s.From_Date,
                        To_Date = s.To_Date,
                        CreatedAt = s.CreatedAt,
                        CreatedBy = s.CreatedBy,
                        UpdatedAt = s.UpdatedAt,
                        UpdatedBy = s.UpdatedBy,
                        Ip = s.Ip,
                        Remarks = s.Remarks,
                        IsActive = s.IsActive,
                        StockGrpCode = g.Code
                    }).OrderByDescending(x=>x.CreatedAt).ToList();            
        }
        public StockItemViewModel GetByID(string id)
        {

            return (from s in _unitOfWork.StockItemRepository.GetAll(w => w.IsActive)
                    join g in _unitOfWork.StockGroupRepository.GetAll(w => w.IsActive)
                    on s.StockGrp_Id equals g.Id
                    where s.Id == id

                    select new StockItemViewModel
                    {
                        Id = s.Id,
                        Code = s.Code,
                        Description = s.Description,
                        StockGrp_Id = s.StockGrp_Id,
                        Sell_Price = s.Sell_Price,
                        Offer_price = s.Offer_price,
                        BarCode = s.BarCode,
                        img_path = string.IsNullOrEmpty(s.img_path) ? "/images/Default_Item.jpg" : s.img_path,
                        Disc_Type = s.Disc_Type,
                        Disc_Amount = s.Disc_Amount,
                        Disc_Percent = s.Disc_Percent,
                        From_Date = s.From_Date,
                        To_Date = s.To_Date,
                        CreatedAt = s.CreatedAt,
                        CreatedBy = s.CreatedBy,
                        UpdatedAt = s.UpdatedAt,
                        UpdatedBy = s.UpdatedBy,
                        Ip = s.Ip,
                        Remarks = s.Remarks,
                        IsActive = s.IsActive,
                        StockGrpCode = g.Code
                    }).FirstOrDefault();
        }       
        public bool Update(StockItemViewModel stockItemVM)
        {
            try
            {              
                StockItemEntity stockItemEntity = _unitOfWork.StockItemRepository.GetAll(w => w.IsActive && w.Id == stockItemVM.Id).FirstOrDefault();
                if (stockItemEntity is not null)
                {
                    stockItemEntity.Sell_Price = stockItemVM.Sell_Price;
                    stockItemEntity.Offer_price = stockItemVM.Offer_price;
                    stockItemEntity.Description = stockItemVM.Description;
                    stockItemEntity.BarCode = stockItemVM.BarCode;
                    stockItemEntity.img_path = stockItemVM.img_path;
                    stockItemEntity.Disc_Type = stockItemVM.Disc_Type;
                    stockItemEntity.Disc_Amount = stockItemVM.Disc_Amount;
                    stockItemEntity.Disc_Percent = stockItemVM.Disc_Percent;
                    stockItemEntity.From_Date = stockItemVM.From_Date;
                    stockItemEntity.To_Date = stockItemVM.To_Date;
                    stockItemEntity.UpdatedAt = DateTime.Now;
                    stockItemEntity.UpdatedBy = "System";
                    stockItemEntity.Ip = GetIpAsync().Result;
                    stockItemEntity.IsActive = true;
                    stockItemEntity.Remarks = stockItemVM.Remarks;

                    _unitOfWork.StockItemRepository.Update(stockItemEntity);
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
