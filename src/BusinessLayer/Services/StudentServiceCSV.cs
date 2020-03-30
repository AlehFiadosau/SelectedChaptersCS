using BusinessLayer.DTO;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Entities;
using System.Collections.Generic;
using AutoMapper;
using System.Linq;
using DataAccessLayer.Repositories;
using System.Runtime.CompilerServices;
using BusinessLayer.Core;

[assembly: InternalsVisibleTo("DataAccessLayer")]
namespace BusinessLayer.Services
{
    public class StudentServiceCSV
    {
        private readonly IRepository _studentRepository;

        public StudentServiceCSV()
        {
            _studentRepository = new StudentRepositoryCSV();
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
            var students = mapper.Map<IEnumerable<StudentDto>>(_studentRepository.GetAll(path));

            return students;
        }
    }
}
