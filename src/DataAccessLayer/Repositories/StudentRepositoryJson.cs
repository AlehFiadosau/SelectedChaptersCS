using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("BusinessLayer")]
namespace DataAccessLayer.Repositories
{
    internal class StudentRepositoryJson : IRepository
    {

        public StudentRepositoryJson()
        {
        }

        public void Create(IEnumerable<StudentToWrite> item, double averageGroup, string path)
        {
            using (var writer = new StreamWriter(path, false, Encoding.UTF8))
            {
                writer.Write(JsonConvert.SerializeObject(item));
            }
        }

        public IEnumerable<Student> GetAll(string path)
        {
            IEnumerable<Student> students = JsonConvert.DeserializeObject<List<Student>>(File.ReadAllText(path));

            return students;
        }
    }
}
