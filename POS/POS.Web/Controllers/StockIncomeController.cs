using Microsoft.AspNetCore.Mvc;
using POS.Domain.Models.ViewModels;
using POS.Services;

namespace POS.Web.Controllers
{
    public class StockIncomeController : Controller
    {
        private readonly IStockIncomeService _stockIncomeService;
        private readonly IStockGroupService _stockGroupService;
        private readonly IStockItemService _stockItemService;
        private readonly ISupplierService _supplierService;
        public StockIncomeController(IStockIncomeService stockIncomeService,IStockGroupService stockGroupService,IStockItemService stockItemService,ISupplierService supplierService)
        {
            this._stockIncomeService = stockIncomeService;
            this._stockGroupService = stockGroupService;
            this._stockItemService = stockItemService;
            this._supplierService = supplierService;
        }
        public IActionResult List() => View(_stockIncomeService.GetAll().ToList());        
        public IActionResult Entry()
        {
            StockIncomeViewModel stkIncomeVM = new StockIncomeViewModel
            {
                StkGrpVM = _stockGroupService.GetAll().ToList(),
                StkItemVM = _stockItemService.GetAll().ToList(),
                SupplierVM = _supplierService.GetAll().ToList(),
            };            
            return View(stkIncomeVM);
        }
        public IActionResult Edit(string id, string suprId, string grpId, string itemId)
        {
            StockIncomeViewModel stkIncomeVM = _stockIncomeService.GetAll()
                .FirstOrDefault(s => s.IsActive && s.Id == id && s.SUPPLIER_ID== suprId && s.STOCKGRP_ID== grpId && s.STOCKITEM_ID== itemId);

            stkIncomeVM.StkGrpVM = _stockGroupService.GetAll().ToList();
            stkIncomeVM.StkItemVM = _stockItemService.GetAll().ToList();
            stkIncomeVM.SupplierVM = _supplierService.GetAll().ToList();     

            return View(stkIncomeVM);
        }
        public IActionResult DeleteRec(string id)
        {
            try
            {
                string result = _stockIncomeService.DeleteRec(id);

                return Json(new
                {
                    message = result,                    
                    redirectUrl = Url.Action("List")
                });
            }
            catch (Exception)
            {               
                return Json(null);
            }
        }
        public IActionResult DeleteItem(string pCaller, string id, int line_id)
        {
            try
            {
                string result = _stockIncomeService.DeleteItem(id, line_id);

                if (pCaller == "List")//delete from List page
                {
                    TempData["Msg"] = result;                    
                    return RedirectToAction("List");
                }
                else
                {
                    var retVal = GetById(id);

                    var retSuccess = new
                    {
                        message = result,                        
                        resVal = retVal
                    };
                    return Json(retSuccess);
                }                    
            }
            catch (Exception)
            {
                if (pCaller == "List")
                {
                    TempData["Msg"] = "Error was occured when the record is deleted.";
                    return RedirectToAction("List");
                }
                else
                {
                    return Json(null);
                }
            }
        }
        [HttpPost]
        public IActionResult UpdateItem(string pCaller, StockIncomeViewModel stkIncomeVM)
        {            
            try
            {
                string result = _stockIncomeService.Update(stkIncomeVM);

                if (pCaller == "Edit")//update from Edit page
                {
                    TempData["Msg"] = result;
                    return RedirectToAction("List");
                }
                else
                {
                    var retVal = GetById(stkIncomeVM.Id);

                    var retSuccess = new
                    {
                        message = result,
                        resVal = retVal
                    };
                    return Json(retSuccess);
                }
            }
            catch (Exception)
            {
                if (pCaller == "Edit")
                {
                    TempData["Msg"] = "Error was occured when the record is updated.";
                    return RedirectToAction("List");
                }
                else
                {
                    return Json(null);
                }
            }
        }
        [HttpPost]
        public IActionResult AddItem(StockIncomeViewModel stkIncomeVM)
        {
            string result = "";
            try
            {                
                result = _stockIncomeService.Create(stkIncomeVM);

                if (result is not null)
                {
                    if (result == "AE")
                    {
                        var retSuccess = new
                        {
                            message = "Record already exist.",
                            status = false,
                            resVal = ""
                        };
                        return Json(retSuccess); 
                    }
                    else
                    {                    
                        var retVal = GetById(result);
                        var retSuccess = new
                        {
                            message = "Record has been saved successfully.",
                            status = true,
                            resVal = retVal
                        };
                        return Json(retSuccess);
                    }
                }
                else
                {
                    return Json(null);
                }
            }
            catch (Exception)
            {
                return Json(null);
            }                       
        }
        private IEnumerable<StockIncomeViewModel> GetById(string result)
        {
            return _stockIncomeService.GetById(result);
        }
    }
}
