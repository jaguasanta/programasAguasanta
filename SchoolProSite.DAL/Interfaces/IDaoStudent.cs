using SchoolProSite.DAL.Entities;

namespace SchoolProSite.DAL.Interfaces
{
    public interface IDaoStudent
    { 
    void SaveStudent(Student Student);
    void UpdateStudent(Student Student);
    void RemoveStudent(Student Student);
    Student GetStudent(int Id);
    List<Student> GetStudents();
    List<Student> GetStudents(Func<Student, bool> filter);
    bool ExistsStudent(Func<Student, bool> filter);
    }
}
