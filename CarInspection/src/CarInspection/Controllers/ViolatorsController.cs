using BusinessLayer.Ecxeptions;
using BusinessLayer.Entities;
using BusinessLayer.Interfaces;
using CarInspection.Interfaces;
using EasyConsole;
using System;
using System.Linq;

namespace CarInspection.Controllers
{
    public class ViolatorsController : IController
    {
        private readonly IService<Violator, int> _violatorService;

        public ViolatorsController(IService<Violator, int> violatorService)
        {
            _violatorService = violatorService;
        }

        public void Start()
        {
            Console.WriteLine("Violator");

            var menu = new Menu()
              .Add("Display all violators", () => DisplayAllViolators())
              .Add("Display violator by id", () => DisplayViolatorById())
              .Add("Create violator", () => CreateViolator())
              .Add("Update violator", () => UpdateViolator())
              .Add("Delete violator", () => DeleteViolator());

            menu.Display();
        }

        public void DisplayAllViolators()
        {
            try
            {
                var allViolators = _violatorService.GetAllAsync().GetAwaiter().GetResult();

                Console.WriteLine("All inspectors");
                foreach (var violator in allViolators)
                {
                    Console.Write("Driver id: ");
                    Output.WriteLine(ConsoleColor.Green, violator.DriverId.ToString());
                    Console.Write("Inspector id: ");
                    Output.WriteLine(ConsoleColor.Green, violator.InspectorId.ToString());
                    Console.Write("Violation id: ");
                    Output.WriteLine(ConsoleColor.Green, violator.ViolationId.ToString());
                    Console.Write("Reinspection date (yy-mm-dd): ");
                    Output.WriteLine(ConsoleColor.Green, violator.ReinspectionDate.ToString());
                }
            }
            catch (NotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void DisplayViolatorById()
        {
            Console.WriteLine("Violator by id");

            try
            {
                Console.Write("Indicate id: ");
                var id = int.Parse(Console.ReadLine());
                var violator = _violatorService.GetByIdAsync(id).GetAwaiter().GetResult();

                Console.Write("Driver id: ");
                Output.WriteLine(ConsoleColor.Green, violator.DriverId.ToString());
                Console.Write("Inspector id: ");
                Output.WriteLine(ConsoleColor.Green, violator.InspectorId.ToString());
                Console.Write("Violation id: ");
                Output.WriteLine(ConsoleColor.Green, violator.ViolationId.ToString());
                Console.Write("Reinspection date (yy-mm-dd): ");
                Output.WriteLine(ConsoleColor.Green, violator.ReinspectionDate.ToString());
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void CreateViolator()
        {
            var violator = new Violator();

            try
            {
                Console.WriteLine("Create violator");
                Console.WriteLine("Driver id: ");
                violator.DriverId = int.Parse(Console.ReadLine());
                Console.WriteLine("Inspector id: ");
                violator.InspectorId = int.Parse(Console.ReadLine());
                Console.WriteLine("Violation id: ");
                violator.ViolationId = int.Parse(Console.ReadLine());
                Console.WriteLine("Reinspection date (yy-mm-dd): ");
                violator.ReinspectionDate = DateTimeOffset.Parse(Console.ReadLine());

                _violatorService.CreateAsync(violator).GetAwaiter().GetResult();
                Console.Write("Violator created succesfully ");
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (DateException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void UpdateViolator()
        {
            var violator = new Violator();
            Console.WriteLine("Update violator");

            try
            {
                Console.WriteLine("Indicate id: ");
                violator.Id = int.Parse(Console.ReadLine());
                Console.WriteLine("Reinspection date (yy-mm-dd): ");
                violator.ReinspectionDate = DateTimeOffset.Parse(Console.ReadLine());

                _violatorService.UpdateAsync(violator).GetAwaiter().GetResult();
                Console.Write("Violator updated succesfully ");
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
        }

        public void DeleteViolator()
        {
            Console.WriteLine("Delete violator");

            try
            {
                Console.WriteLine("Indicate id: ");
                int id = int.Parse(Console.ReadLine());

                var allViolators = _violatorService.GetAllAsync().GetAwaiter().GetResult();
                var violator = allViolators.Where(val => val.Id == id).FirstOrDefault();

                _violatorService.DeleteAsync(violator).GetAwaiter().GetResult();
                Console.Write("Violator deleted succesfully ");
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
