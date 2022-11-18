using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntitiyFramework;
using EntitiyLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.ViewComponents.Writer
{
    public class WriterAboutOnDashboard : ViewComponent
    {
        WriterManager wm = new WriterManager(new EFWriterRepository());
        Context c = new Context();

        private readonly UserManager<AppUser> _userManager;

        public WriterAboutOnDashboard(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        
        public IViewComponentResult Invoke()
        {
            // solidi ezdik. session baska bir yerde tanımlanmalı
            // sisteme authonntacide olan kullanıcı bilgileri
            var username = User.Identity.Name; // aktif kullanıcının mailini çekti

            ViewBag.v = username;
            var usermail = c.Users.Where(x => x.UserName == username).Select(y=> y.Email).FirstOrDefault();
       
            var writerID = c.Writers.Where(x => x.WriterMail== usermail).Select(y => y.WriterID).FirstOrDefault();
            //  ViewBag.v2 = ggamzeeceylan@gmail.com;
            var values = wm.GetWriterById(writerID); // sisteme kayıt olan yazar idsi

            return View(values);

            

        }
    
        /*
        public async Task<IViewComponentResult> InvokeAsync()
        {
           var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var usermail = User.Identity.Name; // aktif kullanıcının mailini çekti
            //ViewBag.v1 = usermail; --> gelmedi
            ViewBag.v1 = user;
            // ViewBag.v = usermail;

            var writerID = c.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriterID).FirstOrDefault();
            //  ViewBag.v2 = ggamzeeceylan@gmail.com;
            var values = wm.GetWriterById(writerID); // sisteme kayıt olan yazar idsi

            return View(user);
        }
        */
    }
}
