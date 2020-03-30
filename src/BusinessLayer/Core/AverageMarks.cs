using BusinessLayer.DTO;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLayer.Core
{
    public class AverageMarks
    {
        /// <summary>
        /// Calculating average marks student
        /// </summary>
        /// <param name="students"></param>
        /// <returns></returns>
        public double AverageMarksStudent(StudentDto student)
        {
            double averageScore = 0;
            averageScore = student.Marks.Average();

            return averageScore;
        }

        /// <summary>
        /// Calculating average marks group
        /// </summary>
        /// <param name="students"></param>
        /// <returns></returns>
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
