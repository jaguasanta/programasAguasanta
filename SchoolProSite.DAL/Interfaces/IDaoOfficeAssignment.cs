using SchoolProSite.DAL.Entities;

namespace SchoolProSite.DAL.Interfaces
{
    public interface IDaoOfficeAssignment
    {
        void SaveOfficeAssignment(OfficeAssignment OfficeAssignment);
        void UpdateOfficeAssignment(OfficeAssignment OfficeAssignment);
        void RemoveOfficeAssignment(OfficeAssignment OfficeAssignment);
        OfficeAssignment GetOfficeAssignment(int Id);
        List<OfficeAssignment> GetOfficeAssignments();
        List<OfficeAssignment> GetOfficeAssignments(Func<OfficeAssignment, bool> filter);
        bool ExistsOfficeAssignment(Func<OfficeAssignment, bool> filter);
    }
}
