using Bank.Domain.ViewModels.Account;
using Bank.Service.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Bank.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return View();
            }
            else return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, bool rememberMe)
        {
                if (ModelState.IsValid)
                {
                    var response = await _accountService.Login(model);
                    if (response.StatusCode == Domain.Enum.StatusCode.OK)
                    {
                    if(rememberMe)
                    {
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(response.Data),
                            new AuthenticationProperties { IsPersistent = true });
                    }
                    else
                    {
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(response.Data),
                            new AuthenticationProperties { IsPersistent = false
                            ,ExpiresUtc = DateTimeOffset.UtcNow.AddSeconds(5) 
                            });
                    }
                        
                        
                        return RedirectToAction("Index", "Home");
                    }
                    ModelState.AddModelError("", response.Description);
                }
                return View(model);
        }

        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public IActionResult ChangePassword() => View();

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _accountService.ChangePassword(model);
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    return RedirectToAction("Login", "Account");
                    //return Json(new { description = response.Description });
                }
                ModelState.AddModelError("", response.Description);
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Lockscreen()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return View();
            }
            else return RedirectToAction("Index", "Home");
        }
    }
}
