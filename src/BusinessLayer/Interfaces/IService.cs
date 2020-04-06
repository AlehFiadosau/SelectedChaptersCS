using BusinessLayer.Entities;
using System.Collections.Generic;

namespace BusinessLayer.Interfaces
{
    public interface IService
    {
        void Create(IEnumerable<Student> item, string path);

        IEnumerable<Student> GetAll(string path);
    }
}
