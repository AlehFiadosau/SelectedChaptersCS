using BusinessLayer.Entities;
using BusinessLayer.Infrastructe;
using BusinessLayer.Interfaces;
using EasyConsole;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CarInspection.Controllers
{
    public class DriverController
    {
        private readonly IService<Driver> _driverService;

        public DriverController(IService<Driver> driverService)
        {
            _driverService = driverService;
        }

        public void Start()
        {
            Console.WriteLine("Водитель");

            var menu = new Menu()
              .Add("Отобразить всех водителей", () => GetAllDriver().GetAwaiter().GetResult())
              .Add("Отобразить водителя по номеру", () => GetByIdDriver().GetAwaiter().GetResult())
              .Add("Создание водителя", () => CreateDriver().GetAwaiter().GetResult())
              .Add("Изменение водителя", () => UpdateDriver().GetAwaiter().GetResult())
              .Add("Удаление водителя", () => DeleteDriver().GetAwaiter().GetResult());
            
            menu.Display();
        }

        public async Task GetAllDriver()
        {
            try
            {
                var allDrivers = await _driverService.GetAll();

                Console.WriteLine("Все водители");
                foreach (var driver in allDrivers)
                {
                    Console.Write("Имя: ");
                    Output.WriteLine(ConsoleColor.Green, driver.FirstName);
                    Console.Write("Фамилию: ");
                    Output.WriteLine(ConsoleColor.Green, driver.Surname);
                    Console.Write("Отчество: ");
                    Output.WriteLine(ConsoleColor.Green, driver.Patronic);
                    Console.Write("Адрес: ");
                    Output.WriteLine(ConsoleColor.Green, driver.Address);
                    Console.Write("Номер лицензии: ");
                    Output.WriteLine(ConsoleColor.Green, driver.LicenseNumber);
                    Console.Write("Дата рождения (гг-мм-дд):");
                    Output.WriteLine(ConsoleColor.Green, driver.DateOfBirth.ToString());
                    Console.Write("Дата получения прав (гг-мм-дд):");
                    Output.WriteLine(ConsoleColor.Green, driver.DateOfRights.ToString());
                }
            }
            catch (NotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task GetByIdDriver()
        {
            Console.WriteLine("Водитель по номеру");
            Console.Write("Укажите номер: ");
            try
            {
                var id = int.Parse(Console.ReadLine());
                var driver = await _driverService.GetById(id);

                Console.Write("Имя: ");
                Output.WriteLine(ConsoleColor.Green, driver.FirstName);
                Console.Write("Фамилию: ");
                Output.WriteLine(ConsoleColor.Green, driver.Surname);
                Console.Write("Отчество: ");
                Output.WriteLine(ConsoleColor.Green, driver.Patronic);
                Console.Write("Адрес: ");
                Output.WriteLine(ConsoleColor.Green, driver.Address);
                Console.Write("Номер лицензии: ");
                Output.WriteLine(ConsoleColor.Green, driver.LicenseNumber);
                Console.Write("Дата рождения (гг-мм-дд):");
                Output.WriteLine(ConsoleColor.Green, driver.DateOfBirth.ToString());
                Console.Write("Дата получения прав (гг-мм-дд):");
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

        public async Task CreateDriver()
        {
            var driver = new Driver();

            Console.WriteLine("Создание водителя");
            Console.WriteLine("Укажите Имя: ");
            try
            {
                driver.FirstName = Console.ReadLine();
                Console.WriteLine("Укажите Фамилию: ");
                driver.Surname = Console.ReadLine();
                Console.WriteLine("Укажите Отчество: ");
                driver.Patronic = Console.ReadLine();
                Console.WriteLine("Укажите Адрес: ");
                driver.Address = Console.ReadLine();
                Console.WriteLine("Укажите Номер лицензии: ");
                driver.LicenseNumber = Console.ReadLine();
                Console.WriteLine("Укажите Дату рождения (гг-мм-дд):");
                driver.DateOfBirth = DateTimeOffset.Parse(Console.ReadLine());
                Console.WriteLine("Укажите Дату получения прав (гг-мм-дд):");
                driver.DateOfRights = DateTimeOffset.Parse(Console.ReadLine());
                Console.WriteLine("Водитель успешно создан!");

                await _driverService.Create(driver);
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

        public async Task UpdateDriver()
        {
            var driver = new Driver();

            Console.WriteLine("Изменение данных о водителе");
            Console.WriteLine("Укажите Номер водителя: ");
            try
            {
                driver.Id = int.Parse(Console.ReadLine());
                Console.WriteLine("Укажите Имя: ");
                driver.FirstName = Console.ReadLine();
                Console.WriteLine("Укажите Фамилию: ");
                driver.Surname = Console.ReadLine();
                Console.WriteLine("Укажите Отчество: ");
                driver.Patronic = Console.ReadLine();
                Console.WriteLine("Укажите Адрес: ");
                driver.Address = Console.ReadLine();
                Console.WriteLine("Укажите Номер лицензии: ");
                driver.LicenseNumber = Console.ReadLine();
                Console.WriteLine("Укажите Дату рождения (гг-мм-дд):");
                driver.DateOfBirth = DateTimeOffset.Parse(Console.ReadLine());
                Console.WriteLine("Укажите Дату получения прав (гг-мм-дд):");
                driver.DateOfRights = DateTimeOffset.Parse(Console.ReadLine());
                Console.WriteLine("Водитель успешно Изменен!");

                await _driverService.Update(driver);
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

        public async Task DeleteDriver()
        {
            Console.WriteLine("Удаление водителя");
            Console.WriteLine("Укажите Номер водителя: ");
            try
            {
                int id = int.Parse(Console.ReadLine());

                var allDrivers = await _driverService.GetAll();
                var driver = allDrivers.Where(val => val.Id == id).FirstOrDefault();

                await _driverService.Delete(driver);
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
