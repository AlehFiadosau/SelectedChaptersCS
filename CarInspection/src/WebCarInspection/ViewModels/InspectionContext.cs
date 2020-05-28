using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebCarInspection.ViewModels
{
    public class InspectionContext : IdentityDbContext<UserViewModel>
    {
        public InspectionContext(DbContextOptions<InspectionContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public virtual DbSet<DriverViewModel> Driver { get; set; }

        public virtual DbSet<InspectorViewModel> Inspector { get; set; }

        public virtual DbSet<InspectionViewModel> Inspection { get; set; }

        public virtual DbSet<ViolatorViewModel> Violator { get; set; }

        public virtual DbSet<ViolationViewModel> Violations { get; set; }
    }
}
