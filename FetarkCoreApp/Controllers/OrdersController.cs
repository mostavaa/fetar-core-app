using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.ViewModels;

namespace FetarkCoreApp.Controllers
{
    public class OrdersController : Controller
    {
        public IOrderService OrderService { get; }
        public IResponseService ResponseService { get; }

        public OrdersController(IOrderService orderService, IResponseService responseService)
        {
            OrderService = orderService;
            ResponseService = responseService;
        }
        public IActionResult Order(Guid id)
        {
            var Order = OrderService.FullGetByGuid(id);
             OrderService.GroupOrder(Order);
            ViewBag.groupData = ResponseService.Data;
            return View(Order);
        }


        public IActionResult CreateOrder()
        {

            var canCreateOrder = OrderService.CanAddOrder();
            if (canCreateOrder)
            {
                var model = new OrderVM();
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public IActionResult CreateOrder(OrderVM model)
        {
            OrderService.CreateOrder(model);
            ViewData.Add("success", null);
            ViewData.Add("error", null);
            if (ResponseService.Status)
                ViewData["success"] = ResponseService.Success;
            else
                ViewData["error"] = ResponseService.Errors;
            return View(model);
        }

        #region APIS
        public IActionResult OrderTheOrder(int id, bool isChecked)
        {
            OrderService.OrderTheOrder(id, isChecked);
            if (ResponseService.Status)
                return Ok(ResponseService.Success);
            else
                return BadRequest(ResponseService.Errors);
        }
        public IActionResult ChangePayment(int id, bool isChecked)
        {
            OrderService.ChangePayment(id, isChecked);
            if (ResponseService.Status)
                return Ok(ResponseService.Success);
            else
                return BadRequest(ResponseService.Errors);
        }
        public IActionResult SaveNotes(int id, string notes)
        {
            OrderService.SaveNotes(id, notes);
            if (ResponseService.Status)
                return Ok(ResponseService.Success);
            else
                return BadRequest(ResponseService.Errors);
        }
        [Authorize]
        public IActionResult RequestOrderItem(Guid userId, int amount, Guid itemId , string notes)
        {
            OrderService.RequestOrderItem(userId, amount, itemId, notes);
            if (ResponseService.Status)
            {
                return Ok(ResponseService.Success);
            }
            else
            {
                return BadRequest(ResponseService.Errors);
            }
        }
        [Authorize]
        public IActionResult GetMyOrders(Guid userId)
        {
            OrderService.GetMyOrders(userId);
            if (ResponseService.Status)
            {
                return Ok(ResponseService.Data);
            }
            else
            {
                return BadRequest(ResponseService.Errors);
            }
        }
        #endregion
    }
}