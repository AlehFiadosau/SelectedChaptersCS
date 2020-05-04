using BusinessLayer.Entities;
using BusinessLayer.Infrastructe;
using BusinessLayer.Interfaces;
using EasyConsole;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CarInspection.Controllers
{
    public class InspectionController
    {
        private readonly IService<Inspection> _inspectionService;

        public InspectionController(IService<Inspection> inspectionService)
        {
            _inspectionService = inspectionService;
        }

        public void Start()
        {
            Console.WriteLine("Инспекция");

            var menu = new Menu()
              .Add("Отобразить все инспекции", () => GetAllInspection().GetAwaiter().GetResult())
              .Add("Отобразить инспекцию по номеру", () => GetByIdInspection().GetAwaiter().GetResult())
              .Add("Создание инспекции", () => CreateInspection().GetAwaiter().GetResult())
              .Add("Изменение инспекции", () => UpdateInspection().GetAwaiter().GetResult())
              .Add("Удаление инспекции", () => DeleteInspection().GetAwaiter().GetResult());

            menu.Display();
        }

        public async Task GetAllInspection()
        {
            try
            {
                var allInspections = await _inspectionService.GetAll();

                Console.WriteLine("Все инспекции");
                foreach (var inspection in allInspections)
                {
                    Console.Write("Название: ");
                    Output.WriteLine(ConsoleColor.Green, inspection.Name);
                    Console.Write("Описание: ");
                    Output.WriteLine(ConsoleColor.Green, inspection.Description);
                    Console.Write("Стоимость: ");
                    Output.WriteLine(ConsoleColor.Green, inspection.Price.ToString());
                    Console.Write("Дата инспекции (гг-мм-дд): ");
                    Output.WriteLine(ConsoleColor.Green, inspection.InspectionDate.ToString());
                }
            }
            catch (NotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task GetByIdInspection()
        {
            Console.WriteLine("Инпекция по номеру");
            Console.Write("Укажите номер: ");
            try
            {
                var id = int.Parse(Console.ReadLine());
                var inspection = await _inspectionService.GetById(id);

                Console.Write("Название: ");
                Output.WriteLine(ConsoleColor.Green, inspection.Name);
                Console.Write("Описание: ");
                Output.WriteLine(ConsoleColor.Green, inspection.Description);
                Console.Write("Стоимость: ");
                Output.WriteLine(ConsoleColor.Green, inspection.Price.ToString());
                Console.Write("Дата инспекции (гг-мм-дд):");
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

        public async Task CreateInspection()
        {
            var inspection = new Inspection();

            Console.WriteLine("Создание инспекции");
            Console.WriteLine("Укажите Название: ");
            try
            {
                inspection.Name = Console.ReadLine();
                Console.WriteLine("Укажите Описание: ");
                inspection.Description = Console.ReadLine();
                Console.WriteLine("Укажите Стоимость: ");
                inspection.Price = decimal.Parse(Console.ReadLine());
                Console.WriteLine("Укажите Дату инспекции (гг-мм-дд):");
                inspection.InspectionDate = DateTimeOffset.Parse(Console.ReadLine());
                Console.WriteLine("Укажите Номер водителя:");
                inspection.DriverId = int.Parse(Console.ReadLine());
                Console.WriteLine("Укажите Номер проверяющего:");
                inspection.InspectorId = int.Parse(Console.ReadLine());

                await _inspectionService.Create(inspection);
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

        public async Task UpdateInspection()
        {
            var inspection = new Inspection();

            Console.WriteLine("Изменение данных об инспекции");
            Console.WriteLine("Укажите Номер инспекции: ");
            try
            {
                inspection.Id = int.Parse(Console.ReadLine());
                Console.WriteLine("Укажите Название: ");
                inspection.Name = Console.ReadLine();
                Console.WriteLine("Укажите Описание: ");
                inspection.Description = Console.ReadLine();
                Console.WriteLine("Укажите Стоимсоть: ");
                inspection.Price = decimal.Parse(Console.ReadLine());
                Console.WriteLine("Укажите Дату инспекции (гг-мм-дд): ");
                inspection.InspectionDate = DateTimeOffset.Parse(Console.ReadLine());

                await _inspectionService.Update(inspection);
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

        public async Task DeleteInspection()
        {
            Console.WriteLine("Удаление инспекции");
            Console.WriteLine("Укажите Номер инспекции: ");
            try
            {
                int id = int.Parse(Console.ReadLine());

                var allInspections = await _inspectionService.GetAll();
                var inspection = allInspections.Where(val => val.Id == id).FirstOrDefault();

                await _inspectionService.Delete(inspection);
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
