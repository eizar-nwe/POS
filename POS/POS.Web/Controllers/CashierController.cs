using Microsoft.AspNetCore.Mvc;
using POS.Domain.Models.ViewModels;
using POS.Services;

namespace POS.Web.Controllers
{
    public class CashierController : Controller
    {
        private readonly ICashierService _CashierServices;

        public CashierController(ICashierService CashierServices)
        {
            this._CashierServices = CashierServices;
        }
        public IActionResult List() => View(_CashierServices.GetAll().ToList());
        
        public IActionResult Entry()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Entry(CashierViewModel CashierVM)
        {
            try
            {
                bool result = _CashierServices.Create(CashierVM);
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
        
        public IActionResult Delete(string id)
        {
            try
            {
                bool result = _CashierServices.Delete(id);
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
        public IActionResult Edit(string id)
        {
            CashierViewModel suprVM = _CashierServices.GetById(id);
            return View(suprVM);
        }
        [HttpPost]
        public IActionResult Update(CashierViewModel CashierVM)
        {
            try
            {
                bool result = _CashierServices.Update(CashierVM);
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
    }
}
