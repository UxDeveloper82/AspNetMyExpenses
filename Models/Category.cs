using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyExpenses.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(120)]
        [Display(Name="Category Name")]
        public string CategoryName { get; set; }

        [Required]
        [MaxLength(2)]
        [Display(Name ="Type")]
        public string Type { get; set; }

        [Required]
        public bool state { get; set; }
    }
}
