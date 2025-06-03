using Microsoft.AspNetCore.Mvc;
using POS.Domain.Models.ViewModels;
using POS.Services;
using System;
using System.Security.Cryptography;

namespace POS.Web.Controllers
{
    public class SaleOrderController : Controller
    {
        private readonly ISaleOrderService _saleOrdrService;        
        private readonly IStockGroupService _grpService;
        private readonly IStockItemService _itemService;
        private readonly IMemberService _memberService;

        public SaleOrderController(ISaleOrderService SaleOrdrService, IStockGroupService grpService,IStockItemService itemService,IMemberService memberService)
        {           
            this._saleOrdrService = SaleOrdrService;            
            this._grpService = grpService;
            this._itemService = itemService;
            this._memberService = memberService;
        }
        public IActionResult List()
        {
            SaleOrderViewModel SaleVM = new SaleOrderViewModel
            {
                StkGrpVM = _grpService.GetAll().ToList(),
                StkItemVM = _itemService.GetAll().ToList(),
                MemberVM = _memberService.GetAll().ToList(),
            };
            return View(SaleVM);
        }
        public IActionResult Entry_Order()
        {
            SaleOrderViewModel SaleVM = new SaleOrderViewModel
            {
                StkGrpVM = _grpService.GetAll().OrderBy(x => x.Description)
                    .ThenBy(x => x.Code)
                    .ToList(),
                StkItemVM = _itemService.GetAll()
                    .OrderBy(x => x.Description)
                    .ThenBy(x => x.Code)
                    .ToList(),
                MemberVM = _memberService.GetAll().OrderBy(x => x.Name).ToList(),
            };

            return View(SaleVM);
        }

        [HttpPost]
        public async Task<IActionResult> AddItem(string id,int lineId, string GrpId, string ItemId,string memId, string ordrType, string rmk)
        {
            string hdrId = "";string retMsg = "";
            if (lineId == 0) //add new item
            {
                var (hId, Msg) = await _saleOrdrService.AddItem(id, GrpId, ItemId, memId, ordrType, 1, rmk);
                hdrId = hId;
                retMsg = Msg;
            }
            else //add Quantity
            {
                string Msg = await _saleOrdrService.UpdateItem("U",id, lineId, GrpId, ItemId, 1, rmk);
                hdrId = id;
                retMsg = Msg;
            }

            var retVal = GetById(hdrId);
            var retTot = GetTotal(hdrId);

            var retSuccess = new
                {
                    message = retMsg,                    
                    resVal = retVal,
                    resTot = retTot
            };
            return Json(retSuccess);
        }

        [HttpPost]
        public async Task<IActionResult> ReduceItem(string id, int lineId, string GrpId, string ItemId)
        {
            string retMsg = await _saleOrdrService.UpdateItem("R",id, lineId, GrpId, ItemId, 1, "");           
            
            var retVal = GetById(id);
            var retTot = GetTotal(id);

            var retSuccess = new
            {
                message = retMsg,
                resVal = retVal,
                resTot = retTot
            };
            return Json(retSuccess);
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder(string id, string payMethod, string cardNo,string ordrtype)
        {
            string retMsg = await _saleOrdrService.AddPayment(id, payMethod, cardNo, ordrtype);

            var retSuccess = new
            {
                message = retMsg,
                resVal = "" 
            };
            return Json(retSuccess);
        }

        #region "Helper"
        private IEnumerable<SaleOrderDetailViewModel> GetById(string id)
        {
            return _saleOrdrService.GetDetlById(id);
        }
        private async Task<SaleOrderViewModel> GetTotal(string id)
        {
            return await _saleOrdrService.GetTotalSumsAsync(id);
        }
        #endregion
    }
}
