using CsvHelper;
using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;
using OfficeOpenXml;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace DataAccessLayer.Repositories
{
    public class StudentRepositoryExcel : IRepository
    {
        public void Create(IEnumerable<StudentToWriteDto> item, double averageGroup, string path)
        {
            const string worksheetName = "Students";
            const string averageGroupName = "Average group:";

            using (var excelPackage = new ExcelPackage())
            {
                var worksheet = excelPackage.Workbook.Worksheets.Add(worksheetName);

                worksheet.Cells["A1"].LoadFromCollection(item);
                worksheet.Cells["F1"].Value = averageGroupName;
                worksheet.Cells["F2"].Value = averageGroup;

                var fileForWrite = new FileInfo(path);
                excelPackage.SaveAs(fileForWrite);
            }
        }

        public IEnumerable<StudentDto> GetAll(string path)
        {
            List<StudentDto> students = new List<StudentDto>();
            const int numberHeadersForMarks = 3;

            try
            {
                using var reader = new StreamReader(path);
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

                csv.Read();
                csv.ReadHeader();
                var headers = csv.Context.HeaderRecord;

                while (csv.Read())
                {
                    var marks = GetMarks(headers.Length - numberHeadersForMarks, csv, headers);
                    students.Add(new StudentDto
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
            catch (MissingFieldException)
            {
                students = new List<StudentDto>();
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
