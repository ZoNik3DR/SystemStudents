using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagementSystemStudents.ViewModels;
using System.Collections.ObjectModel;

namespace ManagementSystemStudents.ViewModels
{


    public class Teacher : ViewModelBase
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


        public Teacher(string name, string subjectname)
        {
            this.Name = name;
            this.SubjectName = subjectname;
        }

    }


    public class Term : ViewModelBase
    {

        private int num;


        private ObservableCollection<Teacher> teachers;
        public ObservableCollection<Teacher> Teachers => teachers;


        private Teacher selectedTeacher;
        public Teacher SelectedTeacher
        {
            get { return selectedTeacher; }
            set
            {
                selectedTeacher = value;
                OnPropertyChanged("SelectedTeacher");
           }
        }

        public Term(int num)
        {
            teachers = new ObservableCollection<Teacher>();
            this.num = num;
        }

        public void Add(Teacher t)
        {
            teachers.Add(t);
            OnPropertyChanged("Teachers");
        }



        public override string ToString()
        {
            return num.ToString();
        }

    }



    public class Group : ViewModelBase
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



        private Student captain;
        public Student Captain
        {
            get { return captain; }
            set
            {
                captain = value;
                OnPropertyChanged("Captain");
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

        private Term selectedTerm;
        public Term SelectedTerm
        {
            get { return selectedTerm; }
            set
            {
                selectedTerm = value;
                OnPropertyChanged("SelectedTerm");
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

        public void RaiseEvents()
        {
            OnPropertyChanged("Terms");
            OnPropertyChanged("SelectedTerm");
        }

        //ctors

        public Group() : this("Undefined", null) { } 

        public Group(string GroupNum, Student captain=null)
        {
            this.GroupNum = GroupNum;
            this.captain = captain;
            isdisbanded = false;
            terms = new List<Term>(8);
            for (int i = 0,j=0; i < 8; ++i)
                terms.Add(new Term(++j));
        }

        //funcs

        public void setNewCaptain(Student captain)
        {
            if (captain.CurrentGroup == this)
                this.captain = captain;
            else throw new ArgumentException("captain isnt member of the group");
        }
        public Student getCaptain() => captain;

       

        //public void DisbandGroup()
        //{
        //    isDisbanded = true;
        //}


        public bool Equals(Group other)
        {
            return GroupNum == other.GroupNum;
        }

       



    }


}
