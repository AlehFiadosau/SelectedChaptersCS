using AutoMapper;
using BusinessLayer.Core;
using BusinessLayer.Entities;
using BusinessLayer.Interfaces;
using DataAccessLayer.DTO;
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

        public void Create(IEnumerable<Student> item, string path)
        {
            var studentsToWrite = new List<StudentToWriteDto>();

            foreach (var student in item)
            {
                var studentToWrite = new StudentToWriteDto
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

        public IEnumerable<Student> GetAll(string path)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<StudentDto, Student>()).CreateMapper();
            var students = mapper.Map<List<Student>>(_studentRepository.GetAll(path));

            return students;
        }
    }
}