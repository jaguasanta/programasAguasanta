using SchoolProSite.DAL.Entities;
using SchoolProSite.DAL.Models;

namespace SchoolProSite.DAL.Interfaces
{
    public interface IDaoOnlineCourse
    {
        void SaveOnlineCourse(OnlineCourse OnlineCourse);
        void UpdateOnlineCourse(OnlineCourse OnlineCourse);
        void RemoveOnlineCourse(OnlineCourse OnlineCourse);
        OnlineCourseDaoModel GetOnlineCourse(int Id);
        List<OnlineCourseDaoModel> GetOnlineCourses();
        List<OnlineCourseDaoModel> GetOnlineCourses(Func<OnlineCourse, bool> filter);
        bool ExistsOnlineCourse(Func<OnlineCourse, bool> filter);
    }
}
