using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using ManagementSystemStudents.Commands;
using System.Windows.Data;
using System.IO;
using System.Windows;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using IPluginSpace;
using System.Windows.Controls;
using System.Threading;
using ManagementSystemStudents.HelpClasses;

namespace ManagementSystemStudents.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        
        private MainWindow wind;
        private AsyncLoadCollection asyncLoadCollection;
        public AsyncLoadCollection AsyncLoadCollection => asyncLoadCollection;

        public MainWindow GetWind => wind;
        public void SetWind(MainWindow wind) => this.wind = wind;






        private Student selectedStudent;
        private Group selectedGroup;
        private string surNameSorting;
        private bool marksChecked;
        private bool nameChecked;
        private string groupNameSearch;



        public ObservableCollection<Student> FullListStudents;
        public ObservableCollection<Group> GroupsList { get; set; }
        public ObservableCollection<Student> StudentsList { get; set; }
        public ObservableCollection<Lecture> LecturesList { get; set; }

        //StudentsSorting
        public bool MarksChecked
        {
            get => marksChecked;
            set
            {
                marksChecked = value;
                OnPropertyChanged("MarksChecked");
                ApplySorting.Execute(null);
            }
        }

        public bool NameChecked
        {
            get => nameChecked;
            set
            {
                nameChecked = value;
                OnPropertyChanged("NameChecked");
                ApplySorting.Execute(null);
            }
        }

        public string SurNameSorting
        {
            get => surNameSorting;
            set
            {
                surNameSorting = value;
                if (SelectedGroup?.GroupNum == "Any Group")
                    StudentsList = new ObservableCollection<Student>(FullListStudents);
                else StudentsList = new ObservableCollection<Student>(FullListStudents.AsParallel().Where(x => x.CurrentGroup == selectedGroup));
                if (!(value == string.Empty))
                    StudentsList = new ObservableCollection<Student>(StudentsList.AsParallel().Where(x => x.SurName.StartsWith(value)));
                OnPropertyChanged("SurNameSorting");
                OnPropertyChanged("StudentsList");
            }
        }

        public string GroupNameSearch
        {
            get => groupNameSearch;
            set
            {
                groupNameSearch = value;
                if (GroupNameSearch == "Any Group")
                    SelectedGroup = (Group)wind.ComboBoxGroups.Items[0];
                else if (groupNameSearch == string.Empty)
                    return;
                else
                {
                    foreach (var item in wind.ComboBoxGroups?.Items)
                    {
                        if (SelectedGroup.GroupNum == ((Group)item).GroupNum)
                            SelectedGroup = (Group)item;
                        else if (((Group)item).GroupNum.StartsWith(value))
                            SelectedGroup = (Group)item;
                        
                    }           
                }
                OnPropertyChanged("SelectedGroup");
            }
        }
        

        //GroupsSorting


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
    //            StudentsList.Clear();
                if (value?.GroupNum == "Any Group")
                    StudentsList = new ObservableCollection<Student>(FullListStudents);
                else StudentsList = new ObservableCollection<Student>(FullListStudents.AsParallel().Where(x => x.CurrentGroup == selectedGroup));
                OnPropertyChanged("StudentsList");
                OnPropertyChanged("SelectedStudent");
                applySorting?.Execute(null);
                SurNameSorting = string.Empty;
            }
        }

        private RelayCommand applySorting;
        public RelayCommand ApplySorting
        {
            get
            {
                return applySorting ??
                    (applySorting = new RelayCommand(obj =>
                    {
                        if (nameChecked)
                            StudentsList = new ObservableCollection<Student>(StudentsList.AsParallel().OrderBy(u => u.SurName));
                        if (marksChecked)
                            StudentsList = new ObservableCollection<Student>(StudentsList.AsParallel().OrderByDescending(u => u.SumOfMarks));
                        if (nameChecked == false && marksChecked == false)
                            if (selectedGroup == null || SelectedGroup.GroupNum == "Any Group")
                                StudentsList = new ObservableCollection<Student>(FullListStudents);
                            else StudentsList = new ObservableCollection<Student>(FullListStudents.AsParallel().Where(x => x.CurrentGroup == SelectedGroup));
                        OnPropertyChanged("StudentsList");
                    }));
            }
        }

        private RelayCommand addLecture;
        public RelayCommand AddLecture
        {
            get
            {
                return addLecture ??
                  (addLecture = new RelayCommand(obj =>
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
        public  RelayCommand AddStudent
        {
            get
            {
                return addStudent ??
                  (addStudent = new RelayCommand(obj =>
                  {
                  AddStudent wind = new AddStudent();
                  asyncLoadCollection = new AsyncLoadCollection(StudentsList);
                  AsyncLoadCollection.Init(wind.OnInit(this, (Student)obj));
                  }));;
            }
        }

        private RelayCommand exit;
        public RelayCommand Exit
        {
            get
            {
                return exit ??
                  (exit = new RelayCommand(obj =>
                  {
                      Database db = new Database();
                      wind.Close();
                      db.SaveToDb(FullListStudents.ToList(), GroupsList.ToList(), LecturesList.ToList());
                      Environment.Exit(0);
                  }));
            }
        }



        //FilesWorks

        private RelayCommand saveCurrentInfo;
        public RelayCommand SaveCurrentInfo
        {
            get
            {
                return saveCurrentInfo ??
                  (saveCurrentInfo = new RelayCommand(obj =>
                  {
                      CommonOpenFileDialog dialog = new CommonOpenFileDialog("Save folder");
                      dialog.InitialDirectory = Environment.CurrentDirectory;
                      dialog.IsFolderPicker = true;
                      if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                      {
                          MessageBox.Show("Save info to: " + dialog.FileName);
                          string filename = dialog.FileName;
                          if (filename.Contains("DB"))
                              filename = filename.Replace("DB", "");
                          Database db = new Database(filename);
                          db.SaveToDb(FullListStudents.ToList(),GroupsList.ToList(),LecturesList.ToList()); 
                      }
                  }));
            }
        }

        private RelayCommand openInfoFromDb;
        public RelayCommand OpenInfoFromDb
        {
            get
            {
                return openInfoFromDb ??
                  (openInfoFromDb = new RelayCommand(async obj =>
                  {
                  CommonOpenFileDialog dialog = new CommonOpenFileDialog("Load from folder");
                  dialog.InitialDirectory = Environment.CurrentDirectory;
                  dialog.IsFolderPicker = true;
                  if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                  {
                      MessageBox.Show("Load info from: " + dialog.FileName);
                      await Task.Run(() =>
                      {
                          string filename = dialog.FileName;
                          if (filename.Contains("DB"))
                              filename = filename.Replace("DB", "");
                          Database db = new Database(filename);
                          db.readFromDb(this);

                          OnPropertyChanged("LecturesList");
                          OnPropertyChanged("StudentsList");
                          OnPropertyChanged("GroupsList");
                      });
                      }
                  }));
            }
        }


        public void ApendCollections(List<Student> ApStud, List<Group> ApGroups, List<Lecture> ApLectures)
        {

            FullListStudents = new ObservableCollection<Student>(FullListStudents.Union(ApStud,new StudentsComparer()).AsParallel());
            GroupsList = new ObservableCollection<Group>(GroupsList.Union(ApGroups,new GroupsComparer()).AsParallel());
            LecturesList = new ObservableCollection<Lecture>(LecturesList.Union(ApLectures, new LecturesComparer()).AsParallel());
            StudentsList = FullListStudents;
            OnPropertyChanged("StudentsList");
            OnPropertyChanged("LecturesList");
            OnPropertyChanged("FullListStudents");
            OnPropertyChanged("GroupsList");
            SelectedGroup = (Group)wind.ComboBoxGroups.Items[0];
        }

        public async void LoadMenu(List<IPlugin> plugs)
        {
            await wind.Dispatcher.BeginInvoke((Action)(() => 
              {
                MenuItem PlugMenu = new MenuItem();
                PlugMenu.Header = "Plugins";
                PlugMenu.Width = 50;
                PlugMenu.HorizontalAlignment = HorizontalAlignment.Left;

                foreach (var item in plugs)
                {
                    MenuItem menuitem = new MenuItem();
                    menuitem.Header = item.PluginName;
                    item.Init(this);
                    menuitem.Click += (o, e) => item.Execute();
                    PlugMenu.Items.Add(menuitem);
                }
                wind.MainMenu.Items.Add(PlugMenu);
            }));
        }

        #region ctors


        public MainViewModel(List<Group> Groups, List<Student> Students, List<Lecture> Lectures)
        {
            FullListStudents = new ObservableCollection<Student>(Students);
            GroupsList = new ObservableCollection<Group>(Groups);
            LecturesList = new ObservableCollection<Lecture>(Lectures);
            StudentsList = new ObservableCollection<Student>(FullListStudents);
            selectedStudent = StudentsList.FirstOrDefault();
            SelectedGroup = GroupsList.FirstOrDefault();
        }

        public MainViewModel()
        {
            FullListStudents = new ObservableCollection<Student>();
            GroupsList = new ObservableCollection<Group>();
            LecturesList = new ObservableCollection<Lecture>();
            StudentsList = new ObservableCollection<Student>();
        }


        #endregion
    }
}
