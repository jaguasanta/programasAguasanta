using Microsoft.EntityFrameworkCore.ValueGeneration;
using SchoolProSite.DAL.Context;
using SchoolProSite.DAL.Entities;
using SchoolProSite.DAL.Enums;
using SchoolProSite.DAL.Exceptions;
using SchoolProSite.DAL.Interfaces;
using System.Net.NetworkInformation;

namespace SchoolProSite.DAL.Dao
{
    public class DaoPerson : IDaoPerson
    {
        private readonly SchoolContext context;

        public DaoPerson(SchoolContext context)
        {
            this.context = context;
        }
        public bool ExistsPerson(Func<Person, bool> filter)
        {
            return this.context.People.Any(filter);
        }

        public Person? GetPerson(int Id)
        {
            return this.context.People.Find(Id);   
        }

        public List<Person> GetPersons()
        {
            return this.context.People.ToList();
        }

        public List<Person> GetPersons(Func<Person, bool> filter)
        {
            return this.context.People.Where(filter).ToList();
        }

        public void RemovePerson(Person Person)
        {
            Person PersonToRemove = this.GetPerson(Person.PersonId);

            this.context.People.Update(PersonToRemove);

            this.context.SaveChanges();
        }

        public void SavePerson(Person Person)
        {

            string message = string.Empty;

            if (!IsPersonValid(Person, ref message, Operations.Save))
                throw new DaoPersonException(message);

            this.context.People.Add(Person);
            this.context.SaveChanges();
        }

        public void UpdatePerson(Person Person)
        {

            string message = string.Empty;

            if (!IsPersonValid(Person, ref message, Operations.Update))
                throw new DaoPersonException(message);


            Person PersonToUpdate = this.GetPerson(Person.PersonId);

            PersonToUpdate.LastName = Person.LastName;
            PersonToUpdate.FirstName = Person.FirstName;
            PersonToUpdate.HireDate = Person.HireDate;
            PersonToUpdate.EnrollmentDate = Person.EnrollmentDate;
            PersonToUpdate.Discriminator = Person.Discriminator;

            this.context.People.Update(PersonToUpdate);
            this.context.SaveChanges();
        }

        private bool IsPersonValid(Person Person, ref string message, Operations operations)
        {
            bool result = false;

            if (string.IsNullOrEmpty(Person.LastName))
            {
                message = "El apellido de la Persona es requerido.";
                return result;
            }

            if (Person.LastName.Length > 50)
            {
                message = "El apellido de la Persona no puede ser mayor a 50 caracteres.";
                return result;
            }

            if (operations == Operations.Save)
            {
                if (this.ExistsPerson(cd => cd.LastName == Person.LastName))
                {
                    message = "La Persona ya se encuentra registrada";
                    return result;
                }

            }
            else
                result = true;

            return result;
        }
    }
}