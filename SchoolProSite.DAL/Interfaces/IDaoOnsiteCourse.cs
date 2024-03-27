using SchoolProSite.DAL.Entities;
using SchoolProSite.DAL.Models;

namespace SchoolProSite.DAL.Interfaces
{
    public interface IDaoOnsiteCourse
    {
        void SaveOnsiteCourse(OnsiteCourse OnsiteCourse);
        void UpdateOnsiteCourse(OnsiteCourse OnsiteCourse);
        void RemoveOnsiteCourse(OnsiteCourse OnsiteCourse);
        OnsiteCourseDaoModel GetOnsiteCourse(int Id);
        List<OnsiteCourseDaoModel> GetOnsiteCourses();
        List<OnsiteCourseDaoModel> GetOnsiteCourses(Func<OnsiteCourse, bool> filter);
        bool ExistsOnsiteCourse(Func<OnsiteCourse, bool> filter);
    }
}
