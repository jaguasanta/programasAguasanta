using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProSite.DAL.Exceptions
{
    public class DaoOnlineCourseException : Exception
    {
        public DaoOnlineCourseException(string message) : base(message)
        {
            // x logica para guardar el error.
        }
    }
}
