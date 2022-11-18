using CoreDemo.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class WriterController : Controller
    {
     
        public IActionResult Index()
        {
            return View();
        }

        // aciton da açılıyor
        public IActionResult WriterList()
        {
            var jsonwriters = JsonConvert.SerializeObject(writers);
            return Json(jsonwriters);
        }

        public IActionResult GetWriterByID(int writerid)
        {
            var findwriter = writers.FirstOrDefault(x => x.Id == writerid);
            var jsonWriters = JsonConvert.SerializeObject(findwriter); // serialize, deserialize ?
            return Json(jsonWriters); // ajax json istiyor
        }

        [HttpPost]
        public IActionResult AddWriter(WriterClass w)
        {
            writers.Add(w);
            var jsonWriters = JsonConvert.SerializeObject(w);
            return Json(jsonWriters); 
        }

        public IActionResult DeleteWriter(int id)
        {
            var writer = writers.FirstOrDefault(x => x.Id == id);
            writers.Remove(writer);
            return Json(writer);
        }

        public IActionResult UpdateWriter(WriterClass w)
        {
            var writer = writers.FirstOrDefault(x => x.Id == w.Id);
            writer.Name = w.Name;
            var jsonWriter = JsonConvert.SerializeObject(w);
            return Json(jsonWriter);
        }

        public static List<WriterClass> writers = new List<WriterClass>
        {
            new WriterClass
            {
                Id = 1,
                Name = "Samet"
            },
            new WriterClass
            {
                Id = 2,
                Name = "Şevval"
            },
            new WriterClass
            {
                Id = 3,
                Name = "Nurana"
            },
            new WriterClass
            {
                Id = 4,
                Name = "Mete Serkan"
            }

        };
    }
}
