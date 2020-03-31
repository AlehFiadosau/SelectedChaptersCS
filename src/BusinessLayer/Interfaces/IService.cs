using BusinessLayer.DTO;
using System.Collections.Generic;

namespace BusinessLayer.Interfaces
{
    public interface IService
    {
        void Create(IEnumerable<StudentDto> item, string path);

        IEnumerable<StudentDto> GetAll(string path);
    }
}
