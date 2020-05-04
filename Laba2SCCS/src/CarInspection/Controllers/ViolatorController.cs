using BusinessLayer.Entities;
using BusinessLayer.Infrastructe;
using BusinessLayer.Interfaces;
using EasyConsole;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CarInspection.Controllers
{
    public class ViolatorController
    {
        private readonly IService<Violator> _violatorService;

        public ViolatorController(IService<Violator> violatorService)
        {
            _violatorService = violatorService;
        }

        public void Start()
        {
            Console.WriteLine("Нарушитель");

            var menu = new Menu()
              .Add("Отобразить все нарушители", () => GetAllViolator().GetAwaiter().GetResult())
              .Add("Отобразить нарушителя по номеру", () => GetByIdViolator().GetAwaiter().GetResult())
              .Add("Создание нарушителя", () => CreateViolator().GetAwaiter().GetResult())
              .Add("Изменение нарушителя", () => UpdateViolator().GetAwaiter().GetResult())
              .Add("Удаление нарушителя", () => DeleteViolator().GetAwaiter().GetResult());

            menu.Display();
        }

        public async Task GetAllViolator()
        {
            try
            {
                var allViolators = await _violatorService.GetAll();

                Console.WriteLine("Все нарушители");
                foreach (var violator in allViolators)
                {
                    Console.Write("Номер водителя: ");
                    Output.WriteLine(ConsoleColor.Green, violator.DriverId.ToString());
                    Console.Write("Номер инспектора: ");
                    Output.WriteLine(ConsoleColor.Green, violator.InspectorId.ToString());
                    Console.Write("Номер нарушения: ");
                    Output.WriteLine(ConsoleColor.Green, violator.ViolationId.ToString());
                    Console.Write("Дата повторной инспекции (гг-мм-дд): ");
                    Output.WriteLine(ConsoleColor.Green, violator.ReinspectionDate.ToString());
                }
            }
            catch (NotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task GetByIdViolator()
        {
            Console.WriteLine("Нарушитель по номеру");
            Console.Write("Укажите номер: ");

            try
            {
                var id = int.Parse(Console.ReadLine());
                var violator = await _violatorService.GetById(id);

                Console.Write("Номер водителя: ");
                Output.WriteLine(ConsoleColor.Green, violator.DriverId.ToString());
                Console.Write("Номер инспектора: ");
                Output.WriteLine(ConsoleColor.Green, violator.InspectorId.ToString());
                Console.Write("Номер нарушения: ");
                Output.WriteLine(ConsoleColor.Green, violator.ViolationId.ToString());
                Console.Write("Дата повторной инспекции (гг-мм-дд): ");
                Output.WriteLine(ConsoleColor.Green, violator.ReinspectionDate.ToString());
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task CreateViolator()
        {
            var violator = new Violator();

            try
            {
                Console.WriteLine("Создание нарушителя");
                Console.WriteLine("Укажите Номер водителя: ");
                violator.DriverId = int.Parse(Console.ReadLine());
                Console.WriteLine("Укажите Номер инспектора: ");
                violator.InspectorId = int.Parse(Console.ReadLine());
                Console.WriteLine("Укажите Номер нарушения: ");
                violator.ViolationId = int.Parse(Console.ReadLine());
                Console.WriteLine("Укажите Дату повторной инспекции (гг-мм-дд): ");
                violator.ReinspectionDate = DateTimeOffset.Parse(Console.ReadLine());

                await _violatorService.Create(violator);
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

        public async Task UpdateViolator()
        {
            var violator = new Violator();

            try
            {
                Console.WriteLine("Изменение данных об нарушителе");
                Console.WriteLine("Укажите Номер нарушителя: ");
                violator.Id = int.Parse(Console.ReadLine());
                Console.WriteLine("Укажите Дату повторной инспекции (гг-мм-дд): ");
                violator.ReinspectionDate = DateTimeOffset.Parse(Console.ReadLine());

                await _violatorService.Update(violator);
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

        public async Task DeleteViolator()
        {
            Console.WriteLine("Удаление нарушителя");
            Console.WriteLine("Укажите Номер нарушителя: ");
            try
            {
                int id = int.Parse(Console.ReadLine());

                var allViolators = await _violatorService.GetAll();
                var violator = allViolators.Where(val => val.Id == id).FirstOrDefault();

                await _violatorService.Delete(violator);
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
