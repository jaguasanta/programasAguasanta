using SchoolProSite.DAL.Models;

namespace SchoolProSite.Web.Models
{
    public class StudentGradeModel
    {
        public StudentGradeModel()
        {
            
        }
        public StudentGradeModel(StudentGradeDaoModel studentGradeDaoModel)
        {

            this.EnrollmentID=studentGradeDaoModel.EnrollmentID;
            this.CourseID=studentGradeDaoModel.CourseID;
            this.CourseName=studentGradeDaoModel.CourseName;
            this.StudentID=studentGradeDaoModel.StudentID;
            this.StudentName=studentGradeDaoModel.StudentName;
            this.Grade=studentGradeDaoModel.Grade;
            
        }

        public int EnrollmentID { get; set; }

        public int CourseID { get; set; }

        public string? CourseName { get; set; }

        public int StudentID { get; set; }

        public string? StudentName { get; set; }

        public decimal? Grade { get; set; }

    }
}
