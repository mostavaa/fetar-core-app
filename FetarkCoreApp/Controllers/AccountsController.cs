using Microsoft.AspNetCore.Mvc;
using Services;
using Services.ViewModels;

namespace FetarkCoreApp.Controllers
{
    public class AccountsController : Controller
    {
        public IUserService UserService { get; }
        public IResponseService ResponseService { get; }

        public AccountsController(IUserService userService, IResponseService responseService)
        {
            UserService = userService;
            ResponseService = responseService;
        }
        [HttpPost]
        public IActionResult Register(UserVM user)
        {
            ViewData.Add("success", null);
            ViewData.Add("error", null);
            if (ModelState.IsValid)
            {
                UserService.Register(user);
                if (ResponseService.Status)
                    ViewData["success"] = ResponseService.Success;
                else
                    ViewData["error"] = ResponseService.Errors;
            }
            return View(user);
        }

        [HttpGet]
        public IActionResult Register()
        {
            ViewData.Add("success", null);
            ViewData.Add("error", null);
            return View(new UserVM());
        }
    }
}