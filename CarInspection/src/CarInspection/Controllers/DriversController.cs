using BusinessLayer.Ecxeptions;
using BusinessLayer.Entities;
using BusinessLayer.Interfaces;
using CarInspection.Interfaces;
using EasyConsole;
using System;
using System.Linq;

namespace CarInspection.Controllers
{
    public class DriversController : IController
    {
        private readonly IService<Driver, int> _driverService;

        public DriversController(IService<Driver, int> driverService)
        {
            _driverService = driverService;
        }

        public void Start()
        {
            Console.WriteLine("Driver");

            var menu = new Menu()
              .Add("Display all drivers", () => DisplayAllDrivers())
              .Add("Display driver by id", () => DisplayDriverById())
              .Add("Create driver", () => CreateDriver())
              .Add("Update driver", () => UpdateDriver())
              .Add("Delete driver", () => DeleteDriver());
            
            menu.Display();
        }

        public void DisplayAllDrivers()
        {
            try
            {
                var allDrivers = _driverService.GetAllAsync().GetAwaiter().GetResult();

                Console.WriteLine("All drivers");
                foreach (var driver in allDrivers)
                {
                    Console.Write("First name: ");
                    Output.WriteLine(ConsoleColor.Green, driver.FirstName);
                    Console.Write("Surname: ");
                    Output.WriteLine(ConsoleColor.Green, driver.Surname);
                    Console.Write("Patronic: ");
                    Output.WriteLine(ConsoleColor.Green, driver.Patronic);
                    Console.Write("Address: ");
                    Output.WriteLine(ConsoleColor.Green, driver.Address);
                    Console.Write("License number: ");
                    Output.WriteLine(ConsoleColor.Green, driver.LicenseNumber);
                    Console.Write("Date of birth (yy-mm-dd):");
                    Output.WriteLine(ConsoleColor.Green, driver.DateOfBirth.ToString());
                    Console.Write("Date of rights (yy-mm-dd):");
                    Output.WriteLine(ConsoleColor.Green, driver.DateOfRights.ToString());
                }
            }
            catch (NotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void DisplayDriverById()
        {
            Console.WriteLine("Driver by id");
            try
            {
                Console.Write("Indicate id: ");
                var id = int.Parse(Console.ReadLine());
                var driver = _driverService.GetByIdAsync(id).GetAwaiter().GetResult();

                Console.Write("First name: ");
                Output.WriteLine(ConsoleColor.Green, driver.FirstName);
                Console.Write("Surname: ");
                Output.WriteLine(ConsoleColor.Green, driver.Surname);
                Console.Write("Patronic: ");
                Output.WriteLine(ConsoleColor.Green, driver.Patronic);
                Console.Write("Address: ");
                Output.WriteLine(ConsoleColor.Green, driver.Address);
                Console.Write("License number: ");
                Output.WriteLine(ConsoleColor.Green, driver.LicenseNumber);
                Console.Write("Date of birth (yy-mm-dd):");
                Output.WriteLine(ConsoleColor.Green, driver.DateOfBirth.ToString());
                Console.Write("Date of rights (yy-mm-dd):");
                Output.WriteLine(ConsoleColor.Green, driver.DateOfRights.ToString());
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

        public void CreateDriver()
        {
            var driver = new Driver();

            Console.WriteLine("Create driver");
            try
            {
                Console.WriteLine("First name: ");
                driver.FirstName = Console.ReadLine();
                Console.WriteLine("Surname: ");
                driver.Surname = Console.ReadLine();
                Console.WriteLine("Patronic: ");
                driver.Patronic = Console.ReadLine();
                Console.WriteLine("Address: ");
                driver.Address = Console.ReadLine();
                Console.WriteLine("License number: ");
                driver.LicenseNumber = Console.ReadLine();
                Console.WriteLine("Date of birth (yy-mm-dd):");
                driver.DateOfBirth = DateTimeOffset.Parse(Console.ReadLine());
                Console.WriteLine("Date of rights (yy-mm-dd):");
                driver.DateOfRights = DateTimeOffset.Parse(Console.ReadLine());

                _driverService.CreateAsync(driver).GetAwaiter().GetResult();
                Console.WriteLine("Driver created succesfully");
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

        public void UpdateDriver()
        {
            var driver = new Driver();

            Console.WriteLine("Update driver");
            try
            {
                Console.WriteLine("Indicate id: ");
                driver.Id = int.Parse(Console.ReadLine());
                Console.WriteLine("First name: ");
                driver.FirstName = Console.ReadLine();
                Console.WriteLine("Surname: ");
                driver.Surname = Console.ReadLine();
                Console.WriteLine("Patronic: ");
                driver.Patronic = Console.ReadLine();
                Console.WriteLine("Address: ");
                driver.Address = Console.ReadLine();
                Console.WriteLine("License number: ");
                driver.LicenseNumber = Console.ReadLine();
                Console.WriteLine("Date of birth (yy-mm-dd):");
                driver.DateOfBirth = DateTimeOffset.Parse(Console.ReadLine());
                Console.WriteLine("Date of rights (yy-mm-dd):");
                driver.DateOfRights = DateTimeOffset.Parse(Console.ReadLine());

                _driverService.UpdateAsync(driver).GetAwaiter().GetResult();
                Console.WriteLine("Driver updates succesfuly");
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

        public void DeleteDriver()
        {
            Console.WriteLine("Delete driver");
            try
            {
                Console.WriteLine("Indicate id: ");
                int id = int.Parse(Console.ReadLine());

                var allDrivers = _driverService.GetAllAsync().GetAwaiter().GetResult();
                var driver = allDrivers.Where(val => val.Id == id).FirstOrDefault();

                _driverService.DeleteAsync(driver).GetAwaiter().GetResult();
                Console.WriteLine("Driver deleted succesfully");
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
