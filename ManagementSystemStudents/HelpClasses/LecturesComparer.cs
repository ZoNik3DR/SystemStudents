using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagementSystemStudents.ViewModels;

namespace ManagementSystemStudents.HelpClasses
{
    class LecturesComparer :IEqualityComparer<Lecture>
    {
        public bool Equals(Lecture x, Lecture y)
        {
            return x.Equals(y);
        }

        public int GetHashCode(Lecture obj)
        {
            return obj.GetHashCode();
        }
    }
}

