using SchoolProSite.DAL.Models;

namespace SchoolProSite.Web.Models
{
    public class OnlineCourseModel
    {
        public OnlineCourseModel()
        {

        }

        public OnlineCourseModel(OnlineCourseDaoModel onlineCourseDaoModel)
        {
            this.CourseID = onlineCourseDaoModel.CourseID;
            this.CourseName = onlineCourseDaoModel.CourseName;
            this.URL = onlineCourseDaoModel.URL;
            
        }

        public int CourseID { get; set; }

        public string? CourseName { get; set; }

        public string? URL { get; set; }
    }
}
