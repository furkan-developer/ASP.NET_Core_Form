using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ASP.NET_Form.Models
{
    [Bind(nameof(Person.FirstName),nameof(Person.LastName),nameof(Person.HasProject),nameof(Person.Expire),nameof(Person.Comment),nameof(Person.Birthday),nameof(Person.Experience),nameof(Person.File))]
    public class Person
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public bool HasProject { get; set; }

        [Required]
        public int? Expire { get; set; }

        [MaxLength(20)]
        public string Comment { get; set; }

        [Required]
        public DateTime Birthday { get; set; }

        [Required]
        public int Experience { get; set; }

        public string? ImagePath { get; set; }

        public IFormFile File { get; set; }
    }
}