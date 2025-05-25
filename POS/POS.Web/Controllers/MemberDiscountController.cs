using Microsoft.AspNetCore.Mvc;
using POS.Domain.Models.ViewModels;
using POS.Services;
using System;

namespace POS.Web.Controllers
{
    public class MemberDiscountController : Controller
    {        
        private readonly IMemberDiscountService _memDiscService;
        private readonly IStockGroupService _stkGrpService;
        private readonly IStockItemService _stkItmService;
        private readonly IMemberService _memService;

        public MemberDiscountController(IMemberDiscountService memDiscService,IStockGroupService stkGrpService,IStockItemService stkItmService,IMemberService memService)
        {            
            this._memDiscService = memDiscService;
            this._stkGrpService = stkGrpService;
            this._stkItmService = stkItmService;
            this._memService = memService;
        }
        public IActionResult List() => View(_memDiscService.GetAll().ToList());
        public IActionResult Entry()
        {
            MemberDiscountViewModel memDiscVM = new MemberDiscountViewModel
            {
                MemberVM=_memService.GetAll().ToList(),
                StkGrpVM=_stkGrpService.GetAll().ToList(),
                StkItemVM=_stkItmService.GetAll().ToList(),
            };
            return View(memDiscVM);
        }
        public IActionResult Edit(string id, int line_id)
        {
            MemberDiscountViewModel memDiscVM = _memDiscService.GetAll()
                .FirstOrDefault(s => s.IsActive && s.Id == id && s.LINE_ID == line_id);

            memDiscVM.MemberVM = _memService.GetAll().ToList();
            memDiscVM.StkGrpVM = _stkGrpService.GetAll().ToList();
            memDiscVM.StkItemVM = _stkItmService.GetAll().ToList();

            return View(memDiscVM);
        }
        [HttpPost]
        public IActionResult AddItem(MemberDiscountViewModel memDiscVM)
        {
            string result = "";
            try
            {
                result = _memDiscService.Create(memDiscVM);

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
        [HttpPost]
        public IActionResult UpdateItem(string pCaller, MemberDiscountViewModel memDiscVM)
        {
            try
            {
                string result = _memDiscService.Update(memDiscVM);

                if (pCaller == "Edit")//update from Edit page
                {
                    TempData["Msg"] = result;
                    return RedirectToAction("List");
                }
                else
                {
                    var retVal = GetById(memDiscVM.Id);

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
        public IActionResult DeleteRec(string id)
        {
            try
            {
                string result = _memDiscService.DeleteRec(id);

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
                string result = _memDiscService.DeleteItem(id, line_id);

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
        private IEnumerable<MemberDiscountViewModel> GetById(string result)
        {
            return _memDiscService.GetById(result);
        }
    }
}
