using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ASP.NET_Form.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ASP.NET_Form.Controllers
{
    public class PersonsController : Controller
    {
        private readonly ICollection<Person> persons;

        public PersonsController(ICollection<Person> persons)
        {
            this.persons = persons;
        }

        public IActionResult Index()
        {
            return View(persons);
        }

        [HttpGet]
        public IActionResult Apply()
        {

            ViewBag.Expires = new Dictionary<int, string>()
            {
                { 1,"1 ay"},
                { 3,"3 ay"},
                { 6,"6 ay"},
                { 12,"12 ay"}
            };

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Apply(
        [FromForm]
        Person person)
        {
            if (ModelState.IsValid)
            {
                persons.Add(person);
                TempData["ApplyStatus"] = "Person ekleme işlemi başarıyla gerçekleşti.";

                return RedirectToAction(actionName: nameof(Index));
            }

            // Validation hatalı olduğunda form tasarımı için tekrar buradan bir request olduğu için ViewBag deki data silinecek. Hata vermemesi için tekrar oluşturulmalıdır.

            ViewBag.Expires = new Dictionary<int, string>()
            {
                { 1,"1 ay"},
                { 3,"3 ay"},
                { 6,"6 ay"},
                { 12,"12 ay"}
            };
            
            return View(person);
        }
    }
}