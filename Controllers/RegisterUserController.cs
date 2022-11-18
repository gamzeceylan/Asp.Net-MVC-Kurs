using CoreDemo.Models;
using EntitiyLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.Controllers
{
    [AllowAnonymous]
    public class RegisterUserController : Controller
    {
       
        private readonly UserManager<AppUser> _userManager; 
        // mimari dışına çıktık


        public RegisterUserController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
           //return View(new UserSignUpViewModel());
            return View();
        }
        

        [HttpPost]
        public async Task<IActionResult> Index(UserSignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser()
                {
                    Email = model.Mail,
                    UserName = model.UserName,
                    NameSurname = model.NameSurname
                };

                // şifre metot çağrılırken girilir
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }

            }
            return View(model);
        }

        
    }
}
