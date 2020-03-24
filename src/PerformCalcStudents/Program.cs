using BusinessLayer.Services;
using BusinessLayer.DTO;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace PerformCalcStudents
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName = "1_TestFile.csv";
            string path = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\", fileName));
            var students = GetDataCSV(path);
            
            fileName = "result.csv";
            path = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\", fileName));
            AverageSaveCSV(students, path);

            fileName = "result.json";
            path = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\", fileName));
            AverageSaveJson(students, path);

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
            var students = studentsServiceCSV.GetAll(path).ToList();

            for (int index = 0; index < students.Count; index++)
            {
                Console.WriteLine("Имя учащегося: ");
                Console.Write(students[index].FirstName + '\n');
                Console.WriteLine("Фамилия учащегося: ");
                Console.Write(students[index].Surname + '\n');
                Console.WriteLine("Отчество учащегося: ");
                Console.Write(students[index].Patronymic + '\n');
                Console.WriteLine("Оценки:");

                for (int mark = 0; mark < students[index].Marks.Length; mark++)
                {
                    Console.WriteLine($"Дисциплина: {students[index].Subjects[mark]}");
                    Console.WriteLine(students[index].Marks[mark]);
                }

                Console.WriteLine("Средняя оценка студента:");
            }

            return students;
        }
    }
}
