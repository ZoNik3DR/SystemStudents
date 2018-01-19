using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagementSystemStudents.ViewModels;

namespace ManagementSystemStudents.HelpClasses
{
    class GroupsComparer : IEqualityComparer<Group>
    {
        public bool Equals(Group x, Group y)
        {
            return x.Equals(y);
        }

        public int GetHashCode(Group obj)
        {
            return obj.GetHashCode();
        }
    }
}
