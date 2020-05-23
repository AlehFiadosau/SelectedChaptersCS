using System.ComponentModel.DataAnnotations;

namespace WebCarInspection.Models
{
    public class InspectorViewModel
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

        [Display(Name = "Phone")]
        [Required(ErrorMessage = "Required phone")]
        public string Phone { get; set; }

        [Display(Name = "Personal number")]
        [Required(ErrorMessage = "Required personal number")]
        public long PersonalNumber { get; set; }
    }
}
