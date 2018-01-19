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
                if (value < 0 || value > 10)
                    value = 0;
                exam = value;
                OnPropertyChanged("Exam");
            }
        }

        public MarkSubject(string name, int mark)
        {
            SubName = name;
            Exam = mark;
        }

        public override string ToString()
        {
            return subname + "\n" + exam;
        }

    }


    public class Student : ViewModelBase, IEquatable<Student>
    {
        //field & properties

        public int SumOfMarks => marksubjects.Count == 0 ? 0 : marksubjects.Sum(x => x.Exam) / marksubjects.Count;


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

        private bool iscaptain;
        public bool IsCaptain
        {
            get { return iscaptain; }
            set
            {
                iscaptain = value;
                OnPropertyChanged("GetGroupNum");
                OnPropertyChanged("IsCaptain");
            }
        }

        public void FirstSetGroup(Group link)
        {
            currentgroup = link;
        }

        private Group currentgroup;
        public Group CurrentGroup
        {
            get { return currentgroup; }
            set
            {
                if (currentgroup != null)
                {
                    if (!PrevGroups.Contains(currentgroup.GroupNum))
                        PrevGroups.Add(currentgroup.GroupNum);
                }
                IsCaptain = false;
                OnPropertyChanged("PrevGroups");
                currentgroup = value;
                OnPropertyChanged("CurrentGroup");
                OnPropertyChanged("GetGroupNum");
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

        public Student(string name, string surName, string midName,
            int receiptYear, bool iscaptain, List<string> prevgroups, List<MarkSubject> Marks, string groupnum)
        {
           
            this.Name = name;
            this.SurName = surName;
            this.MidName = midName;
            if (prevgroups == null)
                prevGroups = new ObservableCollection<string>();
            else this.prevGroups = new ObservableCollection<string>(prevgroups);
            this.currentgroup = null;
            this.iscaptain = iscaptain;
            if (Marks == null)
                marksubjects = new ObservableCollection<MarkSubject>();
            else this.MarkSubjects = new ObservableCollection<MarkSubject>(Marks);
            this.groupnum = groupnum; // temp solution 
            if (receiptYear == 0)
                this.ReceiptYear = DateTime.Now.Year;
            else ReceiptYear = receiptYear;

        }

        public Student(string name = "NoName", string surName = "NoName", string midName = "NoName", int receiptYear =0) : this(name, surName, midName, receiptYear, false, null,
            null, "")
        {
        }

        private string groupnum;
        public string GetGroupNum
        {
            get { return CurrentGroup == null ? "Undefined" : currentgroup.GroupNum; }
        }

        public string getGroupNumNotLinked => groupnum;



        public bool Equals(Student obj) => name == obj.name && surname == obj.surname && midname == obj.midname && receiptyear == obj.receiptyear;

        public string DisplayName => Name + " " + SurName + " " + MidName;


        public void Deconstruct(out string name, out string surname, out string midname, out int receiptyear,
            out string groupnum, out bool iscaptain, out string[] prev, out MarkSubject[] Marks)
        {
            name = this.name;
            surname = this.surname;
            midname = this.midname;
            receiptyear = this.receiptyear;
            groupnum = GetGroupNum;
            iscaptain = this.iscaptain;
            prev = PrevGroups.ToArray();
            Marks = this.marksubjects.ToArray();
        }


        public override int GetHashCode()
        {
            return base.GetHashCode() ^ name.GetHashCode() ^ surname.GetHashCode() ^ midname.GetHashCode() ^ groupnum.GetHashCode();
        }
    }

}
