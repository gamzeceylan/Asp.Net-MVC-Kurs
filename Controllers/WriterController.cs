using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using CoreDemo.Models;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntitiyFramework;
using EntitiyLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.Controllers
{
    // [Authorize]
    public class WriterController : Controller
    {
        WriterManager wm = new WriterManager(new EFWriterRepository());


        private readonly UserManager<AppUser> _userManager;
        UserManager userManager = new UserManager(new EFUserRepository());

        public WriterController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [Authorize]
        // [AllowAnonymous]
        public IActionResult Index()
        {
            var usermail = User.Identity.Name; // aktif kullanıcının mailini çekti

            ViewBag.v = usermail;
            Context c = new Context();
            var writerName = c.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriterName).FirstOrDefault();
            ViewBag.v2 = writerName;
            return View();
        }

        public IActionResult WriterProfile()
        {
            return View();
        }

        //  [Authorize]
        public IActionResult WriterMail()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Test()
        {
            return View();
        }
        [AllowAnonymous]
        public PartialViewResult WriterNavbarPartial()
        {
            return PartialView();
        }

        [AllowAnonymous]
        public PartialViewResult WriterFooterPartial()
        {
            return PartialView();
        }
        //[AllowAnonymous]
        //[HttpGet]
        //public IActionResult WriterEditProfile()
        //{
        //    var writerValues = wm.TGetById(1);
        //    return View(writerValues);
        //}

        /*    
            [HttpGet]
            public IActionResult WriterEditProfile()
            {
                Context c = new Context();
                var username = User.Identity.Name;
                var usermail = c.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();
                var id = c.Users.Where(x => x.Email == usermail).Select(y => y.Id).FirstOrDefault();
                var values = userManager.TGetById(id);
                return View(values);

            }
        */

        [HttpGet]
        public async Task<IActionResult> WriterEditProfile()
        {
            var username = User.Identity.Name; // aktif kullanıcının mailini çekti

            //   ViewBag.v = usermail;

            var values = await _userManager.FindByIdAsync(User.Identity.Name);
            UserUpdateViewModel model = new UserUpdateViewModel();
            model.username = values.UserName;
            model.namesurname = values.NameSurname;
            model.imageurl = values.ImageUrl;
            //     model.phone = values.PhoneNumber;
            model.mail = values.Email;
            return View(model);

        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> WriterEditProfile(string PasswordAgain, UserUpdateViewModel model)
        {
            /*
            //Güncelleme için validatior dan geçmen gerekir
            WriterValidator wl = new WriterValidator();
            ValidationResult results = wl.Validate(p);
            if (results.IsValid)
            {
                wm.TUpdate(p);
                return RedirectToAction("Index", "Dashboard");

            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
            */

            /*
                        // sisteme giriş yapan, kayıt olan kullanıcı bilgiai
                        Context c = new Context();
                        var usermail = User.Identity.Name; // aktif kullanıcının mailini çekti

                        var writerID = c.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriterID).FirstOrDefault();
                        var values = wm.GetWriterById(writerID); // sisteme kayıt olan yazar idsi

                        return View(values);
            */


            //Context c = new Context();
            //var username = User.Identity.Name; // aktif kullanıcının mailini çekti
            //var usermail = c.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault(); // aktif kullanıcının mailini çekti

            //var writerID = c.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriterID).FirstOrDefault();
            //var values = wm.TGetById(writerID); // sisteme kayıt olan yazar idsi

            //return View(values);
            UserManager userManager = new UserManager(new EFUserRepository());


            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            //     UserUpdateViewModel model = new UserUpdateViewModel();
            ViewBag.userName = values;
            values.NameSurname = model.namesurname;
            values.ImageUrl = model.imageurl;
            values.Email = model.mail;
            values.PhoneNumber = model.phone;
            if (model.password != null)
            {
                values.PasswordHash = _userManager.PasswordHasher.HashPassword(values, model.password);

            }
            var result = await _userManager.UpdateAsync(values);

            return RedirectToAction("Index", "Dashboard");

        }




        //  [AllowAnonymous]
        public IActionResult WriterAdd(AddProfileImage p)
        {
            Writer w = new Writer();
            if (p.WriterImage != null)
            {
                var extension = Path.GetExtension(p.WriterImage.FileName);
                var newimagename = Guid.NewGuid() + extension;
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/WriterImageFiles", newimagename);
                var stream = new FileStream(location, FileMode.Create);
                p.WriterImage.CopyTo(stream);
                w.WriterImage = newimagename;
            }
            w.WriterMail = p.WriterMail;
            w.WriterName = p.WriterName;
            w.WriterPassword = p.WriterPassword;
            w.WriterStatus = true;
            w.WriterAbout = p.WriterAbout;
            wm.TAdd(w);
            return View();


        }
    }
}
