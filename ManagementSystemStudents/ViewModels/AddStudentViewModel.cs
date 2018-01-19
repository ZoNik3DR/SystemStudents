using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using ManagementSystemStudents.Commands;
using System.Windows;


namespace ManagementSystemStudents.ViewModels
{
    public class AddStudentViewModel : Student
    {
        private Student student;
        public Student Student => student;



        private ObservableCollection<Group> groupsLink;
        public ObservableCollection<Group> GroupsLink => groupsLink;

        private MainViewModel obj;
        public MainViewModel Main => obj;
        private bool isNew;
        public AddStudent Wind => wind;
        private AddStudent wind;




        public AddStudentViewModel(MainViewModel obj, Student sender, AddStudent wind)
        {
            if (sender == null)
            {
                sender = new Student();
                if (obj.SelectedGroup?.GroupNum != "Any Group")
                    sender.CurrentGroup = (obj).SelectedGroup;
                isNew = true;
            }
            groupsLink = obj.GroupsList;
            student = sender;
            this.wind = wind;
            this.obj = obj;
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

        private RelayCommand deleteSelectedPrevGroup;
        public RelayCommand DeleteSelectedPrevGroup
        {
            get
            {
                return deleteSelectedPrevGroup ??
                  (deleteSelectedPrevGroup = new RelayCommand(obj =>
                  {
                      if (SelectedPrevGroup != null)
                          Student.PrevGroups.Remove(SelectedPrevGroup);
                  }));
            }
        }

        private RelayCommand addMarkSubjectCommand;
        public RelayCommand AddMarkSubjectCommand
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
                      Student.MarkSubjects.Remove((MarkSubject)obj);
                  }));
            }
        }


        private RelayCommand delete;
        public RelayCommand Delete
        {
            get
            {
                return delete ??
                  (delete = new RelayCommand(obj =>
                  {
                      Main.FullListStudents.Remove(Student);
                      Main.StudentsList.Remove(student);
                      wind.Close();
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
                          Main.FullListStudents.Add(student);
                          Main.StudentsList.Add(student);
                      }
                      Group gr = Main.SelectedGroup; // костыль
                      Main.SelectedGroup = null; //
                      Main.SelectedGroup = gr; // 
                      Main.ApplySorting.Execute(null);
                      wind.Close();
                  }));
            }
        }

        private RelayCommand loadPage;
        public RelayCommand LoadPage
        {
            get
            {
                return loadPage ??
                (loadPage = new RelayCommand(obj => 
                {
                    switch ((string)obj)
                    {
                        case "NextButton":
                            {
                                    wind.DataContext = Main.AsyncLoadCollection.NextPage;
                            }
                            break;
                        case "PrevButton":
                            {
                                    wind.DataContext = Main.AsyncLoadCollection.PrevPage;
                            }
                            break;
                        case "FirstButton":
                            {
                                if(Main.AsyncLoadCollection.StartPage!=null)
                                    wind.DataContext = Main.AsyncLoadCollection.StartPage;
                            }
                            break;
                        case "LastButton":
                            {
                                if(Main.AsyncLoadCollection.EndPage!=null)
                                    wind.DataContext = Main.AsyncLoadCollection.EndPage;
                            }
                            break;
                    }
                    Main.AsyncLoadCollection.LoadNext((AddStudentViewModel)wind.DataContext);
                    
                }));
            }

        }

    }
}
