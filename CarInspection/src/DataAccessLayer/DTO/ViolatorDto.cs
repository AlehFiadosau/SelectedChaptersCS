using System;

namespace DataAccessLayer.DTO
{
    public class ViolatorDto
    {
        public int Id { get; set; }

        public DateTimeOffset ReinspectionDate { get; set; }

        public int ViolationId { get; set; }

        public int DriverId { get; set; }

        public int InspectorId { get; set; }
    }
}
