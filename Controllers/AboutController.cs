﻿using BusinessLayer.Concrete;
using DataAccessLayer.EntitiyFramework;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.Controllers
{
    public class AboutController : Controller
    {
        AboutManager abm = new AboutManager(new EFAboutRepository());
        public IActionResult Index()
        {
            var values = abm.GetList();
            return View(values);
        }

        public PartialViewResult SocialMediaAbout()
        {
            
            return PartialView();
        }
    }
}
