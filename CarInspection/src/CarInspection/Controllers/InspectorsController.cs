using BusinessLayer.Ecxeptions;
using BusinessLayer.Entities;
using BusinessLayer.Interfaces;
using CarInspection.Interfaces;
using EasyConsole;
using System;
using System.Linq;

namespace CarInspection.Controllers
{
    public class InspectorsController : IController
    {
        private readonly IService<Inspector, int> _inspectorService;

        public InspectorsController(IService<Inspector, int> inspectorService)
        {
            _inspectorService = inspectorService;
        }

        public void Start()
        {
            Console.WriteLine("Inspector");

            var menu = new Menu()
              .Add("Display all inspectors", () => DisplayAllInspectors())
              .Add("Display inspector by id", () => DisplayInspectorById())
              .Add("Create inspector", () => CreateInspector())
              .Add("Update inspector", () => UpdateInspector())
              .Add("Delete inspector", () => DeleteInspector());

            menu.Display();
        }

        public void DisplayAllInspectors()
        {
            try
            {
                var allInspectors = _inspectorService.GetAllAsync().GetAwaiter().GetResult();

                Console.WriteLine("All inspectors");
                foreach (var inspector in allInspectors)
                {
                    Console.Write("First name: ");
                    Output.WriteLine(ConsoleColor.Green, inspector.FirstName);
                    Console.Write("Furnmae: ");
                    Output.WriteLine(ConsoleColor.Green, inspector.Surname);
                    Console.Write("Patronic: ");
                    Output.WriteLine(ConsoleColor.Green, inspector.Patronic);
                    Console.Write("Phone: ");
                    Output.WriteLine(ConsoleColor.Green, inspector.Phone);
                    Console.Write("Personal number: ");
                    Output.WriteLine(ConsoleColor.Green, inspector.PersonalNumber.ToString());
                }
            }
            catch (NotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void DisplayInspectorById()
        {
            Console.WriteLine("Inspector by id");

            try
            {
                Console.Write("Indicate id: ");
                var id = int.Parse(Console.ReadLine());
                var inspector = _inspectorService.GetByIdAsync(id).GetAwaiter().GetResult();

                Console.Write("Firs name: ");
                Output.WriteLine(ConsoleColor.Green, inspector.FirstName);
                Console.Write("Surname: ");
                Output.WriteLine(ConsoleColor.Green, inspector.Surname);
                Console.Write("Patronic: ");
                Output.WriteLine(ConsoleColor.Green, inspector.Patronic);
                Console.Write("Phone: ");
                Output.WriteLine(ConsoleColor.Green, inspector.Phone);
                Console.Write("Personal number:");
                Output.WriteLine(ConsoleColor.Green, inspector.PersonalNumber.ToString());
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

        public void CreateInspector()
        {
            var inspector = new Inspector();

            Console.WriteLine("Create inspector");
            try
            {
                Console.WriteLine("First name: ");
                inspector.FirstName = Console.ReadLine();
                Console.WriteLine("Surname: ");
                inspector.Surname = Console.ReadLine();
                Console.WriteLine("Patronic: ");
                inspector.Patronic = Console.ReadLine();
                Console.WriteLine("Phone:");
                inspector.Phone = Console.ReadLine();
                Console.WriteLine("Personal number:");
                inspector.PersonalNumber = long.Parse(Console.ReadLine());

                _inspectorService.CreateAsync(inspector).GetAwaiter().GetResult();
                Console.WriteLine("Inspector created succesfully:");
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void UpdateInspector()
        {
            var inspector = new Inspector();

            Console.WriteLine("Update inspector");
            try
            {
                Console.WriteLine("Indicate id: ");
                inspector.Id = int.Parse(Console.ReadLine());
                Console.WriteLine("First name: ");
                inspector.FirstName = Console.ReadLine();
                Console.WriteLine("Surname: ");
                inspector.Surname = Console.ReadLine();
                Console.WriteLine("Patronic: ");
                inspector.Patronic = Console.ReadLine();
                Console.WriteLine("Phone: ");
                inspector.Phone = Console.ReadLine();
                Console.WriteLine("Personal number: ");
                inspector.PersonalNumber = int.Parse(Console.ReadLine());

                _inspectorService.UpdateAsync(inspector).GetAwaiter().GetResult();
                Console.WriteLine("Inspector updated succesfully:");
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (NotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void DeleteInspector()
        {
            Console.WriteLine("Delete inspector");
            try
            {
                Console.WriteLine("Indicated id: ");
                int id = int.Parse(Console.ReadLine());

                var allInspectors = _inspectorService.GetAllAsync().GetAwaiter().GetResult();
                var inspector = allInspectors.Where(val => val.Id == id).FirstOrDefault();

                _inspectorService.DeleteAsync(inspector).GetAwaiter().GetResult();
                Console.WriteLine("Inspector deleted succesfully:");
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
