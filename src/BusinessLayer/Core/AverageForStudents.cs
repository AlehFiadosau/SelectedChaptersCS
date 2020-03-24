using BusinessLayer.DTO;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLayer.Core
{
    public class AverageForStudents
    {
        public double AverageScoreStudent(StudentDto student)
        {
            double averageScore = 0;
            averageScore = student.Marks.Average();

            return averageScore;
        }

        public IEnumerable<double> AverageScoreSubject(List<StudentDto> students)
        {
            var results = new List<double>();
            var subjectMarks = new List<int>();

            double averageScore = 0;

            for (int row = 0; row < students.Count; row++)
            {
                for (int column = 0; column < students[row].Marks.Length; column++)
                {
                    subjectMarks.Add(students[column].Marks[row]);
                }

                averageScore = subjectMarks.Average();
                results.Add(averageScore);
                subjectMarks.Clear();
            }

            return results;
        }

        public double AverageForGroup(List<StudentDto> students)
        {
            var averageforGroup = new List<double>();

            foreach (var student in students)
            {
                averageforGroup.Add(student.Marks.Average());
            }

            return averageforGroup.Average();
        }
    }
}
