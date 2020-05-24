using System;
using System.ComponentModel.DataAnnotations;

namespace WebCarInspection.ViewModels
{
    public class DriverViewModel
    {
        public int Id { get; set; }

        [Display(Name = "First name")]
        [Required(ErrorMessage = "Required first name")]
        public string FirstName { get; set; }

        [Display(Name = "Surname")]
        [Required(ErrorMessage = "Required surname")]
        public string Surname { get; set; }

        [Display(Name = "Patronic")]
        [Required(ErrorMessage = "Required patronic")]
        public string Patronic { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "Required address")]
        public string Address { get; set; }

        [Display(Name = "License number")]
        [Required(ErrorMessage = "Required license number")]
        public string LicenseNumber { get; set; }

        [Display(Name = "Date of birth")]
        [Required(ErrorMessage = "Required date of birth")]
        public DateTimeOffset DateOfBirth { get; set; }

        [Display(Name = "Date of rights")]
        [Required(ErrorMessage = "Required date of rights")]
        public DateTimeOffset DateOfRights { get; set; }
    }
}
