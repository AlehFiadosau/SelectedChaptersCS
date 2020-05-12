using System;

namespace BusinessLayer.Entities
{
    public class Driver
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string Surname { get; set; }

        public string Patronic { get; set; }

        public string Address { get; set; }

        public string LicenseNumber { get; set; }

        public DateTimeOffset DateOfBirth { get; set; }

        // date of driving license 
        public DateTimeOffset DateOfRights { get; set; }
    }
}
