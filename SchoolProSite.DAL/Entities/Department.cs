﻿using System.ComponentModel.DataAnnotations;
using SchoolProSite.DAL.Core;

namespace SchoolProSite.DAL.Entities
{
    public partial class Department : BaseEntity
    {
        
        [Key]
        public int DepartmentId { get; set; }
        public string? Name { get; set; }
        public decimal Budget { get; set; }
        public DateTime StartDate { get; set; }
        public int? Administrator { get; set; }
       
        public virtual ICollection<Course> Courses { get; set; }
    }
}