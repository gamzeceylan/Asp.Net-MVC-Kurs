using CoreDemo.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.ViewComponents
{
    public class ComponentList : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var values = new List<UserComment>
            {
                new UserComment
                {
                     ID=2,
                      Name="Gamze"
                },
                 new UserComment
                {
                     ID=3,
                      Name="Metin"
                },
                  new UserComment
                {
                     ID=6,
                      Name="Büşra"
                }

            };
            return View(values);
        }
    }
}
