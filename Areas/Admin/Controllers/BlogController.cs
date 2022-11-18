using ClosedXML.Excel;
using CoreDemo.Areas.Admin.Models;
using DataAccessLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogController : Controller
    {
        public IActionResult ExportStaticBlogList()
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Blog Listesi");
                worksheet.Cell(1, 1).Value = "Blog ID";
                worksheet.Cell(1, 2).Value = "Blog Adı";

                int BlogRowCount = 2; // kaçıncı satırdan başlayacağı
                foreach (var item in GetBlogList())
                {
                    worksheet.Cell(BlogRowCount, 1).Value = item.ID;
                    worksheet.Cell(BlogRowCount, 2).Value = item.BlogName;
                    BlogRowCount++;
                }

                using(var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var context = stream.ToArray();
                    return File(context, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Dosya.xlsx");
                }
            }

           // return View();

        }

        public List<BlogModel> GetBlogList()
        {
            List<BlogModel> bm = new List<BlogModel>
            {
                new BlogModel{ID = 1, BlogName="C# programlamaya giriş"},
                new BlogModel{ID = 2, BlogName="C# programlamaya giriş"},
                new BlogModel{ID = 3, BlogName="C# programlamaya giriş"},

            };
            return bm;
        }

        public IActionResult BlogListExcel()
        {
            return View();
        }



        public IActionResult ExportDinamicBlogList()
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Blog Listesi");
                worksheet.Cell(1, 1).Value = "Blog ID";
                worksheet.Cell(1, 2).Value = "Blog Adı";

                int BlogRowCount = 2; // kaçıncı satırdan başlayacağı
                foreach (var item in BlogTitleList())
                {
                    worksheet.Cell(BlogRowCount, 1).Value = item.ID;
                    worksheet.Cell(BlogRowCount, 2).Value = item.BlogName;
                    BlogRowCount++;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var context = stream.ToArray();
                    return File(context, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Dosya.xlsx");
                }
            }

            // return View();

        }
        public List<BlogModel2> BlogTitleList() // dinamik excel tablosu için
        {
            List<BlogModel2> bm = new List<BlogModel2>();
         //   var bm = new List<BlogModel>();

            using (var c = new Context())
            {
                bm = c.Blogs.Select(x => new BlogModel2
                {
                    ID = x.BlogID,
                    BlogName = x.BlogTitle
                }).ToList();
            }



            return bm;
        }


        public IActionResult BlogTitleListExcel()
        {
            return View();
        }
    }
}
