using BusinessLayer.Concrete;
using DataAccessLayer.EntitiyFramework;
using Microsoft.AspNetCore.Mvc;
using System;
using X.PagedList; // listeleme için
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntitiyLayer.Concrete;
using BusinessLayer.ValidationRules;
using FluentValidation.Results;

namespace CoreDemo.Areas.Admin.Controllers
{
    [Area("Admin")] // bu controllerdaki action'ların Area'da admin den geldiğini bildirmiş olduk

    public class CategoryController : Controller
    {
        CategoryManager cm = new CategoryManager(new EFCategoryRepository());
        public IActionResult Index(int page = 1)
        {
            // kategori listeleme
            // var values = cm.GetList();
            var values = cm.GetList().ToPagedList(page, 3); // kaçıncı sayfadan kaçıncı sayfaya kadar sayfalama işlemi yapalım
            return View(values);
        }
        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();

        }

        [HttpPost]
        public IActionResult AddCategory(Category p)
        {

            CategoryValidator cv = new CategoryValidator();
            ValidationResult results = cv.Validate(p);

            if (results.IsValid)
            {
                p.CategoryStatus = true;
                cm.TAdd(p);
                return RedirectToAction("Index", "Category");
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

        public IActionResult CategoryDelete(int id)
        {
            var value = cm.TGetById(id);
            cm.TDelete(value);
            return View(value);
        }
    }
}
