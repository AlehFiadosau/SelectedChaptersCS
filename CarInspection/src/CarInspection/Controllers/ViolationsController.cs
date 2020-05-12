using BusinessLayer.Ecxeptions;
using BusinessLayer.Entities;
using BusinessLayer.Interfaces;
using CarInspection.Interfaces;
using EasyConsole;
using System;
using System.Linq;

namespace CarInspection.Controllers
{
    public class ViolationsController : IController
    {
        private readonly IService<Violation, int> _violationService;

        public ViolationsController(IService<Violation, int> violationService)
        {
            _violationService = violationService;
        }

        public void Start()
        {
            Console.WriteLine("Violation");

            var menu = new Menu()
              .Add("Display all violations", () => DisplayAllViolations())
              .Add("Display violation by id", () => DisplayViolatioByIdn())
              .Add("Create violation", () => CreateViolation())
              .Add("Update violation", () => UpdateViolation())
              .Add("Delete violation", () => DeleteViolation());

            menu.Display();
        }

        public void DisplayAllViolations()
        {
            try
            {
                var allViolations = _violationService.GetAllAsync().GetAwaiter().GetResult();

                Console.WriteLine("All violations");
                foreach (var violation in allViolations)
                {
                    Console.Write("Name: ");
                    Output.WriteLine(ConsoleColor.Green, violation.Name);
                    Console.Write("Description: ");
                    Output.WriteLine(ConsoleColor.Green, violation.Description);
                }
            }
            catch (NotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void DisplayViolatioByIdn()
        {
            Console.WriteLine("Violation by id");
            try
            {
                Console.Write("Indicate id: ");
                var id = int.Parse(Console.ReadLine());
                var violation = _violationService.GetByIdAsync(id).GetAwaiter().GetResult();

                Console.Write("Name: ");
                Output.WriteLine(ConsoleColor.Green, violation.Name);
                Console.Write("Description: ");
                Output.WriteLine(ConsoleColor.Green, violation.Description);
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

        public void CreateViolation()
        {
            var violation = new Violation();

            Console.WriteLine("Create violation");
            Console.WriteLine("Name: ");
            violation.Name = Console.ReadLine();
            Console.WriteLine("Description: ");
            violation.Description = Console.ReadLine();

            _violationService.CreateAsync(violation).GetAwaiter().GetResult();
            Console.WriteLine("Inspection created succesfully");
        }

        public void UpdateViolation()
        {
            var violation = new Violation();
            Console.WriteLine("Update violation");

            try
            {
                Console.WriteLine("Indicate id: ");
                violation.Id = int.Parse(Console.ReadLine());
                Console.WriteLine("Name: ");
                violation.Name = Console.ReadLine();
                Console.WriteLine("Description: ");
                violation.Description = Console.ReadLine();

                _violationService.UpdateAsync(violation).GetAwaiter().GetResult();
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
        }

        public void DeleteViolation()
        {
            Console.WriteLine("Delete violation");
            try
            {
                Console.WriteLine("Indicate id: ");
                int id = int.Parse(Console.ReadLine());

                var allViolations = _violationService.GetAllAsync().GetAwaiter().GetResult();
                var violation = allViolations.Where(val => val.Id == id).FirstOrDefault();

                _violationService.DeleteAsync(violation).GetAwaiter().GetResult();
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
