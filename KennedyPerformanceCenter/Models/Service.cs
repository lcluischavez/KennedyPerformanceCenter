using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace KennedyPerformanceCenter.Models
{
    public class Service
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string ImagePath { get; set; }
        //[Required]
        //public double Cost { get; set; }
        [Required]
        public string ApplicationUserId { get; set; }


        //[Display(Name = "Posted By")]
        public ApplicationUser ApplicationUser { get; set; }
    }
}