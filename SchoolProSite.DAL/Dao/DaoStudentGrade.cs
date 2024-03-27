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
    public class DaoStudentGrade : IDaoStudentGrade
    {
        private readonly SchoolContext context;

        public DaoStudentGrade(SchoolContext context)
        {
            this.context = context;
        }
        public bool ExistsStudentGrade(Func<StudentGrade, bool> filter)
        {
            return this.context.StudentGrade.Any(filter);
        }

        public StudentGradeDaoModel GetStudentGrade(int Id)
        {
            StudentGradeDaoModel? studentGradeDaoModel = new StudentGradeDaoModel();
            try
            {

                studentGradeDaoModel = (from studentGrade in this.context.StudentGrade
                                        join course in this.context.Course on studentGrade.CourseID
                                                                            equals course.CourseID
                                        join student in this.context.Students on studentGrade.StudentID
                                                                            equals student.Id
                                        where studentGrade.EnrollmentID == Id
                                        select new StudentGradeDaoModel()
                                        {
                                            EnrollmentID=studentGrade.EnrollmentID,
                                            CourseID = studentGrade.CourseID,
                                            CourseName = course.Title,
                                            StudentID = studentGrade.StudentID,
                               StudentName = student.LastName + " - " + student.FirstName,
                                            Grade=studentGrade.Grade 

                                        }).FirstOrDefault();

            }
            catch (Exception ex)
            {

                throw new DaoStudentGradeException($"Error obteniendo el grado: (ex.Message)");
            }
            return studentGradeDaoModel;
        }

        public List<StudentGradeDaoModel> GetStudentGrades()
        {
            List<StudentGradeDaoModel> studentGradeList = new List<StudentGradeDaoModel>();
            try
            {

                studentGradeList = (from studentGrade in this.context.StudentGrade
                                   join course in this.context.Course on studentGrade.CourseID
                                                                      equals course.CourseID
                                   join student in this.context.Students on studentGrade.StudentID
                                                                         equals student.Id
                                   select new StudentGradeDaoModel()
                                   {
                                        EnrollmentID = studentGrade.EnrollmentID,
                                        CourseID = studentGrade.CourseID,
                                        CourseName = course.Title,
                                        StudentID = studentGrade.StudentID,
                                        StudentName = student.LastName + " - " + student.FirstName,
                                        Grade = studentGrade.Grade

                                   }).ToList();

            }
            catch (Exception ex)
            {
                throw new DaoStudentGradeException($"Error obteniendo el grado: (ex.Message)");
            }
            return studentGradeList;
        }

        public List<StudentGradeDaoModel> GetStudentGrades(Func<StudentGrade, bool> filter)
        {

            List<StudentGradeDaoModel> studentGradeList = new List<StudentGradeDaoModel>();

            var studentGrades = this.context.StudentGrade.Where(filter);

            try
            {

                studentGradeList = (from studentGrade in studentGrades
                                    join course in this.context.Course on studentGrade.CourseID
                                                                       equals course.CourseID
                                    join student in this.context.Students on studentGrade.StudentID
                                                                          equals student.Id

                                    select new StudentGradeDaoModel()
                                    {
                                        EnrollmentID = studentGrade.EnrollmentID,
                                        CourseID = studentGrade.CourseID,
                                        CourseName = course.Title,
                                        StudentID = studentGrade.StudentID,
                                        StudentName = student.LastName + " - " + student.FirstName,
                                        Grade = studentGrade.Grade

                                    }).ToList();

            }
            catch (Exception ex)
            {

                throw new DaoStudentGradeException($"Error obteniendo el grado: (ex.Message)");
            }
            return studentGradeList;

        }

        public void RemoveStudentGrade(StudentGrade StudentGrade)
        {
            //StudentGrade StudentGradeToRemove = this.GetStudentGrade(StudentGrade.EnrollmentID);

            StudentGrade? studentGradeToRemove = this.context.StudentGrade.Find(StudentGrade.EnrollmentID);

            this.context.StudentGrade.Update(studentGradeToRemove);

            this.context.SaveChanges();
        }

        public void SaveStudentGrade(StudentGrade StudentGrade)
        {
            string message = string.Empty;

            if (!IsStudentGradeValid(StudentGrade, ref message, Operations.Save))
                throw new DaoStudentGradeException(message);

            this.context.StudentGrade.Add(StudentGrade);
            this.context.SaveChanges();
        }

        public void UpdateStudentGrade(StudentGrade StudentGrade)
        {
            string message = string.Empty;

            if (!IsStudentGradeValid(StudentGrade, ref message, Operations.Update))
                throw new DaoStudentGradeException(message);

           StudentGrade? studentGradeToUpdate = this.context.StudentGrade.Find(StudentGrade.EnrollmentID);

            if (StudentGrade is null)
                throw new DaoStudentGradeException("No se encontro el grado especificado");

            studentGradeToUpdate.CourseID = StudentGrade.CourseID;
            studentGradeToUpdate.StudentID = StudentGrade.StudentID;
            studentGradeToUpdate.Grade = StudentGrade.Grade;

            this.context.StudentGrade.Update(studentGradeToUpdate);
            this.context.SaveChanges();
        }

        private bool IsStudentGradeValid(StudentGrade StudentGrade, ref string message, Operations operations)
        {
            bool result = false;

            if (StudentGrade.CourseID== 0)
            {
                message = "El curso no puede ser cero";
                return result;
            }

            if (operations == Operations.Save)
            {
                if (this.ExistsStudentGrade(cd => cd.StudentID == StudentGrade.StudentID)
                    && this.ExistsStudentGrade(cd => cd.CourseID == StudentGrade.CourseID))
                {
                    message = "El estudiante ya se encuentra registrado en el grado";
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
