using System.ComponentModel.DataAnnotations;

namespace WebCarInspection.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "User name")]
        [Required(ErrorMessage = "Required user name")]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Required password")]
        public string Password { get; set; }
    }
}
