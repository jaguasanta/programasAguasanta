using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProSite.DAL.Models
{
    public class CourseDaoModel
    {
        public int CourseID { get; set; }

        public string? Title { get; set; }

        public int Credits { get; set; }
        public int DepartmentId { get; set; }

        public string? DepartmentName { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
