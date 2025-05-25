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
    public class MemberDiscountService : IMemberDiscountService
    {
        private readonly IUnitOfWork _unitOfWork;
        public MemberDiscountService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        #region "GetData"        
        public IEnumerable<MemberDiscountViewModel> GetAll()
        {
            return (from i in _unitOfWork.MemberDiscountRepository.GetAll(w => w.IsActive)                    
                    join t in _unitOfWork.StockItemRepository.GetAll(w => w.IsActive)
                    on new { grpId = i.STOCKGRP_ID, itmId = i.STOCKITEM_ID }
                    equals new { grpId = t.StockGrp_Id, itmId = t.Id }
                    
                    join g in _unitOfWork.StockGroupRepository.GetAll(w => w.IsActive)
                    on i.STOCKGRP_ID equals g.Id
                    
                    join m in _unitOfWork.MemberRepository.GetAll(w => w.IsActive)
                    on i.MEMBER_ID equals m.Id

                    select new MemberDiscountViewModel
                    {
                        Id = i.Id,
                        LINE_ID = i.LINE_ID,
                        STOCKGRP_ID = i.STOCKGRP_ID,
                        STOCKITEM_ID = i.STOCKITEM_ID,
                        MEMBER_ID = i.MEMBER_ID,
                        ORIGIN_PRICE = i.ORIGIN_PRICE,
                        OFFER_PRICE = i.OFFER_PRICE,
                        Disc_Type = string.IsNullOrEmpty(i.Disc_Type)? "":i.Disc_Type,
                        Disc_Amount = i.Disc_Amount,
                        Disc_Percent = i.Disc_Percent,
                        From_Date = i.From_Date,
                        To_Date = i.To_Date,
                        Remarks = string.IsNullOrEmpty(i.Remarks) ? "" : i.Remarks,
                        CreatedAt = i.CreatedAt,
                        CreatedBy = i.CreatedBy,
                        UpdatedAt = i.UpdatedAt,
                        UpdatedBy = i.UpdatedBy,
                        IsActive = i.IsActive,
                        Ip = i.Ip,
                        StkGrpInfo = g.Code,
                        StkItemInfo = t.Code,
                        MemberInfo = m.Name
                    }).OrderByDescending(x => x.CreatedAt).ToList();
        }
        public IEnumerable<MemberDiscountViewModel> GetById(string id)
        {

            return (from i in _unitOfWork.MemberDiscountRepository.GetAll(w => w.IsActive)
                    join t in _unitOfWork.StockItemRepository.GetAll(w => w.IsActive)
                    on new { grpId = i.STOCKGRP_ID, itmId = i.STOCKITEM_ID }
                    equals new { grpId = t.StockGrp_Id, itmId = t.Id }
                    
                    join g in _unitOfWork.StockGroupRepository.GetAll(w => w.IsActive)
                    on i.STOCKGRP_ID equals g.Id
                    
                    join m in _unitOfWork.MemberRepository.GetAll(w => w.IsActive)
                    on i.MEMBER_ID equals m.Id

                    where i.Id == id

                    select new MemberDiscountViewModel
                    {
                        Id = i.Id,
                        LINE_ID = i.LINE_ID,
                        STOCKGRP_ID = i.STOCKGRP_ID,
                        STOCKITEM_ID = i.STOCKITEM_ID,
                        MEMBER_ID = i.MEMBER_ID,
                        ORIGIN_PRICE = i.ORIGIN_PRICE,
                        OFFER_PRICE = i.OFFER_PRICE,
                        Disc_Type = string.IsNullOrEmpty(i.Disc_Type) ? "" : i.Disc_Type,
                        Disc_Amount = i.Disc_Amount,
                        Disc_Percent = i.Disc_Percent,
                        From_Date = i.From_Date,
                        To_Date = i.To_Date,
                        Remarks = string.IsNullOrEmpty(i.Remarks) ? "" : i.Remarks,
                        CreatedAt = i.CreatedAt,
                        CreatedBy = i.CreatedBy,
                        UpdatedAt = i.UpdatedAt,
                        UpdatedBy = i.UpdatedBy,
                        IsActive = i.IsActive,
                        Ip = i.Ip,
                        StkGrpInfo = g.Code,
                        StkItemInfo = t.Code,
                        MemberInfo = m.Name
                    }).OrderByDescending(x => x.CreatedAt).ToList();            
        }
        #endregion

        #region "IUD"
        public string Create(MemberDiscountViewModel MemDiscVM)
        {
            try
            {
                string chk = chkRecordExist("ADD", MemDiscVM);
                if (chk == "AE")
                {
                    return chk; //Already Exist
                }

                if (MemDiscVM.Id is null)
                {
                    MemDiscVM.Id = Guid.NewGuid().ToString();
                }

                MemberDiscountEntity memDiscEntity = new MemberDiscountEntity()
                {
                    Id = MemDiscVM.Id,
                    MEMBER_ID = MemDiscVM.MEMBER_ID,
                    STOCKGRP_ID = MemDiscVM.STOCKGRP_ID,
                    STOCKITEM_ID = MemDiscVM.STOCKITEM_ID,
                    ORIGIN_PRICE = MemDiscVM.ORIGIN_PRICE,
                    OFFER_PRICE = MemDiscVM.OFFER_PRICE,
                    Disc_Type = MemDiscVM.Disc_Type,
                    Disc_Amount = MemDiscVM.Disc_Amount,
                    Disc_Percent = MemDiscVM.Disc_Percent,
                    From_Date = MemDiscVM.From_Date,
                    To_Date = MemDiscVM.To_Date,
                    Remarks = MemDiscVM.Remarks,
                    CreatedAt = DateTime.Now,
                    CreatedBy = "System",
                    IsActive = true,
                    Ip = GetIpAsync().Result
                };
                _unitOfWork.MemberDiscountRepository.Create(memDiscEntity);
                _unitOfWork.Commit();
                return memDiscEntity.Id.ToString();
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                return "";
            }
        }
        public string Update(MemberDiscountViewModel MemDiscVM)
        {
            try
            {
                string chk = chkRecordExist("UPDATE", MemDiscVM);
                if (chk == "AE")
                {
                    return "Record already exist."; //Already Exist
                }

                MemberDiscountEntity memDiscEntity = _unitOfWork.MemberDiscountRepository.GetAll(w => w.IsActive && w.LINE_ID == MemDiscVM.LINE_ID).FirstOrDefault();

                if (memDiscEntity != null)
                {
                    // Update fields
                    memDiscEntity.MEMBER_ID = MemDiscVM.MEMBER_ID;
                    memDiscEntity.STOCKGRP_ID = MemDiscVM.STOCKGRP_ID;
                    memDiscEntity.STOCKITEM_ID = MemDiscVM.STOCKITEM_ID;
                    memDiscEntity.ORIGIN_PRICE = MemDiscVM.ORIGIN_PRICE;
                    memDiscEntity.OFFER_PRICE = MemDiscVM.OFFER_PRICE;
                    memDiscEntity.Disc_Type = MemDiscVM.Disc_Type;
                    memDiscEntity.Disc_Amount = MemDiscVM.Disc_Amount;
                    memDiscEntity.Disc_Percent = MemDiscVM.Disc_Percent;
                    memDiscEntity.From_Date = MemDiscVM.From_Date;
                    memDiscEntity.To_Date = MemDiscVM.To_Date;
                    memDiscEntity.Remarks = MemDiscVM.Remarks;
                    memDiscEntity.UpdatedAt = DateTime.Now;
                    memDiscEntity.UpdatedBy = "System";
                    memDiscEntity.IsActive = true;
                    memDiscEntity.Ip = GetIpAsync().Result;

                    bool result = _unitOfWork.MemberDiscountRepository.UpdateAsync(memDiscEntity);
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
                IEnumerable<MemberDiscountEntity> memDiscEntity = _unitOfWork.MemberDiscountRepository.GetAll(w => w.IsActive && w.Id == id).ToList();
                if (memDiscEntity is not null)
                {
                    _unitOfWork.MemberDiscountRepository.DeleteRange(memDiscEntity);
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
                MemberDiscountEntity memDiscEntity = _unitOfWork.MemberDiscountRepository.GetAll(w => w.IsActive && w.Id == id && w.LINE_ID == line_id).FirstOrDefault();
                if (memDiscEntity is not null)
                {

                    bool result = _unitOfWork.MemberDiscountRepository.DeleteAsync(memDiscEntity);
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
        private string chkRecordExist(string caller, MemberDiscountViewModel MemDiscVM)
        {
            MemberDiscountEntity chk;
            if (caller == "ADD")
            {
                chk = _unitOfWork.MemberDiscountRepository.GetAll(w => w.IsActive && w.Id == MemDiscVM.Id && w.MEMBER_ID == MemDiscVM.MEMBER_ID && w.STOCKGRP_ID == MemDiscVM.STOCKGRP_ID && w.STOCKITEM_ID == MemDiscVM.STOCKITEM_ID).FirstOrDefault();
            }
            else
            {
                chk = _unitOfWork.MemberDiscountRepository.GetAll(w => w.IsActive && w.Id == MemDiscVM.Id && w.LINE_ID != MemDiscVM.LINE_ID && w.MEMBER_ID == MemDiscVM.MEMBER_ID && w.STOCKGRP_ID == MemDiscVM.STOCKGRP_ID && w.STOCKITEM_ID == MemDiscVM.STOCKITEM_ID).FirstOrDefault();
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
