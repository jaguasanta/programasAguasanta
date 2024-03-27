using Microsoft.EntityFrameworkCore;
using SchoolProSite.DAL.Context;
using SchoolProSite.DAL.Entities;
using SchoolProSite.DAL.Enums;
using SchoolProSite.DAL.Exceptions;
using SchoolProSite.DAL.Interfaces;
using SchoolProSite.DAL.Models;
using System.Linq;
using System.Net.NetworkInformation;

namespace SchoolProSite.DAL.Dao
{
    public class DaoOnlineCourse : IDaoOnlineCourse
    {
        private readonly SchoolContext context;

        public DaoOnlineCourse(SchoolContext context)
        {
            this.context = context;
        }
        public bool ExistsOnlineCourse(Func<OnlineCourse, bool> filter)
        {
            return this.context.OnlineCourse.Any(filter);
        }

        public OnlineCourseDaoModel GetOnlineCourse(int Id)
        {
            OnlineCourseDaoModel? onlineCourseDaoModel = new OnlineCourseDaoModel();
            try
            {

                onlineCourseDaoModel = (from onlineCourse in this.context.OnlineCourse
                                  join course in this.context.Course on onlineCourse.CourseID
                                                                      equals course.CourseID
                                  where onlineCourse.CourseID == Id
                                  select new OnlineCourseDaoModel()
                                  {
                                      URL = onlineCourse.URL,
                                      CourseID = onlineCourse.CourseID,
                                      CourseName = course.Title
                                      
                                  }).FirstOrDefault();

            }
            catch (Exception ex)
            {

                throw new DaoCourseException($"Error obteniendo el curso: (ex.Message)");
            }
            return onlineCourseDaoModel;
        }

        public List<OnlineCourseDaoModel> GetOnlineCourses()
        {
            List<OnlineCourseDaoModel>? onlineCourseList = new List<OnlineCourseDaoModel>();

            try
            {

                onlineCourseList = (from onlineCourse in this.context.OnlineCourse
                                        join course in this.context.Course on onlineCourse.CourseID
                                                                            equals course.CourseID
                                        select new OnlineCourseDaoModel()
                                        {
                                            URL = onlineCourse.URL,
                                            CourseID = onlineCourse.CourseID,
                                            CourseName = course.Title

                                        }).ToList();

            }
            catch (Exception ex)
            {

                throw new DaoCourseException($"Error obteniendo el curso: (ex.Message)");
            }
            return onlineCourseList;
        }

        public List<OnlineCourseDaoModel> GetOnlineCourses(Func<OnlineCourse, bool> filter)
        {
            List<OnlineCourseDaoModel>? onlineCourseList = new List<OnlineCourseDaoModel>();

            try
            {
                var onlineCourses = this.context.OnlineCourse.Where(filter);

                onlineCourseList = (from onlineCourse in onlineCourses
                                    join course in this.context.Course on onlineCourse.CourseID
                                                                        equals course.CourseID
                                    select new OnlineCourseDaoModel()
                                    {
                                        URL = onlineCourse.URL,
                                        CourseID = onlineCourse.CourseID,
                                        CourseName = course.Title

                                    }).ToList();

            }
            catch (Exception ex)
            {

                throw new DaoCourseException($"Error obteniendo el curso: (ex.Message)");
            }
            return onlineCourseList;
        }

        public void RemoveOnlineCourse(OnlineCourse OnlineCourse)
        {
            OnlineCourse? onlineCourseToRemove = this.context.OnlineCourse.Find(OnlineCourse.CourseID);

            this.context.OnlineCourse.Update(onlineCourseToRemove);

            this.context.SaveChanges();
        }

        public void SaveOnlineCourse(OnlineCourse OnlineCourse)
        {
            string message = string.Empty;

            if (!IsOnlineCourseValid(OnlineCourse, ref message, Operations.Save))
                throw new DaoOnlineCourseException(message);

            this.context.OnlineCourse.Add(OnlineCourse);
            this.context.SaveChanges();
        }

        public void UpdateOnlineCourse(OnlineCourse OnlineCourse)
        {
            string message = string.Empty;

            if (!IsOnlineCourseValid(OnlineCourse, ref message, Operations.Update))
                throw new DaoOnlineCourseException(message);

            OnlineCourse? onlineCourseToUpdate = this.context.OnlineCourse.Find(OnlineCourse.CourseID);

            if (OnlineCourse is null)
                throw new DaoCourseException("No se encontro el Curso Online especificado");

            //onlineCourseToUpdate.CourseID = OnlineCourse.CourseID;
            onlineCourseToUpdate.URL = OnlineCourse.URL;

            this.context.OnlineCourse.Update(onlineCourseToUpdate);
            this.context.SaveChanges();
        }

        private bool IsOnlineCourseValid(OnlineCourse OnlineCourse, ref string message, Operations operations)
        {
            bool result = false;

            if (string.IsNullOrEmpty(OnlineCourse.URL))
            {
                message = "URL es requerida.";
                return result;
            }

            if (OnlineCourse.URL.Length > 100)
            {
                message = "La URL no puede ser mayor a 100 caracteres.";
                return result;
            }

            if (operations == Operations.Save)
            {
                if (this.ExistsOnlineCourse(cd => cd.URL == OnlineCourse.URL))
                {
                    message = "El curso Online La localizacion ya se encuentra registrado";
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
