using BusinessLayer.Entities;
using BusinessLayer.Infrastructe;
using BusinessLayer.Interfaces;
using EasyConsole;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CarInspection.Controllers
{
    public class ViolationController
    {
        private readonly IService<Violation> _violationService;

        public ViolationController(IService<Violation> violationService)
        {
            _violationService = violationService;
        }

        public void Start()
        {
            Console.WriteLine("Нарушение");

            var menu = new Menu()
              .Add("Отобразить все нарушения", () => GetAllViolation().GetAwaiter().GetResult())
              .Add("Отобразить нарушение по номеру", () => GetByIdViolation().GetAwaiter().GetResult())
              .Add("Создание нарушение", () => CreateViolation().GetAwaiter().GetResult())
              .Add("Изменение нарушение", () => UpdateViolation().GetAwaiter().GetResult())
              .Add("Удаление нарушение", () => DeleteViolation().GetAwaiter().GetResult());

            menu.Display();
        }

        public async Task GetAllViolation()
        {
            try
            {
                var allViolations = await _violationService.GetAll();

                Console.WriteLine("Все нарушения");
                foreach (var violation in allViolations)
                {
                    Console.Write("Назвнаие: ");
                    Output.WriteLine(ConsoleColor.Green, violation.Name);
                    Console.Write("Описание: ");
                    Output.WriteLine(ConsoleColor.Green, violation.Description);
                }
            }
            catch (NotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task GetByIdViolation()
        {
            Console.WriteLine("Нарушение по номеру");
            Console.Write("Укажите номер: ");
            try
            {
                var id = int.Parse(Console.ReadLine());
                var violation = await _violationService.GetById(id);

                Console.Write("Название: ");
                Output.WriteLine(ConsoleColor.Green, violation.Name);
                Console.Write("Описание: ");
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

        public async Task CreateViolation()
        {
            var violation = new Violation();

            Console.WriteLine("Создание нарушение");
            Console.WriteLine("Укажите Название: ");
            violation.Name = Console.ReadLine();
            Console.WriteLine("Укажите Описание: ");
            violation.Description = Console.ReadLine();

            await _violationService.Create(violation);
        }

        public async Task UpdateViolation()
        {
            var violation = new Violation();

            try
            {
                Console.WriteLine("Изменение данных об нарушении");
                Console.WriteLine("Укажите Номер нарушения: ");
                violation.Id = int.Parse(Console.ReadLine());
                Console.WriteLine("Укажите Название: ");
                violation.Name = Console.ReadLine();
                Console.WriteLine("Укажите Описание: ");
                violation.Description = Console.ReadLine();

                await _violationService.Update(violation);
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

        public async Task DeleteViolation()
        {
            Console.WriteLine("Удаление нарушения");
            Console.WriteLine("Укажите Номер нарушения: ");
            try
            {
                int id = int.Parse(Console.ReadLine());

                var allViolations = await _violationService.GetAll();
                var violation = allViolations.Where(val => val.Id == id).FirstOrDefault();

                await _violationService.Delete(violation);
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
