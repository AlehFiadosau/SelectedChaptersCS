using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DataAccessLayer.Repositories
{
    public class StudentRepositoryJson : IRepository
    {
        public void Create(IEnumerable<StudentToWriteDto> item, double averageGroup, string path)
        {
            using var writer = new StreamWriter(path, false, Encoding.UTF8);
            writer.Write(JsonConvert.SerializeObject(item));
        }

        public IEnumerable<StudentDto> GetAll(string path) => JsonConvert.DeserializeObject<IEnumerable<StudentDto>>(File.ReadAllText(path));
    }
}
