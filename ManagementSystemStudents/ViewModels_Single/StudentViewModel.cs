using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagementSystemStudents.ViewModels;
using System.Collections.Specialized;
using System.Collections.ObjectModel;


namespace ManagementSystemStudents.ViewModels
{
    public class MarkSubject
    {
        private string subname { get; set; }
        private int exam;

        public MarkSubject(string name)
        {
            subname = name;
        }

        int Exam
        {
            get => exam;
            set
            {
                if (value > 10)
                    throw new ArgumentOutOfRangeException();
                exam = value;
            }
        }
    }


    public class Student : ViewModelBase
    {
        //field & properties


        private List<MarkSubject> subjects;
        public List<MarkSubject> Subjects
        {
            get { return subjects; }
            set
            {
                subjects = value;
                OnPropertyChanged("Subjects");
            }
        }

        void AddSubject(MarkSubject Sub)
        {
            Subjects.Add(Sub);
            OnPropertyChanged("MarkSubjects");
        }


        private Group currentgroup;
        public Group CurrentGroup
        {
            get { return currentgroup; }
            set
            {
                PrevGroups += currentgroup?.GroupNum + "\n";
                OnPropertyChanged("PrevGroups");
                currentgroup = value;
                OnPropertyChanged("CurrentGroup");
                OnPropertyChanged("GetGroupNum");

            }
        }

        private Group selectedPrevGroup;
        public Group SelectedPrevGroup
        {
            get { return selectedPrevGroup; }
            set
            {
                selectedPrevGroup = value;
                OnPropertyChanged("SelectedPrevGroup");
            }
        }


        public List<string> prevGroups;
        public List<string> PrevGroups
        {
            get { return prevGroups; }
            set
            {
                prevGroups = value;
                OnPropertyChanged("PrevGroups");
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
                OnPropertyChanged("DisplayName");

            }
        }

        private string surname;
        public string SurName
        {
            get { return surname; }
            set
            {
                surname = value;
                OnPropertyChanged("SurName");
                OnPropertyChanged("DisplayName");
            }
        }

        private string midname;
        public string MidName
        {
            get { return midname; }
            set
            {
                midname = value;
                OnPropertyChanged("MidName");
                OnPropertyChanged("DisplayName");
            }
        }


        private int receiptyear;
        public int ReceiptYear
        {
            get { return receiptyear; }
            set
            {
                receiptyear = value;
                OnPropertyChanged("ReceiptYear");
            }
        }

        //ctors



        public Student(string name, string surName, string midName, string photourl,
            int receiptYear, List<MarkSubject> Subjects, Group currentGroup)
        {
            this.Name = name;
            this.SurName = surName;
            this.MidName = midName;
            //        if (prevGroups == null)
            //              this.prevGroups = new List<string>();
            //            else this.PrevGroups = prevGroups;
            PrevGroups += "text" + "\n";
            this.CurrentGroup = currentGroup;
            if (Subjects == null)
                Subjects = new List<MarkSubject>();
            else this.Subjects = Subjects;
            
                       
        }

        public Student(string name="NoName", string surName= "NoName", string midName= "NoName", int receiptYear=0) : this(name, surName, midName, "NoPhoto", receiptYear
            , null, null) { }


        public string GetGroupNum
        {
            get { return  CurrentGroup == null ? "Undefined" : currentgroup.GroupNum; }
        }
    
        //func 

        //Interface Implementation

        public bool Equals(Student obj)
        {
            if (name == obj.name && surname == obj.surname && midname == obj.midname && receiptyear == obj.receiptyear)
                return true;
            else return false;
        }



        public string DisplayName
        {
            get { return Name + " " + SurName + " " + MidName;  }
        }

    }

}
