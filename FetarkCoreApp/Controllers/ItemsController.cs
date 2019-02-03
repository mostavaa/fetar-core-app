using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.ViewModels;

namespace FetarkCoreApp.Controllers
{
    public class ItemsController : Controller
    {
        public IItemService ItemService { get; }
        public IResponseService ResponseService { get; }

        public ItemsController(IItemService itemService, IResponseService responseService)
        {
            ItemService = itemService;
            ResponseService = responseService;
        }
        public IActionResult Items()
        {
            if (TempData.ContainsKey("success"))
            {
                ViewData.Add("success", TempData["success"]);
            }
            if (TempData.ContainsKey("error"))
            {
                ViewData.Add("error", TempData["error"]);
            }
            List<ItemVM> items = ItemService.GetAll();
            return View(items);
        }
        public IActionResult Create()
        {
            return View(new ItemVM());
        }
        [HttpPost]
        public IActionResult Create(ItemVM model)
        {
            if (ModelState.IsValid)
            {
                ItemService.CreateItem(model);
                ViewData.Add("success", null);
                ViewData.Add("error", null);
                if (ResponseService.Status)
                    ViewData["success"] = ResponseService.Success;
                else
                    ViewData["error"] = ResponseService.Errors;
            }
            return View(model);
        }
        public IActionResult Edit(Guid id)
        {
            ItemVM model = ItemService.GetByGuid(id);
            if (model != null)
                return View("Create", model);
            else
                return RedirectToAction("Items");
        }

        [HttpPost]
        public IActionResult Edit(ItemVM model)
        {
            if (ModelState.IsValid)
            {
                ItemService.EditItem(model);
                ViewData.Add("success", null);
                ViewData.Add("error", null);
                if (ResponseService.Status)
                    ViewData["success"] = ResponseService.Success;
                else
                    ViewData["error"] = ResponseService.Errors;
            }
            return View("Create", model);
        }

        public IActionResult Delete(Guid id)
        {
            if (id != null && id != Guid.Empty)
            {
                ItemService.Delete(id);
                if (ResponseService.Status)
                    TempData["success"] = ResponseService.Success;
                else
                    TempData["error"] = ResponseService.Errors;
            }
            else
            {
                TempData["error"] = ResponseService.Errors;
            }
            return RedirectToAction("Items");


        }

        #region APIS
        public IActionResult GetAllItems()
        {
            List<ItemVM> items = ItemService.GetAll();
            return Ok(items);
        }
        public IActionResult DeleteItem(Guid id)
        {
            ItemService.DeleteItemDetails(id);
            if (ResponseService.Status)
                return Ok(ResponseService.Success);
            return BadRequest(ResponseService.Errors);
        }
        #endregion
    }
}