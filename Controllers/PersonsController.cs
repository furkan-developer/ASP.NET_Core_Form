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
        private readonly List<Person> persons;

        public PersonsController(ICollection<Person> persons)
        {
            this.persons = persons.ToList();
        }

        public IActionResult Index()
        {
            return View(persons);
        }
    }
}