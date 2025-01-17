﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CoreDemo.Controllers
{
    // çalışması için api projesinin ayakta olmaısı lazım
    // ve getasync içinde vereceğimiz local hosu karşılayacak olan değere ihtiyacımız var
    public class EmployeeTestController : Controller
    {
        // api olduğu iiçin asenktonik
        public async Task<IActionResult> Index()
        {
            // local hosta bulunan adreste bir istekte bulnup orada buluan
            // değerleti listelemeye çaloşıyoruz
            var httpClient = new HttpClient();
            var responseMessage = await httpClient.GetAsync("https://localhost:44368/api/Default"); // istek yapacağımız adres
            var jsonString = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<Class1>>(jsonString); // empoloyee'i yakalayacak olan class
            
            return View(values);
        }

        public IActionResult AddEmployee()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddEmployee(Class1 p)
        {
            var httpClient = new HttpClient();
            var jsonEmployee = JsonConvert.SerializeObject(p);
            StringContent content = new StringContent(jsonEmployee, Encoding.UTF8, "application/json");
            var responseMessage = await httpClient.PostAsync("https://localhost:44368/api/Default",content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(p);
        }
        [HttpGet]
        public async Task<IActionResult> EditEmployee(int id)
        {
            var httpClient = new HttpClient();
            var responseMessage = await httpClient.GetAsync("https://localhost:44368/api/Default/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonEmployee = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<Class1>(jsonEmployee);
                return View(values);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> EditEmployee(Class1 p)
        {
            var httpClient = new HttpClient();
            var jsonEmployee = JsonConvert.SerializeObject(p);
            var content = new StringContent(jsonEmployee, Encoding.UTF8,"application/json");
            var responseMessage = await httpClient.PutAsync("https://localhost:44368/api/Default", content);
            /*
             response mesajda apiye bağlı olarak gerçekleşecek olan işlem yazılım
             
             
             */
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(p);

        }
        
        public async Task<IActionResult> DeleteEmpoyee(int id)
        {
            var httpClient = new HttpClient();
            var responseMessage = await httpClient.DeleteAsync("https://localhost:44368/api/Default/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
          

        }
    }
    public class Class1
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
 
}
