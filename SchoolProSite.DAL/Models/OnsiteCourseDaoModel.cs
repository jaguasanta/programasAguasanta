using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProSite.DAL.Models
{
    public  class OnsiteCourseDaoModel
    {
        public int CourseID { get; set; }

        public string? CourseName { get; set; }

        public string? Location { get; set; }

        public string? Days { get; set; }

        public DateTime Time { get; set; }
    }
}
