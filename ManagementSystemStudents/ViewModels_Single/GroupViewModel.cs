using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagementSystemStudents.ViewModels;
using System.Collections.ObjectModel;

namespace ManagementSystemStudents.ViewModels
{
    public class Lecture : ViewModelBase, IEquatable<Lecture>
    {
        private string subjectname;
        public string SubjectName
        {
            get { return subjectname; }
            set
            {
                subjectname = value;
                OnPropertyChanged("SubjectName");
            }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public string NameAndSubject => name + "  " + subjectname;

        public override string ToString()
        {
            return NameAndSubject + "\n";
        }

        public bool Equals(Lecture other)
        {
            return name==other.name && subjectname == other.subjectname;
        }

        public Lecture(string name, string subjectname)
        {
            this.Name = name;
            this.SubjectName = subjectname;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ name.GetHashCode() ^ subjectname.GetHashCode();
        }


    }


    public class Term : ViewModelBase
    {
        private int num;
        private ObservableCollection<Lecture> lectures;
        public ObservableCollection<Lecture> Lectures => lectures;

        public Term(int num)
        {
            lectures = new ObservableCollection<Lecture>();
            this.num = num;
        }

        public void Add(Lecture t)
        {
            lectures.Add(t);
            OnPropertyChanged("Lectures");
        }

        public override string ToString() => num.ToString();

    }

    public class Group : ViewModelBase, IEquatable<Group>
    {
        //fiels & properties
        private string groupnum;
        public string GroupNum
        {
            get { return groupnum; }
            set
            {
                groupnum = value;
                OnPropertyChanged("GroupNum");
            }
        }

        public bool isdisbanded;
        public bool IsDisbanded
        {
            get { return isdisbanded; }
            set
            {
                isdisbanded = value;
                OnPropertyChanged("IsDisbanded");
            }
        }

        private List<Term> terms;
        public List<Term> Terms
        {
            get { return terms; }
            set
            {
                terms = value;
                OnPropertyChanged("Terms");
            }
        }

        //ctors
        public Group() : this("Undefined") { }

        public Group(string GroupNum)
        {
            this.GroupNum = GroupNum;
            isdisbanded = false;
            terms = new List<Term>(8);
            for (int i = 0, j = 0; i < 8; ++i)
                terms.Add(new Term(++j));
        }

        public Group(string groupnum, bool isdisbanded, List<Term> terms)
        {
            GroupNum = groupnum;
            IsDisbanded = isdisbanded;
            Terms = terms;
        }


        public bool Equals(Group other)
        {
            return GroupNum == other.GroupNum;
        }

        public override int GetHashCode()
        {
            return groupnum.GetHashCode() ^ base.GetHashCode();
        }

    }
}
