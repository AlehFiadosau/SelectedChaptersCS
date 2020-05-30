using System.ComponentModel.DataAnnotations;

namespace WebCarInspection.ViewModels
{
    public class UserRegistrationViewModel
    {
        [Display(Name = "User name")]
        [Required(ErrorMessage = "Required user name")]
        public string UserName { get; set; }

        [Display(Name = "First name")]
        [Required(ErrorMessage = "Required first name")]
        public string FirstName { get; set; }

        [Display(Name = "Surname")]
        [Required(ErrorMessage = "Required surname")]
        public string Surname { get; set; }

        [Display(Name = "Patronic")]
        [Required(ErrorMessage = "Required patronic")]
        public string Patronic { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Required password")]
        public string Password { get; set; }

        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "Required confirm password")]
        public string ConfirmPassword { get; set; }
    }
}
