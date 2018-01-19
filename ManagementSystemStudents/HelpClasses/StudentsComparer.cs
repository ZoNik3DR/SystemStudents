using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagementSystemStudents.ViewModels;

namespace ManagementSystemStudents.HelpClasses
{
    class StudentsComparer :IEqualityComparer<Student>
    {
        public bool Equals(Student x, Student y)
        {
            return x.Equals(y);
        }

        public int GetHashCode(Student obj)
        {
            return obj.GetHashCode();
        }
    }
}
