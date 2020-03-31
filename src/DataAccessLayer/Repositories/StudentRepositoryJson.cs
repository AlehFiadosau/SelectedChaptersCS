using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DataAccessLayer.Repositories
{
    public class StudentRepositoryJson : IRepository
    {
        public void Create(IEnumerable<StudentToWrite> item, double averageGroup, string path)
        {
            using var writer = new StreamWriter(path, false, Encoding.UTF8);
            writer.Write(JsonConvert.SerializeObject(item));
        }

        public IEnumerable<Student> GetAll(string path) => JsonConvert.DeserializeObject<IEnumerable<Student>>(File.ReadAllText(path));
    }
}
