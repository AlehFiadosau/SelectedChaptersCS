using System;

namespace DataAccessLayer.DTO
{
    public class DriverDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string Surname { get; set; }

        public string Patronic { get; set; }

        public string Address { get; set; }

        public string LicenseNumber { get; set; }

        public DateTimeOffset DateOfBirth { get; set; }

        public DateTimeOffset DateOfRights { get; set; }
    }
}
