using Microsoft.AspNetCore.Mvc;
using POS.Domain.Models.ViewModels;
using POS.Services;

namespace POS.Domain.Controllers
{
    public class StockItemController : Controller
    {
        private readonly IStockItemService _stockItemService;
        private readonly IStockGroupService _stockGroupService;

        public StockItemController(IStockItemService stockItemService,IStockGroupService stockGroupService)
        {
            this._stockItemService = stockItemService;
            this._stockGroupService = stockGroupService;
        }
        public IActionResult List() => View(_stockItemService.GetAll().ToList());
        public IActionResult Entry()
        {
            StockItemViewModel stockItemVM = new StockItemViewModel
            {
                StockGrpVMs = _stockGroupService.GetAll().ToList()
            };
            return View(stockItemVM);
        }
        [HttpPost]
        public IActionResult Entry(StockItemViewModel stockItemVM)
        {
            try
            {
                bool result = _stockItemService.Create(stockItemVM);
                if (result)
                {
                    TempData["Msg"] = "Data has been saved successfully.";
                    TempData["IsError"] = false;
                }
                else
                {
                    TempData["Msg"] = "Error was occured when the record is created.";
                    TempData["IsError"] = true;
                }
            }
            catch (Exception)
            {
                TempData["Msg"] = "Error was occured when the record is created.";
                TempData["IsError"] = true;
            }
            return RedirectToAction("List");
        }
        public IActionResult Edit(string id)
        {
            StockItemViewModel stockItemVM = _stockItemService.GetByID(id);

            stockItemVM.StockGrpVMs = _stockGroupService.GetAll().ToList();
            
            return View(stockItemVM);
        }

        [HttpPost]
        public IActionResult Update(StockItemViewModel stockItemVM)
        {
            try
            {
                bool result = _stockItemService.Update(stockItemVM);
                if (result)
                {
                    TempData["Msg"] = "Data has been updated successfully.";
                    TempData["IsError"] = false;
                }
                else
                {
                    TempData["Msg"] = "Error was occured when the record is updated.";
                    TempData["IsError"] = true;
                }
            }
            catch (Exception)
            {
                TempData["Msg"] = "Error was occured when the record is updated.";
                TempData["IsError"] = true;
            }
            return RedirectToAction("List");
        }
        public IActionResult Delete (string id)
        {
            try
            {
                bool result = _stockItemService.Delete(id);
                if (result)
                {
                    TempData["Msg"] = "Data has been deleted successfully.";
                    TempData["IsError"] = false;
                }
                else
                {
                    TempData["Msg"] = "Error was occured when the record is deleted.";
                    TempData["IsError"] = true;
                }
            }
            catch (Exception)
            {
                TempData["Msg"] = "Error was occured when the record is deleted.";
                TempData["IsError"] = true;
            }
            return RedirectToAction("List");
        }
    }
}
