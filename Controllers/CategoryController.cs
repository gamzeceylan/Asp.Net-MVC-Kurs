﻿using BusinessLayer.Concrete;
using DataAccessLayer.EntitiyFramework;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.Controllers
{
    public class CategoryController : Controller
    {
        CategoryManager cm = new CategoryManager(new EFCategoryRepository());
        public IActionResult Index()
        {
            var values = cm.GetList();
            return View(values);
        }
    }
}
