using SchoolProSite.DAL.Entities;
using SchoolProSite.DAL.Models;

namespace SchoolProSite.DAL.Interfaces
{
    public interface IDaoStudentGrade
    {
        void SaveStudentGrade(StudentGrade StudentGrade);
        void UpdateStudentGrade(StudentGrade StudentGrade);
        void RemoveStudentGrade(StudentGrade StudentGrade);
        StudentGradeDaoModel GetStudentGrade(int Id);
        List<StudentGradeDaoModel> GetStudentGrades();
        List<StudentGradeDaoModel> GetStudentGrades(Func<StudentGrade, bool> filter);
        bool ExistsStudentGrade(Func<StudentGrade, bool> filter);
    }
}
