using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using POS.Domain.Models.DataModels;
using POS.Domain.Models.ViewModels;
using POS.Services;

namespace POS.Domain.Controllers
{
    public class StockItemController : Controller
    {
        private readonly IWebHostEnvironment _whe;
        private readonly IStockItemService _stockItemService;
        private readonly IStockGroupService _stockGroupService;
        public StockItemController(IWebHostEnvironment whe,IStockItemService stockItemService,IStockGroupService stockGroupService)
        {
            this._whe = whe;
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
                bool result = _stockItemService.Delete(id, _whe.WebRootPath);
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
        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile image)
        {            
            if (image == null || image.Length == 0)
                return BadRequest("No image uploaded.");

            // Create path to wwwroot/images
            var imagesPath = Path.Combine(_whe.WebRootPath, "images");
            if (!Directory.Exists(imagesPath))
                Directory.CreateDirectory(imagesPath);

            // Generate a unique file name
            var fileName = image.FileName;// Guid.NewGuid() + Path.GetExtension(image.FileName);
            var filePath = Path.Combine(imagesPath, fileName);

            //delete existing image files
            var currFile = Path.GetFileNameWithoutExtension(fileName) + ".*";
            var existFiles = Directory.GetFiles(imagesPath, currFile);

            foreach (var file in existFiles)
            {
                if (System.IO.File.Exists(file))
                {
                    System.IO.File.Delete(file);
                }
            }

            // Save the file
            using (var stream = new FileStream(filePath, FileMode.Create))
            {                
                await image.CopyToAsync(stream);
            }

            // Return the image path relative to wwwroot
            var relativePath = "/images/" + fileName;
            return Ok(new { imagePath = relativePath });
        }

        public IActionResult CheckItem(string stkGrp, string stkItem)
        {
            StockItemViewModel chkItem = _stockItemService.GetAll()
                .FirstOrDefault(s => s.StockGrp_Id == stkGrp && s.Code.ToLower() == stkItem.ToLower());

            if (chkItem is not null)
            {
                return Json("Stock Item already exist!");
            }

            return Json(null);
        }

    }
}
