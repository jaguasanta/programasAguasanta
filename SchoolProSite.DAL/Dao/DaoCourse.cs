using Microsoft.EntityFrameworkCore;
using SchoolProSite.DAL.Context;
using SchoolProSite.DAL.Entities;
using SchoolProSite.DAL.Enums;
using SchoolProSite.DAL.Exceptions;
using SchoolProSite.DAL.Interfaces;
using SchoolProSite.DAL.Models;
using System.Net.NetworkInformation;

namespace SchoolProSite.DAL.Dao
{
    public class DaoCourse : IDaoCourse
    {
        private readonly SchoolContext context;

        public DaoCourse(SchoolContext context)
        {
            this.context = context;
        }
        public bool ExistsCourse(Func<Course, bool> filter)
        {
            return this.context.Course.Any(filter);
        }

        public CourseDaoModel GetCourse(int Id)
        {
            CourseDaoModel? courseDaoModel = new CourseDaoModel();
            try
            {

                courseDaoModel = (from course in this.context.Course
                                  join depto in this.context.Departments on course.DepartmentId
                                                                      equals depto.DepartmentId
                                  where course.Deleted == false
                                  && course.CourseID == Id
                                  select new CourseDaoModel()
                                  {
                                      CourseID = course.CourseID,
                                      CreationDate = course.CreationDate,
                                      Credits = course.Credits,
                                      DepartmentId = course.DepartmentId,
                                      DepartmentName = depto.Name,
                                      Title = course.Title
                                  }).FirstOrDefault();

            }
            catch (Exception ex)
            {

                throw new DaoCourseException($"Error obteniendo el curso: (ex.Message)");
            }
            return courseDaoModel;
        }

        public List<CourseDaoModel> GetCourses()
        {
            List<CourseDaoModel>? courseList = new List<CourseDaoModel>();

            try
            {
                    courseList = (from course in this.context.Course
                                  join depto in this.context.Departments on course.DepartmentId
                                                                      equals depto.DepartmentId
                                  where course.Deleted == false
                                  orderby course.CreationDate descending
                                  select new CourseDaoModel()
                                  {
                                      CourseID = course.CourseID,
                                      CreationDate = course.CreationDate,
                                      Credits = course.Credits,
                                      DepartmentId = course.DepartmentId,
                                      DepartmentName = depto.Name,
                                      Title = course.Title
                                  }).ToList();

            }
            catch (Exception ex)
            {

                throw new DaoCourseException($"Error obteniendo el curso: (ex.Message)");
            }
            return courseList;
        }

        public List<CourseDaoModel> GetCourses(Func<Course, bool> filter)
        {
            List<CourseDaoModel>? courseList = new List<CourseDaoModel>();
            try
            {
                var courses = this.context.Course.Where(filter);

                courseList = (from course in courses
                              join depto in this.context.Departments on course.DepartmentId
                                                                  equals depto.DepartmentId
                              where course.Deleted == false
                              select new CourseDaoModel()
                              {
                                  CourseID = course.CourseID,
                                  CreationDate = course.CreationDate,
                                  Credits = course.Credits,
                                  DepartmentId = course.DepartmentId,
                                  DepartmentName = depto.Name,
                                  Title = course.Title
                              }).ToList();

            }
            catch (Exception ex)
            {

                throw new DaoCourseException($"Error obteniendo el curso: (ex.Message)");
            }
            return courseList;
        }

        public void RemoveCourse(Course Course)
        {
            Course? CourseToRemove = this.context.Course.Find(Course.CourseID);

            CourseToRemove.Deleted = Course.Deleted;
            CourseToRemove.DeletedDate = Course.DeletedDate;
            CourseToRemove.UserDeleted = CourseToRemove.UserDeleted;

            this.context.Course.Update(CourseToRemove);

            this.context.SaveChanges();
        }

        public void SaveCourse(Course Course)
        {
            string message = string.Empty;

            if (!IsCourseValid(Course, ref message, Operations.Save))
                throw new DaoCourseException(message);

            this.context.Course.Add(Course);
            this.context.SaveChanges();
        }

        public void UpdateCourse(Course Course)
        {
            string message = string.Empty;

            if (!IsCourseValid(Course, ref message, Operations.Update))
                throw new DaoCourseException(message);

            Course? courseToUpdate = this.context.Course.Find(Course.CourseID);

            if (Course is null)
                throw new DaoCourseException("No se encontro el curso especificado");
            
            courseToUpdate.ModifyDate = DateTime.Now;
            courseToUpdate.Title = Course.Title;
            courseToUpdate.Credits = Course.Credits;
            courseToUpdate.DepartmentId = Course.DepartmentId;
            courseToUpdate.UserMod = Course.UserMod;

            this.context.Course.Update(courseToUpdate);
            this.context.SaveChanges();
        }

        private bool IsCourseValid(Course Course, ref string message, Operations operations)
        {
            bool result = false;

            if (string.IsNullOrEmpty(Course.Title))
            {
                message = "El nombre del curso es requerido.";
                return result;
            }

            if (Course.Title.Length > 100)
            {
                message = "El nombre del curso no puede ser mayor a 100 caracteres.";
                return result;
            }

            if (Course.Credits == 0)
            {
                message = "El credito no puede ser cero(0).";
                return result;
            }

            if (operations == Operations.Save)
            {
                if (this.ExistsCourse(cd => cd.Title == Course.Title))
                {
                    message = "El curso ya se encuentra registrado";
                    return result;
                }
                else
                {
                    result = true;
                }
            }
            else
                result = true;

            return result;
        }
    }
}
