using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntitiyFramework;
using EntitiyLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.Controllers
{
    public class BlogController : Controller
    {
        BlogManager bm = new BlogManager(new EFBlogRepository());
        CategoryManager cm = new CategoryManager(new EFCategoryRepository());
        Context c = new Context();

        [AllowAnonymous]

        public IActionResult Index()
        {
            var values = bm.GetListWithCategory();
            return View(values);
        }

        [AllowAnonymous]

        public IActionResult BlogReadAll(int id)
        {
            ViewBag.i = id;
            var values = bm.GetBlogById(id);
            return View(values);
        }

        public IActionResult BlogListByWriter()
        {
            // solidi ezdik. session baska bir yerde tanımlanmalı
            // sisteme authonntacide olan kullanıcı bilgileri
            var username = User.Identity.Name; // aktif kullanıcının mailini çekti

            //   ViewBag.v = usermail;

            var usermail = c.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();
            var writerID = c.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriterID).FirstOrDefault();
            //  ViewBag.v2 = ggamzeeceylan@gmail.com;
            //         var values = wm.GetWriterById(writerID); // sisteme kayıt olan yazar idsi


            var values = bm.GetListWithCategoryByWriterBm(writerID);
            return View(values);
        }

        [HttpGet]
        public IActionResult BlogAdd()
        {
        // solide uygulanması sende
            List<SelectListItem> categoryValues = (from x in cm.GetList()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.CategoryID.ToString()
                                                   }).ToList();

            ViewBag.cv = categoryValues;
            return View();
        }

        [HttpPost]
        public IActionResult BlogAdd(Blog p)
        {
            // sisteme giriş yapan yazarı çekme
            var username = User.Identity.Name; 

            var usermail = c.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();
            var writerID = c.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriterID).FirstOrDefault();

            BlogValidator bv = new BlogValidator();
            ValidationResult results = bv.Validate(p);

            if (results.IsValid)
            {
                p.BlogStatus = true;
                p.BLogCreateDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                p.WriterID = writerID;
                bm.BlogAdd(p);
                return RedirectToAction("BlogListByWriter", "Blog");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }

        public IActionResult DeleteBlog(int id)
        {
            var blogvalue = bm.TGetById(id);
            bm.BlogDelete(blogvalue);

            return RedirectToAction("BlogListByWriter"); 
        }

        [HttpGet]
        public IActionResult EditBlog(int id)
        {
            // solide uygulanması sende
            List<SelectListItem> categoryValues = (from x in cm.GetList()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.CategoryID.ToString()
                                                   }).ToList();

            ViewBag.cv = categoryValues;


            var blogvalue = bm.TGetById(id);
            return View(blogvalue);
        }
        [HttpPost]
        public IActionResult EditBlog(Blog p)
        {
            // sisteme giriş yapan yazarı çekme
            var username = User.Identity.Name; // aktif kullanıcının mailini çekti

            //   ViewBag.v = usermail;

            var usermail = c.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();
            var writerID = c.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriterID).FirstOrDefault();



            // tarihin aynı kalması için nasıl tutarız?
            p.WriterID = writerID;
            p.BLogCreateDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            p.BlogStatus = true;
            bm.BlogUpdate(p);
            return RedirectToAction("BlogListByWriter");
        }

    }
}
