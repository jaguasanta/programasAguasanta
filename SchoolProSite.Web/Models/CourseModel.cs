using SchoolProSite.DAL.Models;

namespace SchoolProSite.Web.Models
{
    public class CourseModel
    {
        public CourseModel()
        {
            
        }

        public CourseModel(CourseDaoModel courseDaoModel)
        {
            this.CourseID = courseDaoModel.CourseID;
            this.DepartmentId = courseDaoModel.DepartmentId;
            this.DepartmentName = courseDaoModel.DepartmentName;
            this.CreationDate = courseDaoModel.CreationDate;
            this.Credits = courseDaoModel.Credits;
            this.Title = courseDaoModel.Title;  
        }
        public int CourseID { get; set; }

        public string? Title { get; set; }

        public int Credits { get; set; }

        public int DepartmentId { get; set; }

        public string? DepartmentName { get; set; }

        public DateTime CreationDate { get; set; }

    }
}
