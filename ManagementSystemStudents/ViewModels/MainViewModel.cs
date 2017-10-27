using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using ManagementSystemStudents.Commands;
using System.Windows.Data;

namespace ManagementSystemStudents.ViewModels
{
    public class MainViewModel : ViewModelBase
    {

        private Student selectedStudent;
        private Group selectedGroup;
        private ObservableCollection<Student> FullListStudents; 

        public ObservableCollection<Group> GroupsList { get; set; }
        public ObservableCollection<Student> StudentsList { get; set; }
        public ObservableCollection<Teacher> TeachersList { get; set; }


        public void AddStudentToFullListStudents(Student stud) => FullListStudents.Add(stud);

        public void RemoveStudentToFullListStudents(Student stud) => FullListStudents.Remove(stud);
             


        public Student SelectedStudent
        {
            get { return selectedStudent; }
            set
            {
                selectedStudent = value;
                OnPropertyChanged("SelectedStudent");
            }
        }

        public Group SelectedGroup
        {
            get { return selectedGroup; }
            set
            {
                selectedGroup = value;
                OnPropertyChanged("SelectedGroup");
                StudentsList.Clear();
                if (value.GroupNum == "Any Group")
                {
                    StudentsList = new ObservableCollection<Student>(FullListStudents);
                }
                else StudentsList = new ObservableCollection<Student>(FullListStudents.Where(x => x.CurrentGroup == selectedGroup));
                OnPropertyChanged("StudentsList");
                OnPropertyChanged("SelectedStudent");
            }
        }

        private RelayCommand addTeacher;
        public RelayCommand AddTeacher
        {
            get
            {
                return addTeacher ??
                  (addTeacher = new RelayCommand(obj =>
                  {
                      AddLecture LectureWindow = new AddLecture();
                      LectureWindow.OnInitAdd(this);
                      LectureWindow.ShowDialog();                      
                  }));
            }
        }

        private RelayCommand addGroup;
        public RelayCommand AddGroup
        {
            get
            {
                return addGroup ??
                  (addGroup = new RelayCommand(obj =>
                  {
                      GroupInfo wind = new GroupInfo();
                      if (((Group)obj)?.GroupNum == "Any Group")
                          obj = null;
                      wind.OnInit(this, (Group)obj);
                  }));
            }
        }

       

        private RelayCommand addStudent;
        public RelayCommand AddStudent
        {
            get
            {
                return addStudent ??
                  (addStudent = new RelayCommand( obj =>
                  {
                      AddStudent wind = new AddStudent();
                      wind.OnInit(this, (Student)obj);
                  }));
            }
        }


        //ComboBox

        



        #region ctors


        public MainViewModel(List<Group> Groups, List<Student> Students, List<Teacher> Teachers)
        {
            FullListStudents = new ObservableCollection<Student>(Students);
            GroupsList = new ObservableCollection<Group>(Groups);
            TeachersList = new ObservableCollection<Teacher>(Teachers);
            StudentsList = new ObservableCollection<Student>(FullListStudents);
            selectedStudent = StudentsList.FirstOrDefault();
        }
        #endregion
    }
}
