using Microsoft.EntityFrameworkCore;
using SchoolProSite.DAL.Context;
using SchoolProSite.DAL.Entities;
using SchoolProSite.DAL.Enums;
using SchoolProSite.DAL.Exceptions;
using SchoolProSite.DAL.Interfaces;
using System.Net.NetworkInformation;

namespace SchoolProSite.DAL.Dao
{
    public class DaoOfficeAssignment : IDaoOfficeAssignment
    {
        private readonly SchoolContext context;

        public DaoOfficeAssignment(SchoolContext context)
        {
            this.context = context;
        }
        public bool ExistsOfficeAssignment(Func<OfficeAssignment, bool> filter)
        {
            return this.context.OfficeAssignments.Any(filter);
        }

        public OfficeAssignment? GetOfficeAssignment(int Id)
        {
            return this.context.OfficeAssignments.Find(Id);
        }

        public List<OfficeAssignment> GetOfficeAssignments()
        {
            return this.context.OfficeAssignments.ToList();
        }

        public List<OfficeAssignment> GetOfficeAssignments(Func<OfficeAssignment, bool> filter)
        {
            return this.context.OfficeAssignments.Where(filter).ToList();
        }

        public void RemoveOfficeAssignment(OfficeAssignment OfficeAssignment)
        {
            OfficeAssignment OfficeAssignmentToRemove = this.GetOfficeAssignment(OfficeAssignment.InstructorId);

            this.context.OfficeAssignments.Update(OfficeAssignmentToRemove);

            this.context.SaveChanges();
        }
        public void SaveOfficeAssignment(OfficeAssignment OfficeAssignment)
        {

            string message = string.Empty;

            if (!IsOfficeAssignmentValid(OfficeAssignment, ref message, Operations.Save))
                throw new DaoOfficeAssignmentException(message);

            this.context.OfficeAssignments.Add(OfficeAssignment);
            this.context.SaveChanges();
        }

        public void UpdateOfficeAssignment(OfficeAssignment OfficeAssignment)
        {

            string message = string.Empty;

            if (!IsOfficeAssignmentValid(OfficeAssignment, ref message, Operations.Update))
                throw new DaoOfficeAssignmentException(message);


            OfficeAssignment OfficeAssignmentToUpdate = this.GetOfficeAssignment(OfficeAssignment.InstructorId);

            OfficeAssignmentToUpdate.Location = OfficeAssignment.Location;
            OfficeAssignmentToUpdate.Timestamp = OfficeAssignment.Timestamp;

            this.context.OfficeAssignments.Update(OfficeAssignmentToUpdate);
            this.context.SaveChanges();
        }

        private bool IsOfficeAssignmentValid(OfficeAssignment OfficeAssignment, ref string message, Operations operations)
        {
            bool result = false;

            if (string.IsNullOrEmpty(OfficeAssignment.Location))
            {
                message = "La localizacion es requerida.";
                return result;
            }

            if (OfficeAssignment.Location.Length > 50)
            {
                message = "La localizacion no puede ser mayor a 50 caracteres.";
                return result;
            }

            if (operations == Operations.Save)
            {
                if (this.ExistsOfficeAssignment(cd => cd.Location == OfficeAssignment.Location))
                {
                    message = "La localizacion ya se encuentra registrada";
                    return result;
                }

            }
            else
                result = true;

            return result;
        }
    }
}
