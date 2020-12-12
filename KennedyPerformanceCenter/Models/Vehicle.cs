using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KennedyPerformanceCenter.Models
{
    public class Vehicle
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Make { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public string Color { get; set; }
        [Required]
        public int Miles { get; set; }
        [Required]
        public int TransmissionTypeId { get; set; }
        [Display(Name = "Transmission Type")]
        public TransmissionType TransmissionType { get; set; }
        [Required]
        public int EngineTypeId { get; set; }
        [Display(Name = "Engine Type")]
        public EngineType EngineType { get; set; }
        [Required]
        public int DrivetrainTypeId { get; set; }
        [Display(Name = "Drivetrain Type")]
        public DrivetrainType DrivetrainType { get; set; }
        [Required]
        public string VIN { get; set; }
        [Required]
        public int StockNumber { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string ImagePath { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }
        [Display(Name = "By")]
        public ApplicationUser ApplicationUser { get; set; }
    }
}