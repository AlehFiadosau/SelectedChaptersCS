using Microsoft.AspNetCore.Identity;

namespace DataAccessLayer.DTO.DB
{
    public class UserDto : IdentityUser
    {
        public string FirstName { get; set; }

        public string Surname { get; set; }

        public string Patronic { get; set; }
    }
}
