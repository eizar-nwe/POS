using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POS.Domain.Models.ViewModels;
using POS.Services;
using System.Reflection.Metadata.Ecma335;

namespace POS.Web.Controllers
{
    public class StockGroupController : Controller
    {
        private readonly IStockGroupService _stockGroupService;

        public StockGroupController(IStockGroupService stockGroupService)
        {
            this._stockGroupService = stockGroupService;
        }
        public IActionResult List() => View(_stockGroupService.GetAll().ToList());
        public IActionResult Entry()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Entry(StockGroupViewModel stockGrpVM)
        {
            try
            {
                bool result = _stockGroupService.Create(stockGrpVM);
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
            catch (Exception e)
            {
                TempData["Msg"] = "Error was occured when the record is created.";
                TempData["IsError"] = true;
            }
            return RedirectToAction("List");
        }
        public IActionResult Edit(string id) => View(_stockGroupService.GetById(id));

        [HttpPost]
        public IActionResult Update(StockGroupViewModel stockGrpVM)
        {
            try
            {
                bool result =_stockGroupService.Update(stockGrpVM);
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
            catch (Exception e)
            {
                TempData["Msg"] = "Error was occured when the record is updated.";
                TempData["IsError"] = true;
            }
            return RedirectToAction("List");           
        }

        public IActionResult Delete(string id)
        {
            try
            {
                bool result=_stockGroupService.Delete(id);
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
            catch (Exception e)
            {
                TempData["Msg"] = "Error was occured when the record is deleted.";
                TempData["IsError"] = true;
            }
            return RedirectToAction("List"); ;
        }
    }
}
