using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Obooking.Models
{
    public class Category
    {
        [Key]
        [Display(Name = "ID")]
        public int CategoryID { get; set; }

        [Required]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }
    }
}