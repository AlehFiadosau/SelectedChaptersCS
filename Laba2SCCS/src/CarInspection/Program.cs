using BusinessLayer.Configuration;
using BusinessLayer.Entities;
using BusinessLayer.Interfaces;
using CarInspection.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace CarInspection
{
    public static class Program
    {
        private static IServiceProvider _serviceProvider;
        private static IServiceCollectionForBll _serviceCollectionForBll;

        public static void Main(string[] args)
        {
            RegisterServices();

            Menu();

            DisposeServices();
            Console.WriteLine("Task executed");
            Console.ReadKey();
        }

        private static void RegisterServices()
        {
            var collection = new ServiceCollection();
            collection.AddTransient<IServiceCollectionForBll, ServiceCollectionForBll>();
            _serviceProvider = collection.BuildServiceProvider();

            string path = Path.Combine(Directory.GetCurrentDirectory() + @"\..\..\..\", "appsettings.json");
            string connectionName = "dbConnection";

            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile(path)
                .Build();

            _serviceCollectionForBll = _serviceProvider.GetRequiredService<IServiceCollectionForBll>();
            _serviceCollectionForBll.RegisterDependencies(config, collection, connectionName);
            _serviceProvider = collection.BuildServiceProvider();
        }

        private static void DisposeServices()
        {
            if (_serviceProvider != null)
            {
                if (_serviceProvider is IDisposable)
                {
                    ((IDisposable)_serviceProvider).Dispose();
                }
            }
        }

        private static void Driver()
        {
            var service = _serviceProvider.GetRequiredService<IService<Driver>>();
            var controller = new DriverController(service);
            controller.Start();
        }

        private static void Inspection()
        {
            var service = _serviceProvider.GetRequiredService<IService<Inspection>>();
            var controller = new InspectionController(service);
            controller.Start();
        }

        private static void Inspector()
        {
            var service = _serviceProvider.GetRequiredService<IService<Inspector>>();
            var controller = new InspectorController(service);
            controller.Start();
        }

        private static void Violation()
        {
            var service = _serviceProvider.GetRequiredService<IService<Violation>>();
            var controller = new ViolationController(service);
            controller.Start();
        }

        private static void Violator()
        {
            var service = _serviceProvider.GetRequiredService<IService<Violator>>();
            var controller = new ViolatorController(service);
            controller.Start();
        }

        public static void Menu()
        {
            var menu = new EasyConsole.Menu()
              .Add("Работа с Водителем", () => Driver())
              .Add("Работа с Инспектором", () => Inspection())
              .Add("Работа с Инспекцией", () => Inspector())
              .Add("Работа с Нарушением", () => Violation())
              .Add("Работа с Нарушителем", () => Violator());
            
            menu.Display();
        }
    }
}
