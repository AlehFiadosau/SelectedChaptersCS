using DataAccessLayer.DTO;
using System.Collections.Generic;

namespace DataAccessLayer.Interfaces
{
    public interface IRepository
    {
        void Create(IEnumerable<StudentToWriteDto> item, double averageGroup, string path);

        IEnumerable<StudentDto> GetAll(string path);
    }
}
