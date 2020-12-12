using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KennedyPerformanceCenter.Models.ViewModels
{
    public class VehicleFormViewModel
    {
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
        public string VIN { get; set; }
        [Required]
        public int StockNumber { get; set; }
        [Required]
        public string Description { get; set; }

        [Display(Name = "Image Url")]
        public string ImagePath { get; set; }
        public List<SelectListItem> TransmissionTypeOptions { get; set; }

        public int TransmissionTypeId { get; set; }

        [Display(Name = "Transmission Type")]
        public TransmissionType TransmissionType { get; set; }
        public List<SelectListItem> DrivetrainTypeOptions { get; set; }

        public int DrivetrainTypeId { get; set; }

        [Display(Name = "Drivetrain Type")]
        public DrivetrainType DrivetrainType { get; set; }
        public List<SelectListItem> EngineTypeOptions { get; set; }

        public int EngineTypeId { get; set; }

        [Display(Name = "Engine Type")]
        public EngineType EngineType { get; set; }

        public string ApplicationUserId { get; set; }

    }
}
