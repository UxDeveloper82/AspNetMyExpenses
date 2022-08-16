using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyExpenses.Models
{
    public class MyIncomePlusExpenses
    {
        [Key]
        public int id { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        [Required]
        [Display(Name = "Date")]
        public DateTime Date { get; set; }


        [Required]
        [Range(1, 100000)]
        [DisplayFormat(DataFormatString ="{0:C}")]
        [Display(Name ="Price")]
        public double Price { get; set; }
    }
}
