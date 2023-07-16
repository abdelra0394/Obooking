using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Obooking.Models
{
    public class Review
    {
        [Key]
        [Display(Name = "ID")]
        public int Review_ID { get; set; }

        [Required]
        [Display(Name = "Review")]
        public string Review_content { get; set; }

        public int userid { get; set; }
        public string Book_Name { get; set; }
        //nav properties
        public Book Book { get; set; }
        public User user { get; set; }

    }
}