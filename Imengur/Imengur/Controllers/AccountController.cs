using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Imengur.Models;

namespace Imengur.Controllers
{
   
    public class AccountController : Controller
    {
        private UserManager<BetterUser> _userManager;
        private SignInManager<BetterUser> _signInManager;

        public AccountController(UserManager<BetterUser> userManager, SignInManager<BetterUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            return View(new Login
            {
                ReturnUrl = returnUrl
            });
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login login)
        {
            if (ModelState.IsValid)
            {
                BetterUser user = await _userManager.FindByNameAsync(login.Name);
                if (user != null)
                {
                    await _signInManager.SignOutAsync();
                    if ((await _signInManager.PasswordSignInAsync(user, login.Password, false, false)).Succeeded)
                    {
                        return Redirect(login?.ReturnUrl ?? "/");
                    }
                }
            }
            ModelState.AddModelError("", "Nieprawidłowa nazwa użytkownika lub hasło");
            return View(login);
        }

        public async Task<RedirectResult> Logout(string returnUrl = "/")
        {
            await _signInManager.SignOutAsync();
            return Redirect(returnUrl);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(Register register)
        {
            if (ModelState.IsValid)
            {
                var user = new BetterUser
                {
                    UserName = register.Name,
                    Email = register.Email,
                };

                var result = await _userManager.CreateAsync(user, register.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                ModelState.AddModelError("", "Błąd rejestracji");
            }
            return View(register);
        }
    }
}
