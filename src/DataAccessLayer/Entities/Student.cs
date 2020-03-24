using CsvHelper.Configuration.Attributes;

namespace DataAccessLayer.Entities
{
    public class Student
    {
        [Name("FirstName")]
        public string FirstName { get; set; }

        [Name("Surname")]
        public string Surname { get; set; }

        [Name("Patronymic")]
        public string Patronymic { get; set; }

        [Name("Subjects")]
        public string[] Subjects { get; set; }

        [Name("Marks")]
        public int [] Marks { get; set; }
    }
}
