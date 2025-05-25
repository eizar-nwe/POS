using Microsoft.AspNetCore.Mvc;
using POS.Domain.Models.ViewModels;
using POS.Services;

namespace POS.Web.Controllers
{
    public class SaleOrderController : Controller
    {
        private readonly ISaleOrderHaderService _hdrService;
        private readonly ISaleOrderDetailService _dtlService;
        private readonly IStockGroupService _grpService;
        private readonly IStockItemService _itemService;
        private readonly IMemberService _memberService;

        public SaleOrderController(ISaleOrderHaderService hdrService, ISaleOrderDetailService dtlService, IStockGroupService grpService,IStockItemService itemService,IMemberService memberService)
        {
            this._hdrService = hdrService;
            this._dtlService = dtlService;
            this._grpService = grpService;
            this._itemService = itemService;
            this._memberService = memberService;
        }
        public IActionResult List()
        {
            return View();
        }
        public IActionResult Entry_Order()
        {
            SaleOrderViewModel SaleVM = new SaleOrderViewModel
            {
                StkGrpVM = _grpService.GetAll().ToList(),
                StkItemVM = _itemService.GetAll().ToList(),
                MemberVM = _memberService.GetAll().ToList(),
            };

            return View(SaleVM);
        }
    }
}
