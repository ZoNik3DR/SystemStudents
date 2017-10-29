using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagementSystemStudents.ViewModels;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;


namespace ManagementSystemStudents.ViewModels
{
    public class MarkSubject : ViewModelBase
    {
        private string subname;
        public string SubName
        {
            get => subname;
            set
            {
                subname = value;
                OnPropertyChanged("SubName");
            }
        }


        private int exam;
        public int Exam
        {
            get => exam;
            set
            {
                exam = value;
                OnPropertyChanged("Exam");
            }
        }


        public MarkSubject(string name, int mark)
        {
            SubName= name;
            Exam = mark;
        }

        //int Exam
        //{
        //    get => exam;
        //    set
        //    {
        //        if (value > 10)
        //            throw new ArgumentOutOfRangeException();
        //        exam = value;
        //    }
        //}
    }


    public class Student : ViewModelBase
    {
        //field & properties


        private object selectedMarkSubject;
        public object SelectedMarkSubject
        {
            get { return selectedMarkSubject; }
            set
            {
                selectedMarkSubject = value;
                OnPropertyChanged("SelectedMarkSubject");
            }
        }

        private ObservableCollection<MarkSubject> marksubjects;
        public ObservableCollection<MarkSubject> MarkSubjects
        {
            get { return marksubjects; }
            set
            {
                marksubjects = value;
                OnPropertyChanged("MarkSubjects");
            }
        }

    
        private Group currentgroup;
        public Group CurrentGroup
        {
            get { return currentgroup; }
            set
            {
                if (currentgroup !=null)
                    PrevGroups.Add(currentgroup.GroupNum);
                OnPropertyChanged("PrevGroups");
                currentgroup = value;
                OnPropertyChanged("CurrentGroup");
                OnPropertyChanged("GetGroupNum");
            }
        }

        private string selectedPrevGroup;
        public string SelectedPrevGroup
        {
            get { return selectedPrevGroup; }
            set
            {
                selectedPrevGroup = value;
                OnPropertyChanged("SelectedPrevGroup");
            }
        }


        private ObservableCollection<string> prevGroups;
        public ObservableCollection<string> PrevGroups
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
            int receiptYear, ObservableCollection<MarkSubject> Subjects, Group currentGroup)
        {
            this.Name = name;
            this.SurName = surName;
            this.MidName = midName;
            prevGroups = new ObservableCollection<string>();
            this.CurrentGroup = currentGroup;
            if (Subjects == null)
                marksubjects = new ObservableCollection<MarkSubject>();
            else this.MarkSubjects = Subjects;
            
                       
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
