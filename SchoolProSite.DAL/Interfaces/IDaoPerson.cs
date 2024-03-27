using SchoolProSite.DAL.Entities;

namespace SchoolProSite.DAL.Interfaces
{
    public interface IDaoPerson
    {
        void SavePerson(Person Person);
        void UpdatePerson(Person Person);
        void RemovePerson(Person Person);
        Person? GetPerson(int Id);
        List<Person> GetPersons();
        List<Person> GetPersons(Func<Person, bool> filter);
        bool ExistsPerson(Func<Person, bool> filter);
    }
}
