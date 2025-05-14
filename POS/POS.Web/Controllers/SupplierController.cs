using Microsoft.AspNetCore.Mvc;
using POS.Domain.Models.ViewModels;
using POS.Services;

namespace POS.Web.Controllers
{
    public class SupplierController : Controller
    {
        private readonly ISupplierServices _supplierServices;

        public SupplierController(ISupplierServices supplierServices)
        {
            this._supplierServices = supplierServices;
        }
        public IActionResult List() => View(_supplierServices.GetAll().ToList());
        
        public IActionResult Entry()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Entry(SupplierViewModel supplierVM)
        {
            try
            {
                bool result = _supplierServices.Create(supplierVM);
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
        [HttpPost]
        public IActionResult Delete(string id)
        {
            try
            {
                bool result = _supplierServices.Delete(id);
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
            SupplierViewModel suprVM = _supplierServices.GetById(id);
            return View(suprVM);
        }
        [HttpPost]
        public IActionResult Update(SupplierViewModel supplierVM)
        {
            try
            {
                bool result = _supplierServices.Update(supplierVM);
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
