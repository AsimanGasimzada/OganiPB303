using Microsoft.AspNetCore.Mvc;
using Ogani.BLL.Services.Contracts;
using Ogani.BLL.ViewModels.AppUserViewModels;

namespace Ogani.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _service;

        public AccountController(IAccountService service)
        {
            _service = service;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            var result = await _service.RegisterAsync(vm, ModelState);

            if (result == false)
                return View(vm);

            return Json("Ok");
        }

        public IActionResult Login()
        {
           
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            var result = await _service.LoginAsync(vm, ModelState);

            if (result == false) return View(vm);

            return Json("ok");
        }


        public async Task<IActionResult> LogOut()
        {
            var result=await _service.SignOutAsync();


            if (result is false)
                return BadRequest();

            return Json("OK");

        }
    }
}
