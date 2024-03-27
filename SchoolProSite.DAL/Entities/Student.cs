using System.ComponentModel.DataAnnotations;
using SchoolProSite.DAL.Core;

namespace SchoolProSite.DAL.Entities
{
    public partial class Student : PersonBase
    {
        [Key]
        public int Id { get; set; }

        public DateTime EnrollmentDate { get; set; }

    }
}