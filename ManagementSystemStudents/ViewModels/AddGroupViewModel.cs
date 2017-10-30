using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using ManagementSystemStudents.ViewModels;
using ManagementSystemStudents.Commands;
using System.Windows;

namespace ManagementSystemStudents.ViewModels
{
    public class AddGroupViewModel : ViewModelBase
    {
        private Group group;
        public Group Group => group;

        private ObservableCollection<Student> studentsLink;
        public ObservableCollection<Student> StudentsLink => studentsLink;

        private ObservableCollection<Lecture> lecturesList;
        public ObservableCollection<Lecture> LecturesList => lecturesList;

        private MainViewModel obj;
        public MainViewModel Main => obj;
        private bool isNew;

        public GroupInfo wind;

        public AddGroupViewModel(MainViewModel obj, Group sender, GroupInfo wind)
        {
            studentsLink = obj.StudentsList;
            if (sender == null)
            {
                isNew = true;
                sender = new Group();
                studentsLink = null;
            }
            lecturesList = obj.LecturesList;
            group = sender;
            this.wind = wind;
            this.obj = obj;
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

        private Lecture selectedLecture;
        public Lecture SelectedLecture
        {
            get { return selectedLecture; }
            set
            {
                selectedLecture = value;
                OnPropertyChanged("SelectedLecture");
            }
        }

        private RelayCommand delSubject;
        public RelayCommand DelSubject
        {
            get
            {
                return delSubject ??
                  (delSubject = new RelayCommand(obj =>
                  {
                      if (SelectedLecture != null)
                      {
                          SelectedTerm.Lectures.Remove(SelectedLecture);
                      }
                  }));
            }
        }

        private RelayCommand addSubject;
        public RelayCommand AddSubject
        {
            get
            {
                return addSubject ??
                  (addSubject = new RelayCommand(obj =>
                  {
                      AddSubject wind = new AddSubject();
                      wind.OnInit(Main,this);
                      wind.Show();
                     
                  }));
            }
        }

        private RelayCommand save;
        public RelayCommand Save
        {
            get
            {
                return save ??
                  (save = new RelayCommand(obj =>
                  {
                          if (isNew)
                              Main.GroupsList.Add(group);
                          wind.Close();
                  }));
            }
        }

        private RelayCommand disband;
        public RelayCommand Disband
        {
            get
            {
                return disband ??
                  (disband = new RelayCommand(obj =>
                  {
                      Group.IsDisbanded = true;
                      MessageBox.Show("Group disbanded.");                      
                      for (int i = 0; i < StudentsLink.Count; ++i)
                          StudentsLink[i].CurrentGroup = null;
                      studentsLink = null;
                      OnPropertyChanged("StudentsLink");
                      Group.Captain = null;
                      OnPropertyChanged("Captain");
                      Main.GroupsList.Remove(group);
                      wind.Close();
                  }));
            }
        }

        
    }
}
