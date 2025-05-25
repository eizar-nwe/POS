using POS.Domain.Models.DataModels;
using POS.Domain.Models.ViewModels;
using POS.Domain.Utilities;
using POS.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Services
{
    public class StockIncomeService : IStockIncomeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StockIncomeService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        #region "GetData"        
        public IEnumerable<StockIncomeViewModel> GetAll()
        {
            return (from i in _unitOfWork.StockIncomeRepository.GetAll(w => w.IsActive)
                    join g in _unitOfWork.StockGroupRepository.GetAll(w => w.IsActive)
                    on i.STOCKGRP_ID equals g.Id

                    join t in _unitOfWork.StockItemRepository.GetAll(w => w.IsActive)
                    on new { grpId = i.STOCKGRP_ID, itmId = i.STOCKITEM_ID }
                    equals new { grpId = t.StockGrp_Id, itmId = t.Id }

                    join s in _unitOfWork.SupplierRepository.GetAll(w => w.IsActive)
                    on i.SUPPLIER_ID equals s.Id

                    select new StockIncomeViewModel
                    {
                        Id = i.Id,
                        LINE_ID = i.LINE_ID,
                        STOCKGRP_ID = i.STOCKGRP_ID,
                        STOCKITEM_ID = i.STOCKITEM_ID,
                        SUPPLIER_ID = i.SUPPLIER_ID,
                        PRICE = i.PRICE,
                        QUANTITY = i.QUANTITY,
                        Remarks = i.Remarks,
                        CreatedAt = i.CreatedAt,
                        CreatedBy = i.CreatedBy,
                        UpdatedAt = i.UpdatedAt,
                        UpdatedBy = i.UpdatedBy,
                        IsActive = i.IsActive,
                        Ip = i.Ip,
                        StkGrpInfo = g.Code,
                        StkItemInfo = t.Code,
                        SupplierInfo = s.Name
                    }).OrderByDescending(x => x.CreatedAt).ToList();
        }
        public IEnumerable<StockIncomeViewModel> GetById(string id)        
        {
            return (from i in _unitOfWork.StockIncomeRepository.GetAll(w => w.IsActive)
                    join g in _unitOfWork.StockGroupRepository.GetAll(w => w.IsActive)
                    on i.STOCKGRP_ID equals g.Id

                    join t in _unitOfWork.StockItemRepository.GetAll(w => w.IsActive)
                    on new { grpId = i.STOCKGRP_ID, itmId = i.STOCKITEM_ID }
                    equals new { grpId = t.StockGrp_Id, itmId = t.Id }

                    join s in _unitOfWork.SupplierRepository.GetAll(w => w.IsActive)
                    on i.SUPPLIER_ID equals s.Id

                    where i.Id == id

                    select new StockIncomeViewModel
                    {
                        Id = i.Id,
                        LINE_ID = i.LINE_ID,
                        STOCKGRP_ID = i.STOCKGRP_ID,
                        STOCKITEM_ID = i.STOCKITEM_ID,
                        SUPPLIER_ID = i.SUPPLIER_ID,
                        PRICE = i.PRICE,
                        QUANTITY = i.QUANTITY,
                        Remarks = i.Remarks,
                        CreatedAt = i.CreatedAt,
                        CreatedBy = i.CreatedBy,
                        UpdatedAt = i.UpdatedAt,
                        UpdatedBy = i.UpdatedBy,
                        IsActive = i.IsActive,
                        Ip = i.Ip,
                        StkGrpInfo = g.Code,
                        StkItemInfo = t.Code,
                        SupplierInfo = s.Name
                    }).OrderByDescending(x => x.CreatedAt).ToList();
        }
        #endregion
        
        #region "IUD"       
        public string Create(StockIncomeViewModel stkIncomeVM)
        {
            try
            {
                string chk = chkRecordExist("ADD",stkIncomeVM);
                if (chk =="AE")
                {
                    return chk; //Already Exist
                }

                if (stkIncomeVM.Id is null)
                {
                    stkIncomeVM.Id = Guid.NewGuid().ToString();
                }

                StockIncomeEntity stkIncomeEntity = new StockIncomeEntity()
                {                    
                    Id = stkIncomeVM.Id,
                    STOCKGRP_ID = stkIncomeVM.STOCKGRP_ID,
                    STOCKITEM_ID = stkIncomeVM.STOCKITEM_ID,
                    SUPPLIER_ID = stkIncomeVM.SUPPLIER_ID,
                    PRICE = stkIncomeVM.PRICE,
                    QUANTITY = stkIncomeVM.QUANTITY,
                    Remarks = stkIncomeVM.Remarks,
                    CreatedAt = DateTime.Now,
                    CreatedBy = "System",
                    IsActive = true,
                    Ip = GetIpAsync().Result
                };
                _unitOfWork.StockIncomeRepository.Create(stkIncomeEntity);
                _unitOfWork.Commit();
                return stkIncomeEntity.Id.ToString();
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                return "";
            }
        }
        public string Update(StockIncomeViewModel stkIncomeVM)
        {
            try
            {
                string chk = chkRecordExist("UPDATE",stkIncomeVM);
                if (chk == "AE")
                {
                    return "Record already exist."; //Already Exist
                }

                StockIncomeEntity stkIncomeEntity = _unitOfWork.StockIncomeRepository.GetAll(w => w.IsActive && w.Id == stkIncomeVM.Id && w.LINE_ID == stkIncomeVM.LINE_ID).FirstOrDefault();
                                     
                if (stkIncomeEntity != null)
                {
                    // Update fields
                    stkIncomeEntity.STOCKGRP_ID = stkIncomeVM.STOCKGRP_ID;
                    stkIncomeEntity.STOCKITEM_ID = stkIncomeVM.STOCKITEM_ID;
                    stkIncomeEntity.SUPPLIER_ID = stkIncomeVM.SUPPLIER_ID;
                    stkIncomeEntity.PRICE = stkIncomeVM.PRICE;
                    stkIncomeEntity.QUANTITY = stkIncomeVM.QUANTITY;
                    stkIncomeEntity.Remarks = stkIncomeVM.Remarks;
                    stkIncomeEntity.UpdatedAt = DateTime.Now;
                    stkIncomeEntity.UpdatedBy = "System";
                    stkIncomeEntity.IsActive = true;
                    stkIncomeEntity.Ip = GetIpAsync().Result;

                    bool result = _unitOfWork.StockIncomeRepository.UpdateAsync(stkIncomeEntity);
                    if (result)
                    {
                        _unitOfWork.Commit();
                        return "Record has been updated successfully.";
                    }
                    else
                    {                        
                        _unitOfWork.Rollback();
                        return "Error was occured when the record is updated.";
                    }                    
                }
                else return "Record does not exist.";
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                return "Error was occured when the record is updated.";
            }            
        }
        public string DeleteRec(string id)
        {
            try
            {
                IEnumerable<StockIncomeEntity> stkIncomeEntity = _unitOfWork.StockIncomeRepository.GetAll(w => w.IsActive && w.Id == id).ToList();
                if (stkIncomeEntity is not null)
                {
                    _unitOfWork.StockIncomeRepository.DeleteRange(stkIncomeEntity);
                    _unitOfWork.Commit();
                    return "Record has been deleted successfully.";
                }
                else return "Record does not exist.";
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                return "Error was occured when the record is deleted.";
            }
        }
        public string DeleteItem(string id, int line_id)
        {
            try
            {
                StockIncomeEntity stkIncomeEntity = _unitOfWork.StockIncomeRepository.GetAll(w => w.IsActive && w.Id == id && w.LINE_ID== line_id).FirstOrDefault();
                if (stkIncomeEntity is not null)
                {

                    bool result = _unitOfWork.StockIncomeRepository.DeleteAsync(stkIncomeEntity);
                    if (result)
                    {
                        _unitOfWork.Commit();
                        return "Record has been deleted successfully.";
                    }
                    else
                    {
                        _unitOfWork.Rollback();
                        return "Error was occured when the record is deleted.";
                    }                    
                }
                else return "Record does not exist.";
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                return "Error was occured when the record is deleted.";
            }            
        }
        #endregion

        #region "helper" 
        private async Task<string> GetIpAsync()
        {
            return await NetworkHelper.GetIpAddressAsnyc();
        }
        private string chkRecordExist(string caller,StockIncomeViewModel stkIncomeVM)
        {
            StockIncomeEntity chk;
            if (caller == "ADD")
            {
                chk = _unitOfWork.StockIncomeRepository.GetAll(w => w.IsActive && w.Id == stkIncomeVM.Id && w.SUPPLIER_ID == stkIncomeVM.SUPPLIER_ID && w.STOCKGRP_ID == stkIncomeVM.STOCKGRP_ID && w.STOCKITEM_ID == stkIncomeVM.STOCKITEM_ID).FirstOrDefault();
            }
            else
            {
                chk = _unitOfWork.StockIncomeRepository.GetAll(w => w.IsActive && w.Id == stkIncomeVM.Id && w.LINE_ID != stkIncomeVM.LINE_ID && w.SUPPLIER_ID == stkIncomeVM.SUPPLIER_ID && w.STOCKGRP_ID == stkIncomeVM.STOCKGRP_ID && w.STOCKITEM_ID == stkIncomeVM.STOCKITEM_ID).FirstOrDefault();
            }
                
            if (chk is not null)
            {
                return "AE"; //Already Exist
            }
            return "";
        }
        #endregion
    }
}
