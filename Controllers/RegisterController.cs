using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntitiyFramework;
using EntitiyLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.Controllers
{
    [AllowAnonymous]
    public class RegisterController : Controller
    {
        WriterManager wm = new WriterManager(new EFWriterRepository());

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Writer p)
        {
            /*
            p.WriterStatus = true;
            p.WriterAbout = "Deneme Test";
            wm.WriterAdd(p);
            return RedirectToAction("Index", "Blog");
            */
            WriterValidator wv = new WriterValidator();
            ValidationResult result = wv.Validate(p);
            if (result.IsValid)
            {
               

                    p.WriterStatus = true;
                    p.WriterAbout = "Deneme Testi";
                    wm.TAdd(p);
                    return RedirectToAction("Index", "Blog");


            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }
    }
}
