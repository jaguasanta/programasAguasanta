using Microsoft.EntityFrameworkCore.ValueGeneration;
using SchoolProSite.DAL.Context;
using SchoolProSite.DAL.Entities;
using SchoolProSite.DAL.Enums;
using SchoolProSite.DAL.Exceptions;
using SchoolProSite.DAL.Interfaces;
using System.Net.NetworkInformation;

namespace SchoolProSite.DAL.Dao
{
    public class DaoStudent : IDaoStudent
    {
        private readonly SchoolContext context;

        public DaoStudent(SchoolContext context)
        {
            this.context = context;
        }
        public bool ExistsStudent(Func<Student, bool> filter)
        {
            return this.context.Students.Any(filter);
        }

        public Student? GetStudent(int Id)
        {
            return this.context.Students.Find(Id);
        }

        public List<Student> GetStudents()
        {
            return this.context.Students
                               .OrderByDescending(student => student.CreationDate).ToList();
        }

        public List<Student> GetStudents(Func<Student, bool> filter)
        {
            return this.context.Students.Where(filter).ToList();
        }

        public void RemoveStudent(Student Student)
        {
            Student StudentToRemove = this.GetStudent(Student.Id);

            StudentToRemove.Deleted = Student.Deleted;
            StudentToRemove.DeletedDate = Student.DeletedDate;
            StudentToRemove.UserDeleted = StudentToRemove.UserDeleted;

            this.context.Students.Update(StudentToRemove);

            this.context.SaveChanges();
        }

        public void SaveStudent(Student Student)
        {

            string message = string.Empty;

            if (!IsStudentValid(Student, ref message, Operations.Save))
                throw new DaoStudentException1(message);

            this.context.Students.Add(Student);
            this.context.SaveChanges();
        }

        public void UpdateStudent(Student Student)
        {

            string message = string.Empty;

            if (!IsStudentValid(Student, ref message, Operations.Update))
                throw new DaoStudentException1(message);

            Student StudentToUpdate = this.GetStudent(Student.Id);

            if (StudentToUpdate is null)
                throw new DaoStudentException1(message);

            StudentToUpdate.ModifyDate = Student.ModifyDate;
            StudentToUpdate.LastName = Student.LastName;
            StudentToUpdate.FirstName = Student.FirstName;
            StudentToUpdate.EnrollmentDate = Student.EnrollmentDate;
            StudentToUpdate.UserMod =  Student.UserMod;
            StudentToUpdate.Id = Student.Id;

            this.context.Students.Update(StudentToUpdate);
            this.context.SaveChanges();
        }

        private bool IsStudentValid(Student Student, ref string message, Operations operations)
        {
            bool result = false;

            if (string.IsNullOrEmpty(Student.LastName))
            {
                message = "El apellido del estudiante es requerido.";
                return result;
            }

            if (Student.LastName.Length > 50)
            {
                message = "El apellido del estudiante no puede ser mayor a 50 caracteres.";
                return result;
            }

            if (operations == Operations.Save)
            {
                if (this.ExistsStudent(cd => cd.LastName == Student.LastName))
                {
                    message = "El estudiante ya se encuentra registrado";
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
