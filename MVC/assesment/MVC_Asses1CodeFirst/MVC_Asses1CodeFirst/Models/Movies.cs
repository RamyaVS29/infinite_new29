using System;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Asses1CodeFirst.Models.Models
{
    public class Movies
    {
        [Key]
        public int id { get; set; }

        [Required]
        public string Moviename { get; set; }

        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        public DateTime DateofRelease { get; set; }
    }
}
