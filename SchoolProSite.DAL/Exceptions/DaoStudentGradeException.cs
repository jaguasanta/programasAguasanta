using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProSite.DAL.Exceptions
{
    public class DaoStudentGradeException : Exception
    {
        public DaoStudentGradeException(string message) : base(message)
        {
            // x logica para guardar el error.
        }
    }
}
