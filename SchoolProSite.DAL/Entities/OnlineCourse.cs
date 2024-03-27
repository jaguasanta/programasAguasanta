using System.ComponentModel.DataAnnotations;

namespace SchoolProSite.DAL.Entities
{
    public partial class OnlineCourse
    {
        [Key]
        public int CourseID{ get; set; }
        public string? URL { get; set; }

        public virtual Course Course { get; set; }
    }
}