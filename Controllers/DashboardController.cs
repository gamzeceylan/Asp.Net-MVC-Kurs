using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntitiyFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.Controllers
{
    public class DashboardController : Controller
    {
        BlogManager blogManager = new BlogManager(new EFBlogRepository());
        CategoryManager categoryManager = new CategoryManager(new EFCategoryRepository());

        // [AllowAnonymous] --> zaten sisteme girenin getirecek
        [AllowAnonymous]
        public IActionResult Index()
        {
            // bu kod solid'e aykırı
            Context c = new Context();

            // sisteme giriş yapan kullanıcıyı getirme
            var username = User.Identity.Name;
            var usermail = c.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();
            var writerid = c.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriterID).FirstOrDefault();



            ViewBag.v1 = c.Blogs.Count().ToString();
          //  ViewBag.v2 = c.Blogs.Where(x => x.WriterID == 1).Count();
            ViewBag.v2 = c.Blogs.Where(x => x.WriterID == writerid).Count();
            ViewBag.v3 = c.Categories.Count();
            return View();
        }
    }
}
