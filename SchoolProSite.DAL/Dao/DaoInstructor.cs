using Microsoft.EntityFrameworkCore;
using SchoolProSite.DAL.Context;
using SchoolProSite.DAL.Entities;
using SchoolProSite.DAL.Enums;
using SchoolProSite.DAL.Exceptions;
using SchoolProSite.DAL.Interfaces;
using System.Net.NetworkInformation;

namespace SchoolProSite.DAL.Dao
{
    public class DaoInstructor : IDaoInstructor
    {
        private readonly SchoolContext context;

        public DaoInstructor(SchoolContext context)
        {
            this.context = context;
        }
        public bool ExistsInstructor(Func<Instructor, bool> filter)
        {
            return this.context.Instructors.Any(filter);
        }

        public Instructor? GetInstructor(int Id)
        {
            return this.context.Instructors.Find(Id);
        }

        public List<Instructor> GetInstructors()
        {
            return this.context.Instructors
                               .OrderByDescending(instructor => instructor.CreationDate).ToList();
        }

        public List<Instructor> GetInstructors(Func<Instructor, bool> filter)
        {
            return this.context.Instructors.Where(filter).ToList();
        }

        public void RemoveInstructor(Instructor Instructor)
        {
            Instructor InstructorToRemove = this.GetInstructor(Instructor.Id);

            InstructorToRemove.Deleted = Instructor.Deleted;
            InstructorToRemove.DeletedDate = Instructor.DeletedDate;
            InstructorToRemove.UserDeleted = InstructorToRemove.UserDeleted;

            this.context.Instructors.Update(InstructorToRemove);

            this.context.SaveChanges();
        }

        public void SaveInstructor(Instructor Instructor)
        {

            string message = string.Empty;

            if (!IsInstructorValid(Instructor, ref message, Operations.Save))
                throw new DaoInstructorException(message);

            this.context.Instructors.Add(Instructor);
            this.context.SaveChanges();
        }

        public void UpdateInstructor(Instructor Instructor)
        {

            string message = string.Empty;

            if (!IsInstructorValid(Instructor, ref message, Operations.Update))
                throw new DaoInstructorException(message);


            Instructor InstructorToUpdate = this.GetInstructor(Instructor.Id);

            if (InstructorToUpdate is null)
                throw new DaoInstructorException(message);

            InstructorToUpdate.ModifyDate = Instructor.ModifyDate;
            InstructorToUpdate.LastName = Instructor.LastName;
            InstructorToUpdate.FirstName = Instructor.FirstName;
            InstructorToUpdate.HireDate = Instructor.HireDate;
            InstructorToUpdate.UserMod = Instructor.UserMod;

            this.context.Instructors.Update(InstructorToUpdate);
            this.context.SaveChanges();
        }

        private bool IsInstructorValid(Instructor Instructor, ref string message, Operations operations)
        {
            bool result = false;

            if (string.IsNullOrEmpty(Instructor.LastName))
            {
                message = "El apellido del instructor es requerido.";
                return result;
            }

            if (Instructor.LastName.Length > 50)
            {
                message = "El apellido del instructor no puede ser mayor a 50 caracteres.";
                return result;
            }

            if (operations == Operations.Save)
            {
                if (this.ExistsInstructor(cd => cd.LastName == Instructor.LastName))
                {
                    message = "El instructor ya encuentra registrado";
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
