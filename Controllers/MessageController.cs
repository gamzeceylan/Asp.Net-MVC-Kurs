
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
    public class MessageController : Controller
    {
        Message2Manager mm = new Message2Manager(new EFMessage2Repository());
        Context c = new Context();

        [AllowAnonymous]
        public IActionResult InBox()
        {
            var username = User.Identity.Name; // aktif kullanıcının mailini çekti

            //   ViewBag.v = usermail;

            var usermail = c.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();
            var writerID = c.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriterID).FirstOrDefault();

            var values = mm.GetInBoxListByWriter(2);

            return View(values);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult MessageDetails(int id)
        {
            // solide uygulanması send

            var value = mm.TGetById(id);
            return View(value);
        }

    }
}
