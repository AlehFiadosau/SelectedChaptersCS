using Microsoft.AspNetCore.Identity;

namespace WebCarInspection.ViewModels
{
    public class UserViewModel : IdentityUser
    {
        public string FirstName { get; set; }

        public string Surname { get; set; }

        public string Patronic { get; set; }
    }
}
