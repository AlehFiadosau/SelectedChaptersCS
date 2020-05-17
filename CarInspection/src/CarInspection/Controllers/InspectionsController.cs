using BusinessLayer.Ecxeptions;
using BusinessLayer.Entities;
using BusinessLayer.Interfaces;
using CarInspection.Interfaces;
using EasyConsole;
using System;
using System.Linq;

namespace CarInspection.Controllers
{
    public class InspectionsController : IController
    {
        private readonly IService<Inspection, int> _inspectionService;

        public InspectionsController(IService<Inspection, int> inspectionService)
        {
            _inspectionService = inspectionService;
        }

        public void Start()
        {
            Console.WriteLine("Inspection");

            var menu = new Menu()
              .Add("Display all inspections", () => DisplayAllInspections())
              .Add("Display inspection by id", () => DisplayInspectionById())
              .Add("Create inspection", () => CreateInspection())
              .Add("Update inspection", () => UpdateInspection())
              .Add("Delete inspection", () => DeleteInspection());

            menu.Display();
        }

        public void DisplayAllInspections()
        {
            try
            {
                var allInspections = _inspectionService.GetAllAsync().GetAwaiter().GetResult();

                Console.WriteLine("All inspections");
                foreach (var inspection in allInspections)
                {
                    Console.Write("Name: ");
                    Output.WriteLine(ConsoleColor.Green, inspection.Name);
                    Console.Write("Description: ");
                    Output.WriteLine(ConsoleColor.Green, inspection.Description);
                    Console.Write("Price: ");
                    Output.WriteLine(ConsoleColor.Green, inspection.Price.ToString());
                    Console.Write("Inspection date (yy-mm-dd): ");
                    Output.WriteLine(ConsoleColor.Green, inspection.InspectionDate.ToString());
                }
            }
            catch (NotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void DisplayInspectionById()
        {
            Console.WriteLine("Inspection by id");
            try
            {
                Console.Write("Indicate id: ");
                var id = int.Parse(Console.ReadLine());
                var inspection = _inspectionService.GetByIdAsync(id).GetAwaiter().GetResult();

                Console.Write("Name: ");
                Output.WriteLine(ConsoleColor.Green, inspection.Name);
                Console.Write("Description: ");
                Output.WriteLine(ConsoleColor.Green, inspection.Description);
                Console.Write("Price: ");
                Output.WriteLine(ConsoleColor.Green, inspection.Price.ToString());
                Console.Write("Inspection date (yy-mm-dd):");
                Output.WriteLine(ConsoleColor.Green, inspection.InspectionDate.ToString());
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (NotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void CreateInspection()
        {
            var inspection = new Inspection();

            Console.WriteLine("Create inspection");
            try
            {
                Console.WriteLine("Name: ");
                inspection.Name = Console.ReadLine();
                Console.WriteLine("Description: ");
                inspection.Description = Console.ReadLine();
                Console.WriteLine("Price: ");
                inspection.Price = decimal.Parse(Console.ReadLine());
                Console.WriteLine("Inspection date (yy-mm-dd):");
                inspection.InspectionDate = DateTimeOffset.Parse(Console.ReadLine());
                Console.WriteLine("Driver id:");
                inspection.DriverId = int.Parse(Console.ReadLine());
                Console.WriteLine("Inspector id:");
                inspection.InspectorId = int.Parse(Console.ReadLine());

                _inspectionService.CreateAsync(inspection).GetAwaiter().GetResult();
                Console.WriteLine("Inspection created succesfully");
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (DateException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void UpdateInspection()
        {
            var inspection = new Inspection();

            Console.WriteLine("Update inspection");
            try
            {
                Console.WriteLine("Indicate id: ");
                inspection.Id = int.Parse(Console.ReadLine());
                Console.WriteLine("Name: ");
                inspection.Name = Console.ReadLine();
                Console.WriteLine("Description: ");
                inspection.Description = Console.ReadLine();
                Console.WriteLine("Price: ");
                inspection.Price = decimal.Parse(Console.ReadLine());
                Console.WriteLine("Inspection date (yy-mm-dd): ");
                inspection.InspectionDate = DateTimeOffset.Parse(Console.ReadLine());

                _inspectionService.UpdateAsync(inspection).GetAwaiter().GetResult();
                Console.WriteLine("Inspection updated succesfully");
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (NotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (DateException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void DeleteInspection()
        {
            Console.WriteLine("Delete inspection");
            try
            {
                Console.WriteLine("Indicate id: ");
                int id = int.Parse(Console.ReadLine());

                var allInspections = _inspectionService.GetAllAsync().GetAwaiter().GetResult();
                var inspection = allInspections.Where(val => val.Id == id).FirstOrDefault();

                _inspectionService.DeleteAsync(inspection).GetAwaiter().GetResult();
                Console.WriteLine("Inspection deleted succesfully");
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (NotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
