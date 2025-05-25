using Microsoft.AspNetCore.Mvc;
using POS.Services;

namespace POS.Web.Controllers
{
    public class _ShareController : Controller
    {
        private readonly IStockGroupService _stockGroupService;
        private readonly IStockItemService _stockItemService;

        public _ShareController(IStockGroupService stockGroupService,IStockItemService stockItemService)
        {
            this._stockGroupService = stockGroupService;
            this._stockItemService = stockItemService;
        }

        [HttpGet]
        public IActionResult FilterByStkGrp(string stkGrp)
        {
            var allStkItem = _stockItemService.GetAll().ToList();

            if(stkGrp != "x")
            {
               var filteredItem = allStkItem.Where(p => p.StockGrp_Id == stkGrp).ToList();

                return PartialView("_ItemListPartial", filteredItem);
            }
            else return PartialView("_ItemListPartial", allStkItem);
        }
        [HttpGet]
        public IActionResult FilterByStkItem(string stkItem)
        {            
            var allStkItem = _stockItemService.GetAll().ToList();
            string GrpId = allStkItem.Where(p => p.Id == stkItem)
                .Select(s => s.StockGrp_Id).FirstOrDefault() ?? "";

            decimal sPrice = 0;
            if (!string.IsNullOrEmpty(GrpId))
            {
                sPrice = allStkItem
                .Where(p => p.StockGrp_Id == GrpId && p.Id == stkItem)
                .Select(s => s.Sell_Price)
                .FirstOrDefault() ?? 0m;
            }            

            var retSuccess = new
            {
                Price = sPrice,
                StkGrpId = GrpId
            };
            return Json(retSuccess);
        }

    }
}
