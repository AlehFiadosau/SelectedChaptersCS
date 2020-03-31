using System;
using System.Collections.Generic;
using BusinessLayer.DTO;
using BusinessLayer.Services;
using CommandLine;
using PerformCalcStudents.CommandLineOptions;

namespace PerformCalcStudents
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
               .WithParsed(o =>
               {
                   string path = o.InputFile;
                   var students = GetDataCSV(path);

                   if (o.FileType == "Excel")
                   {
                       path = o.OutputFile + ".xlsx";
                       AverageSaveCSV(students, path);
                   }
                   else if (o.FileType == "Json")
                   {
                       path = o.OutputFile + ".json";
                       AverageSaveJson(students, path);
                   }
               });

            Console.ReadKey();
        }

        private static void AverageSaveCSV(IEnumerable<StudentDto> students, string path)
        {
            var studentsServiceCSV = new StudentServiceCSV();
            studentsServiceCSV.Create(students, path);
        }

        private static void AverageSaveJson(IEnumerable<StudentDto> students, string path)
        {
            var studentsServiceJson = new StudentServiceJson();
            studentsServiceJson.Create(students, path);
        }

        private static IEnumerable<StudentDto> GetDataCSV(string path)
        {
            var studentsServiceCSV = new StudentServiceCSV();
            var students = studentsServiceCSV.GetAll(path);

            return students;
        }
    }
}
