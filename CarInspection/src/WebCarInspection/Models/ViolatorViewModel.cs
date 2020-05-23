using System;
using System.ComponentModel.DataAnnotations;

namespace WebCarInspection.Models
{
    public class ViolatorViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Reinspection date")]
        [Required(ErrorMessage = "Required first reinspection date")]
        public DateTimeOffset ReinspectionDate { get; set; }

        public int ViolationId { get; set; }

        public int DriverId { get; set; }

        public int InspectorId { get; set; }
    }
}
