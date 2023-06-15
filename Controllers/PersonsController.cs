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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Apply(
        [Bind(nameof(Person.FirstName),nameof(Person.LastName),nameof(Person.HasProject))]
        [FromForm]
        Person person)
        {
            if (ModelState.IsValid)
            {
                persons.Add(person);
                TempData["ApplyStatus"] = "Person ekleme işlemi başarıyla gerçekleşti.";

                return RedirectToAction(actionName: nameof(Index));
            }

            return View(person);
        }
    }
}