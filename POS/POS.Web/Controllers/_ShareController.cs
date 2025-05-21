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
        public string FilterByStkItem(string stkItem)
        {
            //var allStkGrp = _stockGroupService.GetAll().ToList();

            var allStkItem = _stockItemService.GetAll().ToList();
            string GrpIds = allStkItem.Where(p => p.Id == stkItem)
                .Select(s => s.StockGrp_Id).FirstOrDefault();

            return GrpIds;

            //if (stkItem != "x")
            //{
            //    var allStkItem = _stockItemService.GetAll().ToList();                
            //    var GrpIds = allStkItem.Where(p => p.Id == stkItem)
            //        .Select(s => s.StockGrp_Id).Distinct().ToList();
                
                //var filteredGrp = allStkGrp.Where(p => GrpIds.Contains(p.Id)).ToList();

                //return PartialView("_GroupListPartial", filteredGrp);
            //}            
            //else return PartialView("_GroupListPartial", allStkGrp);
        }

    }
}
