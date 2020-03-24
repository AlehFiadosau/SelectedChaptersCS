using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;

namespace DataAccessLayer.Interfaces
{
    public interface IRepository
    {
        public void Create(IEnumerable<StudentToWrite> item, double averageGroup, string path);

        public IEnumerable<Student> GetAll(string path);
    }
}
