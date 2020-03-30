using System;
using System.IO;
using System.Collections.Generic;
using BusinessLayer.DTO;
using BusinessLayer.Services;
using CommandLine;
using PerformCalcStudents.CommandLineOptions;

namespace PerformCalcStudents
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
               .WithParsed(o =>
               {
                   string fileName = o.InputFile;
                   string path = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\", fileName));
                   Console.WriteLine(path);
                   var students = GetDataCSV(path);

                   if (o.FileType == "Excel")
                   {
                       fileName = o.OutputFile + ".csv";
                       path = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\", fileName));
                       AverageSaveCSV(students, path);
                   }
                   else if (o.FileType == "Json")
                   {
                       fileName = o.OutputFile + ".json";
                       path = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\", fileName));
                       AverageSaveJson(students, path);
                   }
               });

            Console.ReadKey();
        }

        static void AverageSaveCSV(IEnumerable<StudentDto> students, string path)
        {
            var studentsServiceCSV = new StudentServiceCSV();
            studentsServiceCSV.Create(students, path);
        }

        static void AverageSaveJson(IEnumerable<StudentDto> students, string path)
        {
            var studentsServiceJson = new StudentServiceJson();
            studentsServiceJson.Create(students, path);
        }

        static IEnumerable<StudentDto> GetDataCSV(string path)
        {
            var studentsServiceCSV = new StudentServiceCSV();
            var students = studentsServiceCSV.GetAll(path);

            return students;
        }
    }
}
