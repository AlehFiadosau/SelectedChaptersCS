using System;

namespace CarInspectionApi.ViewModels
{
    public class InspectionViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public DateTimeOffset InspectionDate { get; set; }

        public int InspectorId { get; set; }

        public int DriverId { get; set; }
    }
}
