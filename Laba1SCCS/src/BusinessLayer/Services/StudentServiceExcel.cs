using DataAccessLayer.Interfaces;
using System.Collections.Generic;
using AutoMapper;
using System.Linq;
using DataAccessLayer.Repositories;
using BusinessLayer.Core;
using BusinessLayer.Interfaces;
using BusinessLayer.Entities;
using DataAccessLayer.DTO;

namespace BusinessLayer.Services
{
    public class StudentServiceExcel : IService
    {
        private readonly IRepository _studentRepository;

        public StudentServiceExcel()
        {
            _studentRepository = new StudentRepositoryExcel();
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
            var students = mapper.Map<IEnumerable<Student>>(_studentRepository.GetAll(path));

            return students;
        }
    }
}
