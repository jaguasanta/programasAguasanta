using Microsoft.Build.Execution;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using SchoolProSite.DAL.Models;

namespace SchoolProSite.Web.Models
{
    public class OnsiteCourseModel
    {
        public OnsiteCourseModel()
        {

        }

        public OnsiteCourseModel(OnsiteCourseDaoModel onsiteCourseDaoModel)
        {
            this.CourseID = onsiteCourseDaoModel.CourseID;
            this.CourseName = onsiteCourseDaoModel.CourseName;
            this.Location = onsiteCourseDaoModel.Location;
            this.Days = onsiteCourseDaoModel.Days;
            this.Time = onsiteCourseDaoModel.Time;

        }

        public int CourseID { get; set; }

        public string? CourseName { get; set; }

        public string? Location { get; set; }

        public string? Days { get; set; }

        public DateTime Time { get; set; }

    }
}
