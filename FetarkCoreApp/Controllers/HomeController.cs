using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FetarkCoreApp.Models;
using Services;
using Services.ViewModels;
using System.Collections.Generic;

namespace FetarkCoreApp.Controllers
{
    public class HomeController : Controller
    {
        public IOrderService OrderService { get; }

        public HomeController(IOrderService orderService)
        {
            OrderService = orderService;
        }
        public IActionResult Index(int page = 0)
        {
            int count = OrderService.Count();
            int pagesCount = count / Constants.PageSize;

            pagesCount = count % Constants.PageSize > 0 ? pagesCount + 1 : pagesCount;

            if (page < 0)
                page = 0;
            else if (page >= pagesCount)
                page = pagesCount-1;
            ViewData.Add("page", page);
            List<OrderVM> orders =  OrderService.GetAll(page);
            ViewData.Add("count", count);

            var canCreateOrder = OrderService.CanAddOrder();
            ViewData.Add("canCreateOrder", canCreateOrder);
            return View(orders);
        }
        public IActionResult Notifications()
        {
            return View();
        }
    
    
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #region APIS
        public IActionResult SendPushNotification(string title, string body)
        {
            string result = FcmService.SendNotificationFromFirebaseCloud(title, body);
            return Ok();
        }

        #endregion
    }
}
