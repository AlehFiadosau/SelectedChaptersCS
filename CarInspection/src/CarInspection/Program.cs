using BusinessLayer.Entities;
using BusinessLayer.Interfaces;
using CarInspection.Controllers;
using EasyConsole;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace CarInspection
{
    public static class Program
    {
        private static IServiceProvider _serviceProvider;

        public static void Main(string[] args)
        {
            RegisterServices();

            StartUserSession();

            DisposeServices();
            Console.ReadKey();
        }

        private static void RegisterServices()
        {
            var serviceCollection = new ServiceCollection();
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory() + @"\..\..\..\")
                .AddJsonFile("appsettings.json", false);
            var config = configBuilder.Build();

            string connectionName = "dbConnection";

            Startup.ConfigureServices(config, serviceCollection, connectionName);
            _serviceProvider = serviceCollection.BuildServiceProvider();
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

        private static void WorkWithDriver()
        {
            var service = _serviceProvider.GetRequiredService<IService<Driver, int>>();
            var controller = new DriversController(service);
            controller.Start();
        }

        private static void WorkWithInspection()
        {
            var service = _serviceProvider.GetRequiredService<IService<Inspection, int>>();
            var controller = new InspectionsController(service);
            controller.Start();
        }

        private static void WorkWithInspector()
        {
            var service = _serviceProvider.GetRequiredService<IService<Inspector, int>>();
            var controller = new InspectorsController(service);
            controller.Start();
        }

        private static void WorkWithViolation()
        {
            var service = _serviceProvider.GetRequiredService<IService<Violation, int>>();
            var controller = new ViolationsController(service);
            controller.Start();
        }

        private static void WorkWithViolator()
        {
            var service = _serviceProvider.GetRequiredService<IService<Violator, int>>();
            var controller = new ViolatorsController(service);
            controller.Start();
        }

        public static void StartUserSession()
        {
            var menu = new Menu()
              .Add("Work with Driver", () => WorkWithDriver())
              .Add("Work with Inspector", () => WorkWithInspection())
              .Add("Work with Inspection", () => WorkWithInspector())
              .Add("Work with Violation", () => WorkWithViolation())
              .Add("Work with Violator", () => WorkWithViolator());
            
            menu.Display();
        }
    }
}
