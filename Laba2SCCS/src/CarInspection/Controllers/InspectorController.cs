using BusinessLayer.Entities;
using BusinessLayer.Infrastructe;
using BusinessLayer.Interfaces;
using EasyConsole;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CarInspection.Controllers
{
    public class InspectorController
    {
        private readonly IService<Inspector> _inspectorService;

        public InspectorController(IService<Inspector> inspectorService)
        {
            _inspectorService = inspectorService;
        }

        public void Start()
        {
            Console.WriteLine("Инспектор");

            var menu = new Menu()
              .Add("Отобразить всех инспекторов", () => GetAllInspector().GetAwaiter().GetResult())
              .Add("Отобразить инспектора по номеру", () => GetByIdInspector().GetAwaiter().GetResult())
              .Add("Создание инспектора", () => CreateInspector().GetAwaiter().GetResult())
              .Add("Изменение инспектора", () => UpdateInspector().GetAwaiter().GetResult())
              .Add("Удаление инспектора", () => DeleteInspector().GetAwaiter().GetResult());

            menu.Display();
        }

        public async Task GetAllInspector()
        {
            try
            {
                var allInspectors = await _inspectorService.GetAll();

                Console.WriteLine("Все инспекторы");
                foreach (var inspector in allInspectors)
                {
                    Console.Write("Имя: ");
                    Output.WriteLine(ConsoleColor.Green, inspector.FirstName);
                    Console.Write("Фамилия: ");
                    Output.WriteLine(ConsoleColor.Green, inspector.Surname);
                    Console.Write("Отчество: ");
                    Output.WriteLine(ConsoleColor.Green, inspector.Patronic);
                    Console.Write("Телефон: ");
                    Output.WriteLine(ConsoleColor.Green, inspector.Phone);
                    Console.Write("Личный номер: ");
                    Output.WriteLine(ConsoleColor.Green, inspector.PersonalNumber.ToString());
                }
            }
            catch (NotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task GetByIdInspector()
        {
            Console.WriteLine("Инспектор по номеру");
            Console.Write("Укажите номер: ");

            try
            {
                var id = int.Parse(Console.ReadLine());
                var inspector = await _inspectorService.GetById(id);

                Console.Write("Имя: ");
                Output.WriteLine(ConsoleColor.Green, inspector.FirstName);
                Console.Write("Фамилия: ");
                Output.WriteLine(ConsoleColor.Green, inspector.Surname);
                Console.Write("Отчество: ");
                Output.WriteLine(ConsoleColor.Green, inspector.Patronic);
                Console.Write("Телефон: ");
                Output.WriteLine(ConsoleColor.Green, inspector.Phone);
                Console.Write("Персональный номер:");
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

        public async Task CreateInspector()
        {
            var inspector = new Inspector();

            Console.WriteLine("Создание инспектора");
            Console.WriteLine("Укажите Имя: ");
            try
            {
                inspector.FirstName = Console.ReadLine();
                Console.WriteLine("Укажите Фамилия: ");
                inspector.Surname = Console.ReadLine();
                Console.WriteLine("Укажите Отчество: ");
                inspector.Patronic = Console.ReadLine();
                Console.WriteLine("Укажите Телефон:");
                inspector.Phone = Console.ReadLine();
                Console.WriteLine("Укажите Личный номер:");
                inspector.PersonalNumber = long.Parse(Console.ReadLine());

                await _inspectorService.Create(inspector);
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

        public async Task UpdateInspector()
        {
            var inspector = new Inspector();

            Console.WriteLine("Изменение данных об инспекторе");
            Console.WriteLine("Укажите Номер инспектора: ");
            try
            {
                inspector.Id = int.Parse(Console.ReadLine());
                Console.WriteLine("Укажите Имя: ");
                inspector.FirstName = Console.ReadLine();
                Console.WriteLine("Укажите Фамилия: ");
                inspector.Surname = Console.ReadLine();
                Console.WriteLine("Укажите Отчество: ");
                inspector.Patronic = Console.ReadLine();
                Console.WriteLine("Укажите Телефон: ");
                inspector.Phone = Console.ReadLine();
                Console.WriteLine("Укажите Личный номер: ");
                inspector.PersonalNumber = int.Parse(Console.ReadLine());

                await _inspectorService.Update(inspector);
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

        public async Task DeleteInspector()
        {
            Console.WriteLine("Удаление инспектора");
            Console.WriteLine("Укажите Номер инспекции: ");
            try
            {
                int id = int.Parse(Console.ReadLine());

                var allInspectors = await _inspectorService.GetAll();
                var inspector = allInspectors.Where(val => val.Id == id).FirstOrDefault();

                await _inspectorService.Delete(inspector);
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
