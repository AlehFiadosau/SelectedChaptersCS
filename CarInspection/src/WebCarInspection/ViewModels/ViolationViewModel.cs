using System.ComponentModel.DataAnnotations;

namespace WebCarInspection.ViewModels
{
    public class ViolationViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Required first mame")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Required first description")]
        public string Description { get; set; }
    }
}
