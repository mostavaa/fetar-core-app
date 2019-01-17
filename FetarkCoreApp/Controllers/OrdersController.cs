using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            OrderService.GetByGuid(id)
            return View();
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
    }
}