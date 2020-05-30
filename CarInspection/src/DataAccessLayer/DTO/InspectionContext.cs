using DataAccessLayer.DTO.DB;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.DTO
{
    public class InspectionContext : IdentityDbContext<UserDto>
    {
        public InspectionContext(DbContextOptions options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public virtual DbSet<DriverDto> Driver { get; set; }

        public virtual DbSet<InspectorDto> Inspector { get; set; }

        public virtual DbSet<InspectionDto> Inspection { get; set; }

        public virtual DbSet<ViolatorDto> Violator { get; set; }

        public virtual DbSet<ViolationDto> Violations { get; set; }
    }
}
