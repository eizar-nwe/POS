using Microsoft.AspNetCore.Mvc;
using POS.Domain.Models.ViewModels;
using POS.Services;

namespace POS.Web.Controllers
{
    public class MemberController : Controller
    {
        private readonly IMemberService _MemberServices;

        public MemberController(IMemberService MemberServices)
        {
            this._MemberServices = MemberServices;
        }
        public IActionResult List() => View(_MemberServices.GetAll().ToList());
        
        public IActionResult Entry()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Entry(MemberViewModel MemberVM)
        {
            try
            {
                bool result = _MemberServices.Create(MemberVM);
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
                bool result = _MemberServices.Delete(id);
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
            MemberViewModel suprVM = _MemberServices.GetById(id);
            return View(suprVM);
        }
        [HttpPost]
        public IActionResult Update(MemberViewModel MemberVM)
        {
            try
            {
                bool result = _MemberServices.Update(MemberVM);
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
