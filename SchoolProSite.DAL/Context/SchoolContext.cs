﻿using Microsoft.EntityFrameworkCore;
using SchoolProSite.DAL.Entities;

namespace SchoolProSite.DAL.Context
{
    public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
        {

        }
#region"Entities"
        public DbSet<Department> Departments { get; set; }
        public DbSet<Course> Course { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<OnlineCourse> OnlineCourse { get; set; }
        public DbSet<OnsiteCourse> OnsiteCourse { get; set; }
        public DbSet<OfficeAssignment> OfficeAssignments { get; set; }
        public DbSet<StudentGrade> StudentGrade { get; set; }
        #endregion
        
    }
}
