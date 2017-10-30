﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagementSystemStudents.ViewModels;
using System.Collections.ObjectModel;

namespace ManagementSystemStudents.ViewModels
{
    public class Lecture : ViewModelBase
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

        public Lecture(string name, string subjectname)
        {
            this.Name = name;
            this.SubjectName = subjectname;
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
  
        public bool Equals(Group other)
        {
            return GroupNum == other.GroupNum;
        }

    }
}
