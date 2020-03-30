using AutoMapper;
using BusinessLayer.Core;
using BusinessLayer.DTO;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DataAccessLayer")]
namespace BusinessLayer.Services
{
    public class StudentServiceJson
    {
        private readonly IRepository _studentRepository;

        public StudentServiceJson()
        {
            _studentRepository = new StudentRepositoryJson();
        }

        public void Create(IEnumerable<StudentDto> item, string path)
        {
            var studentsToWrite = new List<StudentToWrite>();
            var averageForStudents = new AverageMarks();

            foreach (var student in item)
            {
                var studentToWrite = new StudentToWrite
                {
                    FirstName = student.FirstName,
                    Surname = student.Surname,
                    Patronymic = student.Patronymic,
                    AverageMarks = averageForStudents.AverageMarksStudent(student),
                };

                studentsToWrite.Add(studentToWrite);
            }

            _studentRepository.Create(studentsToWrite, averageForStudents.AverageForGroup(item.ToList()), path);
        }

        public IEnumerable<StudentDto> GetAll(string path)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Student, StudentDto>()).CreateMapper();
            var students = mapper.Map<List<StudentDto>>(_studentRepository.GetAll(path));

            return students;
        }
    }
}
