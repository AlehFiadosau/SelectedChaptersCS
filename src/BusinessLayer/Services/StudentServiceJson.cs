using AutoMapper;
using BusinessLayer.Core;
using BusinessLayer.DTO;
using BusinessLayer.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLayer.Services
{
    public class StudentServiceJson : IService
    {
        private readonly IRepository _studentRepository;

        public StudentServiceJson()
        {
            _studentRepository = new StudentRepositoryJson();
        }

        public void Create(IEnumerable<StudentDto> item, string path)
        {
            var studentsToWrite = new List<StudentToWrite>();

            foreach (var student in item)
            {
                var studentToWrite = new StudentToWrite
                {
                    FirstName = student.FirstName,
                    Surname = student.Surname,
                    Patronymic = student.Patronymic,
                    AverageMarks = AverageMarks.AverageMarksStudent(student),
                };

                studentsToWrite.Add(studentToWrite);
            }

            _studentRepository.Create(studentsToWrite, AverageMarks.AverageForGroup(item.ToList()), path);
        }

        public IEnumerable<StudentDto> GetAll(string path)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Student, StudentDto>()).CreateMapper();
            var students = mapper.Map<List<StudentDto>>(_studentRepository.GetAll(path));

            return students;
        }
    }
}