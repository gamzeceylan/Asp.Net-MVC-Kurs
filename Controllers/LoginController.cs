using CoreDemo.Models;
using DataAccessLayer.Concrete;
using EntitiyLayer.Concrete;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CoreDemo.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
   // SignInManager, sisteme identity üzerinden authentike olmak için
   // user manager sisteme identity üzerinden kayıt olmak için 


        private readonly SignInManager<AppUser> _signInManager;

        public LoginController(SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        
        [HttpPost]
        public async Task<IActionResult> Index(UserSignInViewModel model)
        {
            if (ModelState.IsValid)
            {
                // persistant: çerezlerde hatırlasın mı   locoutOnFailure: belli sayıda yanlış girerse banlanacak
                var result = await _signInManager.PasswordSignInAsync(model.username, model.password, false, true);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    //TempData["ErrorMessage"] = "Kullanıcı adınız veya parolanız hatalı lütfen tekrar deneyiniz.";
                    //return View(model);
                    return RedirectToAction("Index", "Login");
                }
            }
            //return View(model);
            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Login");
        }



        //[AllowAnonymous]
        //[HttpPost]
        //public IActionResult Index(Writer p)
        //{
        //    Context c = new Context();
        //    var datavalue = c.Writers.FirstOrDefault(x => x.WriterMail == p.WriterMail && x.WriterPassword == p.WriterPassword);

        //    if(datavalue != null)
        //    {
        //        HttpContext.Session.SetString("username", p.WriterMail);
        //        return RedirectToAction("Index", "Writer");
        //    }
        //    else
        //    {
        //        return View();
        //    }

        //}

        //[AllowAnonymous]
        //[HttpPost]
        //public async Task<IActionResult> Index(Writer p) // sisteme giriş yapınca
        //{
        //    Context c = new Context();
        //    var datavalue = c.Writers.FirstOrDefault(x => x.WriterMail == p.WriterMail && x.WriterPassword == p.WriterPassword);
        //    if (datavalue != null)
        //    {
        //        var claims = new List<Claim>
        //        {
        //            new Claim(ClaimTypes.Name, p.WriterMail)
        //        };
        //        var useridentity = new ClaimsIdentity(claims, "a");
        //        ClaimsPrincipal principal = new ClaimsPrincipal(useridentity);

        //        await HttpContext.SignInAsync(principal);
        //        return RedirectToAction("Index", "Dashboard");
        //    }
        //    else
        //    {
        //        return View();
        //    }
        //}



    }
}
