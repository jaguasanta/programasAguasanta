using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProSite.DAL.Models
{
    public class StudentGradeDaoModel
    {
        public int EnrollmentID { get; set; }

        public int CourseID { get; set; }

        public string? CourseName { get; set; }

        public int StudentID { get; set; }

        public string? StudentName { get; set; }

        public decimal? Grade { get; set; }
    }
}
