using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using ManagementSystemStudents.Commands;


namespace ManagementSystemStudents.ViewModels
{
    public class AddStudentViewModel : Student
    {
        private Student student;
        public Student Student => student;

        
        
        private ObservableCollection<Group> groupsLink;
        public ObservableCollection<Group> GroupsLink => groupsLink;

        private MainViewModel obj;
        private MainViewModel Main => obj;
        private bool isNew; 
        private AddStudent wind;

        public AddStudentViewModel(MainViewModel obj, Student sender, AddStudent wind)
        {
            if (sender == null)
            {
                sender = new Student();
                if(obj.SelectedGroup?.GroupNum != "Any Group")
                    sender.CurrentGroup = (obj).SelectedGroup;
                isNew = true;
            }
            groupsLink = obj.GroupsList;
            student = sender;
            this.wind = wind;
            this.obj = obj;
        }



        private RelayCommand deleteSelectedPrevGroup;
        public RelayCommand DeleteSelectedPrevGroup
        {
            get
            {
                return deleteSelectedPrevGroup ??
                  (deleteSelectedPrevGroup = new RelayCommand(obj =>
                  {
                     if(Student.SelectedPrevGroup!=null)
                          Student.PrevGroups.Remove(Student.SelectedPrevGroup);
                  }));
            }
        }

        private RelayCommand addMarkSubjectCommand;
        public RelayCommand AddMarkSubjectComman
        {
            get
            {
                return addMarkSubjectCommand ??
                  (addMarkSubjectCommand = new RelayCommand(obj =>
                  {
                      Student.MarkSubjects.Add(new MarkSubject("name", 0));
                  }));
            }
        }


        private RelayCommand deleteMarkSubject;
        public RelayCommand DeleteMarkSubject
        {
            get
            {
                return deleteMarkSubject ??
                  (deleteMarkSubject = new RelayCommand(obj =>
                  {
                      
                      if (obj!=null)
                         Student.MarkSubjects.Remove((MarkSubject)SelectedMarkSubject);
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
                      {
                          Main.AddStudentToFullListStudents(student);
                          Main.StudentsList.Add(student);
                      }
                      wind.Close();
                  }));
            }
        }
    }
}
