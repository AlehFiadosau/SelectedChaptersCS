using CsvHelper;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("BusinessLayer")]
namespace DataAccessLayer.Repositories
{
    internal class StudentRepositoryCSV : IRepository
    {

        public StudentRepositoryCSV()
        {
        }

        public void Create(IEnumerable<StudentToWrite> item, double averageGroup, string path)
        {
            string worksheetName = "Students";
            string averageGroupMark = "Average group mark:";

            using (var excelPackage = new ExcelPackage())
            {
                var worksheet = excelPackage.Workbook.Worksheets.Add(worksheetName);

                worksheet.Cells["A1"].LoadFromCollection(item);
                worksheet.Cells["F1"].Value = averageGroupMark;
                worksheet.Cells["F2"].Value = averageGroup;

                var fileForWrite = new FileInfo(path);
                excelPackage.SaveAs(fileForWrite);
            }
        }

        public IEnumerable<Student> GetAll(string path)
        {
            List<Student> students = new List<Student>();

            try
            {
                using var reader = new StreamReader(path);
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

                csv.Read();
                csv.ReadHeader();
                var headers = csv.Context.HeaderRecord;

                while (csv.Read())
                {
                    var marks = GetMarks(headers.Length - 3, csv, headers);
                    students.Add(new Student
                    {
                        FirstName = csv.GetField(headers[0]),
                        Surname = csv.GetField(headers[1]),
                        Patronymic = csv.GetField(headers[2]),
                        Subjects = headers.Skip(3).ToArray(),
                        Marks = marks,
                    });

                    marks = new int[headers.Length - 3];
                }
            }
            catch (CsvHelper.MissingFieldException ex)
            {
                Console.WriteLine(ex.Message);
                students = new List<Student>();
            }

            return students;
        }

        private int[] GetMarks(int size, CsvReader csv, string[] headers)
        {
            var marks = new int[size];

            for (int index = 0; index < marks.Length; index++)
            {
                if (csv.TryGetField(headers[index + 3], out marks[index]))
                {
                    marks[index] = csv.GetField<int>(headers[index + 3]);
                }
                else
                {
                    marks[index] = 0;
                }
            }

            return marks;
        }
    }
}
