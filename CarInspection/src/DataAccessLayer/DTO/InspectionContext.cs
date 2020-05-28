using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.DTO
{
    public class InspectionContext : DbContext
    {
        public InspectionContext(DbContextOptions<InspectionContext> options)
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
