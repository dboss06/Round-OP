using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Round_OP.Models;
using Round_OP.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Round_OP.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["ToastType"] = "error";
                ViewData["ToastMessage"] = "Please correct the errors and try again.";

                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(
                model.Email,
                model.Password,
                model.RememberMe,
                lockoutOnFailure: true);

            if (result.Succeeded)
            {
                TempData["ToastType"] = "success";
                TempData["ToastMessage"] = "Welcome back.";

                return RedirectToAction("Index", "Admin");
            }

            if (result.IsLockedOut)
            {
                ViewData["ToastType"] = "error";
                ViewData["ToastMessage"] =
                    "This account has been temporarily locked. Please try again later.";
            }
            else if (result.IsNotAllowed)
            {
                ViewData["ToastType"] = "error";
                ViewData["ToastMessage"] =
                    "This account is not currently allowed to sign in.";
            }
            else
            {
                ViewData["ToastType"] = "error";
                ViewData["ToastMessage"] =
                    "Invalid email or password.";
            }

            return View(model);
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            ViewData["ToastType"] = "success";
            ViewData["ToastMessage"] = "You have been logged out.";
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
