﻿using System;
using System.ComponentModel.DataAnnotations;

namespace WebCarInspection.ViewModels
{
    public class ViolatorViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Reinspection date")]
        [Required(ErrorMessage = "Required first reinspection date")]
        [DataType(DataType.Date)]
        public DateTimeOffset ReinspectionDate { get; set; }

        public int ViolationId { get; set; }

        public int DriverId { get; set; }

        public int InspectorId { get; set; }
    }
}
