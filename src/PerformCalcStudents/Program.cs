using System.Collections.Generic;
using BusinessLayer.Entities;
using BusinessLayer.Services;
using CommandLine;
using PerformCalcStudents.CommandLineOptions;

namespace PerformCalcStudents
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            const string excelType = "Excel";
            const string jsonType = "Json";

        Parser.Default.ParseArguments<Options>(args)
               .WithParsed(o =>
               {
                   string path = o.InputFile;
                   var students = GetDataCSV(path);

                   if (o.FileType == excelType)
                   {
                       path = o.OutputFile + ".xlsx";
                       AverageSaveCSV(students, path);
                   }
                   else if (o.FileType == jsonType)
                   {
                       path = o.OutputFile + ".json";
                       AverageSaveJson(students, path);
                   }
               });
        }

        private static void AverageSaveCSV(IEnumerable<Student> students, string path)
        {
            var studentsServiceCSV = new StudentServiceExcel();
            studentsServiceCSV.Create(students, path);
        }

        private static void AverageSaveJson(IEnumerable<Student> students, string path)
        {
            var studentsServiceJson = new StudentServiceJson();
            studentsServiceJson.Create(students, path);
        }

        private static IEnumerable<Student> GetDataCSV(string path)
        {
            var studentsServiceCSV = new StudentServiceExcel();
            var students = studentsServiceCSV.GetAll(path);

            return students;
        }
    }
}
