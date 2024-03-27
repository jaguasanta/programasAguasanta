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
    public class DaoOnsiteCourse : IDaoOnsiteCourse
    {
        private readonly SchoolContext context;

        public DaoOnsiteCourse(SchoolContext context)
        {
            this.context = context;
        }
        public bool ExistsOnsiteCourse(Func<OnsiteCourse, bool> filter)
        {
            return this.context.OnsiteCourse.Any(filter);
        }

        public OnsiteCourseDaoModel GetOnsiteCourse(int Id)
        {
            OnsiteCourseDaoModel? onsiteCourseDaoModel = new OnsiteCourseDaoModel();
            try
            {

                onsiteCourseDaoModel = (from onsiteCourse in this.context.OnsiteCourse
                                        join course in this.context.Course on onsiteCourse.CourseID
                                                                            equals course.CourseID
                                        where onsiteCourse.CourseID == Id
                                        select new OnsiteCourseDaoModel()
                                        {
                                            Location = onsiteCourse.Location,
                                            Days = onsiteCourse.Days,
                                            Time = onsiteCourse.Time,
                                            CourseID = onsiteCourse.CourseID,
                                            CourseName = course.Title

                                        }).FirstOrDefault();

            }
            catch (Exception ex)
            {

                throw new DaoCourseException($"Error obteniendo el curso: (ex.Message)");
            }
            return onsiteCourseDaoModel;
        }

        public List<OnsiteCourseDaoModel> GetOnsiteCourses()
        {
            List<OnsiteCourseDaoModel>? onsiteCourseList = new List<OnsiteCourseDaoModel>();

            try
            {
                onsiteCourseList = (from onsiteCourse in this.context.OnsiteCourse
                                    join course in this.context.Course on onsiteCourse.CourseID
                                                                        equals course.CourseID
                                    select new OnsiteCourseDaoModel()
                                    {
                                        Location= onsiteCourse.Location,
                                        Days = onsiteCourse.Days,
                                        Time = onsiteCourse.Time,
                                        CourseID = onsiteCourse.CourseID,
                                        CourseName = course.Title

                                    }).ToList();

            }
            catch (Exception ex)
            {

                throw new DaoCourseException($"Error obteniendo el curso: (ex.Message)");
            }
            return onsiteCourseList;
        }

        public List<OnsiteCourseDaoModel> GetOnsiteCourses(Func<OnsiteCourse, bool> filter)
        {
            List<OnsiteCourseDaoModel>? onsiteCourseList = new List<OnsiteCourseDaoModel>();

            try
            {
                var onsiteCourses = this.context.OnsiteCourse.Where(filter);

                onsiteCourseList = (from onsiteCourse in onsiteCourses
                                    join course in this.context.Course on onsiteCourse.CourseID
                                                                        equals course.CourseID
                                    select new OnsiteCourseDaoModel()
                                    {
                                        Location = onsiteCourse.Location,
                                        Days = onsiteCourse.Days,
                                        Time = onsiteCourse.Time,
                                        CourseID = onsiteCourse.CourseID,
                                        CourseName = course.Title

                                    }).ToList();

            }
            catch (Exception ex)
            {

                throw new DaoCourseException($"Error obteniendo el curso: (ex.Message)");
            }
            return onsiteCourseList;
        }

        public void RemoveOnsiteCourse(OnsiteCourse OnsiteCourse)
        {
            OnsiteCourse? onsiteCourseToRemove = this.context.OnsiteCourse.Find(OnsiteCourse.CourseID);

            this.context.OnsiteCourse.Update(onsiteCourseToRemove);

            this.context.SaveChanges();
        }

        public void SaveOnsiteCourse(OnsiteCourse OnsiteCourse)
        {
            string message = string.Empty;

            if (!IsOnsiteCourseValid(OnsiteCourse, ref message, Operations.Save))
                throw new DaoOnsiteCourseException(message);

            this.context.OnsiteCourse.Add(OnsiteCourse);
            this.context.SaveChanges();
        }

        public void UpdateOnsiteCourse(OnsiteCourse OnsiteCourse)
        {
            string message = string.Empty;

            if (!IsOnsiteCourseValid(OnsiteCourse, ref message, Operations.Update))
                throw new DaoOnsiteCourseException(message);

            OnsiteCourse? onsiteCourseToUpdate = this.context.OnsiteCourse.Find(OnsiteCourse.CourseID);

            if (OnsiteCourse is null)
                throw new DaoCourseException("No se encontro el Curso Online especificado");

            //onsiteCourseToUpdate.CourseID = OnsiteCourse.CourseID;
            onsiteCourseToUpdate.Location = OnsiteCourse.Location;
            onsiteCourseToUpdate.Days = OnsiteCourse.Days;
            onsiteCourseToUpdate.Time = OnsiteCourse.Time;
            
            this.context.OnsiteCourse.Update(onsiteCourseToUpdate);
            this.context.SaveChanges();
        }

        private bool IsOnsiteCourseValid(OnsiteCourse OnsiteCourse, ref string message, Operations operations)
        {
            bool result = false;

            if (string.IsNullOrEmpty(OnsiteCourse.Location))
            {
                message = "La Localizaciob es requerido.";
                return result;
            }

            if (OnsiteCourse.Location.Length > 50)
            {
                message = "La localizacion no puede ser mayor a 50 caracteres.";
                return result;
            }

            if (operations == Operations.Save)
            {
                if (this.ExistsOnsiteCourse(cd => cd.CourseID == OnsiteCourse.CourseID))
                {
                    message = "El curso Onsite ya se encuentra registrado";
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
