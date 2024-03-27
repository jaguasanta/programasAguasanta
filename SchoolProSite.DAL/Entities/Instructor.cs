using System.ComponentModel.DataAnnotations;
using SchoolProSite.DAL.Core;

namespace SchoolProSite.DAL.Entities
{
    public partial class Instructor : PersonBase
    {
        [Key]
        public int Id { get; set; }
        public DateTime HireDate { get; set; }
       
    }
}