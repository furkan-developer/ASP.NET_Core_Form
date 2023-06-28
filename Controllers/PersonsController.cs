using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASP.NET_Form.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

            ViewBag.Experience = new SelectList(new List<ExperienceSelectList>(){
                new ExperienceSelectList(){Data=0,DataText="Don't have experience"},
                new ExperienceSelectList(){Data=1,DataText="One year"},
                new ExperienceSelectList(){Data=3,DataText="Three year"},
                new ExperienceSelectList(){Data=5,DataText="Five year"},
            },nameof(ExperienceSelectList.Data),nameof(ExperienceSelectList.DataText),0);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Apply(
        [FromForm]
        Person person)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // throw new Exception("Person eklenir iken server da hata oluştu");

                    // Create path for store image
                    string path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","images");

                    if(!Directory.Exists(path))
                        Directory.CreateDirectory(path);

                    // Create unique file name
                    StringBuilder fileNameBuilder = new StringBuilder(Guid.NewGuid().ToString());
                    fileNameBuilder.Append(Path.GetExtension(person.File.FileName));

                    string fileNameWithExtension = fileNameBuilder.ToString();
                    path = Path.Combine(path,fileNameWithExtension);

                    // Store to server
                    using(var stream = new FileStream(path:path,mode:FileMode.Create))
                    {
                        await person.File.CopyToAsync(stream);
                    }

                    // Add path to person object
                    person.ImagePath = String.Concat("/images/",fileNameWithExtension);

                    persons.Add(person);
                    TempData["ApplyStatus"] = "Person ekleme işlemi başarıyla gerçekleşti.";

                    return RedirectToAction(actionName: nameof(Index));
                }
                catch (System.Exception ex)
                {
                    ModelState.AddModelError(string.Empty,ex.Message);
                }
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