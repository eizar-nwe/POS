using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using POS.Domain.Models.DataModels;
using POS.Domain.Models.ViewModels;
using POS.Domain.Utilities;
using POS.UnitOfWorks;
using System;
using System.Collections.Immutable;
using System.Security.Cryptography;

namespace POS.Services
{
    public class SaleOrderService : ISaleOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        public SaleOrderService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        #region "GetData"        
        public IEnumerable<StockIncomeViewModel> GetAll()
        {
            return null;
        }
        public async Task<SaleOrderViewModel> GetTotalSumsAsync(string id)
        {
            return await _unitOfWork.SaleOrderDetailRepository.GetAll(w => w.IsActive && w.Id== id)
                .AsQueryable()
                .GroupBy(p => 1)                
                .Select(g => new SaleOrderViewModel
                {
                    SUB_TOT = g.Sum(p => p.TOT_AMT ?? 0m),
                    DISC_TOT = g.Sum(p => p.TOT_DISC_AMT ?? 0m),
                    ALL_TOT = g.Sum(p => p.NET_AMT ?? 0m)
                }).FirstOrDefaultAsync();
        }
        public IEnumerable<SaleOrderDetailViewModel> GetDetlById(string id)
        {
            return (from i in _unitOfWork.SaleOrderDetailRepository.GetAll(w => w.IsActive)
                    //join g in _unitOfWork.StockGroupRepository.GetAll(w => w.IsActive)
                    //on i.STOCKGRP_ID equals g.Id

                    join t in _unitOfWork.StockItemRepository.GetAll(w => w.IsActive)
                    on new { grpId = i.STOCKGRP_ID, itmId = i.STOCKITEM_ID }
                    equals new { grpId = t.StockGrp_Id, itmId = t.Id }

                    where i.Id == id

                    select new SaleOrderDetailViewModel
                    {
                        Id = i.Id,
                        LINE_ID = i.LINE_ID,
                        STOCKGRP_ID = i.STOCKGRP_ID,
                        STOCKITEM_ID = i.STOCKITEM_ID,                        
                        PRICE = i.PRICE,
                        QUANTITY = i.QUANTITY,
                        DISC_TYPE=i.DISC_TYPE,
                        DISC_AMOUNT=i.DISC_AMOUNT,
                        TOT_DISC_AMT=i.TOT_DISC_AMT,
                        TOT_AMT=i.TOT_AMT,
                        NET_AMT=i.NET_AMT,

                        Remarks = i.Remarks,
                        CreatedAt = i.CreatedAt,
                        CreatedBy = i.CreatedBy,
                        UpdatedAt = i.UpdatedAt,
                        UpdatedBy = i.UpdatedBy,
                        IsActive = i.IsActive,
                        Ip = i.Ip,                        
                        StkItemDesc = string.IsNullOrEmpty(t.Description)? t.Code: t.Description,
                    }).OrderByDescending(x => x.CreatedAt).ToList();
        }
        #endregion
        public async Task<(string hId, string Msg)> AddItem(string id,string GrpId, string ItemId,string memId, string ordrType, decimal qnty,string rmk)
        {
            try
            {
                bool chkhdr = false;
                if (id == "" || id == null) //Header does not exist, 1st Item Add
                {
                    id = Guid.NewGuid().ToString();
                    chkhdr = true;
                }
                else { //after delete all items
                    chkhdr = chkRecordExist(id);
                }

                if (chkhdr)
                { 
                    SaleOrderHaderEntity hdrEntity = new SaleOrderHaderEntity
                    {
                        Id = id,
                        TRNS_DATE = DateOnly.FromDateTime(DateTime.Now),
                        CASHIER_ID = "0b24971b-633b-48da-af8d-6d90be1af35d",                        
                        MEMBER_ID = memId,
                        ORDR_TYPE = ordrType,
                        TOT_DISC_AMT = 0,
                        TOT_AMT = 0,
                        NET_AMT = 0,
                        Remarks = "",
                        CreatedAt = DateTime.Now,
                        CreatedBy = "System",
                        Ip = GetIpAsync().Result,
                        IsActive = false
                    };

                    _unitOfWork.SaleOrderHaderRepository.Create(hdrEntity);
                    _unitOfWork.Commit();
                }

                int retval = await _unitOfWork.SaleOrderDetailRepository.Execute_SaleDetl_Procedure("I",id,0, GrpId, ItemId, qnty, rmk, "System");
                
                if (retval == 1)
                {
                    _unitOfWork.Rollback();
                    return (id, "Error in adding item.");
                }
                
                return (id, "");
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                return (id, "");
            }
        }

        public async Task<string> UpdateItem(string cmd, string id, int lineId, string GrpId, string ItemId, decimal qnty, string rmk)
        {
            try
            {                
                int retval = await _unitOfWork.SaleOrderDetailRepository.Execute_SaleDetl_Procedure(cmd, id,lineId, GrpId, ItemId, qnty, rmk, "System");

                if (retval == 1)
                {
                    _unitOfWork.Rollback();
                    return "Error in adding item.";
                }

                return "";
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                return "";
            }
        }

        public async Task<string> AddPayment(string id, string payMethod, string cardNo,string ordrtype)
        {
            try
            {
                SaleOrderViewModel saleTotal = await GetTotalSumsAsync(id);
                int retval = await _unitOfWork.PaymentRepository.Execute_Payment_Procedure(id, payMethod, cardNo, "System", saleTotal, ordrtype);

                if (retval == 1)
                {
                    _unitOfWork.Rollback();
                    return "Error in adding item.";
                }

                return "";
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                return "";
            }
        }

        #region "helper"
        private async Task<string> GetIpAsync()
        {
            return await NetworkHelper.GetIpAddressAsnyc();
        }
        private bool chkRecordExist(string id)
        {
            SaleOrderHaderEntity hdrChk = _unitOfWork.SaleOrderHaderRepository.GetAll(w => w.Id == id).FirstOrDefault();

            if (hdrChk is not null)
            {
                return false; //Already Exist
            }
            return true;
        }

        public Task<string> UpdateItem(string id, int LineId, string GrpId, string ItemId, string memId, decimal qnty, string rmk)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
