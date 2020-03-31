using BusinessLayer.DTO;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLayer.Core
{
    public static class AverageMarks
    {
        /// <summary>
        /// Calculating average marks student
        /// </summary>
        /// <param name="students"></param>
        /// <returns></returns>
        public static double AverageMarksStudent(this StudentDto student) => student.Marks.Average();

        /// <summary>
        /// Calculating average marks group
        /// </summary>
        /// <param name="students"></param>
        /// <returns></returns>
        public static double AverageForGroup(this IEnumerable<StudentDto> students)
            => students.Select(s => s.AverageMarksStudent()).Average();
    }
}
