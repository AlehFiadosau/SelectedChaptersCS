using System;
using System.ComponentModel.DataAnnotations;

namespace WebCarInspection.ViewModels
{
    public class InspectionViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Required name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Required description")]
        public string Description { get; set; }

        [Display(Name = "Price")]
        [Required(ErrorMessage = "Required price")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal Price { get; set; }

        [Display(Name = "InspectionDate")]
        [Required(ErrorMessage = "Required inspection date")]
        public DateTimeOffset InspectionDate { get; set; }

        public int InspectorId { get; set; }

        public int DriverId { get; set; }
    }
}
