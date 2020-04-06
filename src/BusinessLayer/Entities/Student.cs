namespace BusinessLayer.Entities
{
    public class Student
    {
        public string FirstName { get; set; }

        public string Surname { get; set; }

        public string Patronymic { get; set; }

        public string[] Subjects { get; set; }

        public int[] Marks { get; set; }
    }
}
