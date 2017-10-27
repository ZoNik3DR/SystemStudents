using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ManagementSystemStudents.ViewModels;
using System.Collections.ObjectModel;

namespace ManagementSystemStudents
{
    /// <summary>
    /// Interaction logic for AddStudent.xaml
    /// </summary>
    public partial class AddStudent : Window
    {

        public AddStudent()
        {
            InitializeComponent();
        }

        public void OnInit(MainViewModel obj, Student sender)
        {
            if (sender == null)
                this.DeleteButton.Visibility = System.Windows.Visibility.Hidden;
            AddStudentViewModel AddStudentViewModel = new AddStudentViewModel(obj, sender, this);
            this.DataContext = AddStudentViewModel;
            this.Show();
        }

    }
}






//namespace ManagementSystemStudents
//{
//    /// <summary>
//    /// Interaction logic for AddStudent.xaml
//    /// </summary>
//    public partial class AddStudent : Window
//    {
//        public object MainViewModel;
//        public Student Student;
//        public ObservableCollection<Group> GroupsListLink;

//        public AddStudent()
//        {
//            InitializeComponent();
//        }

//        public void OnInit(object obj, Student sender)
//        {
//            if (sender == null)
//            {
//                this.DataContext = new Student();
//                DeleteButton.Visibility = Visibility.Hidden;
//            }
//            else this.DataContext = sender;

//            GroupsListLink = ((MainViewModel)(obj)).GroupsList;
//            GroupChoose.ItemsSource = GroupsListLink;


//            Student = sender;
//            this.MainViewModel = obj;
//        }

//        private void SaveClick(object sender, RoutedEventArgs e)
//        {
//            if (Student == null)
//            {
//                Student stud = (Student)(this.DataContext);
//                ((MainViewModel)MainViewModel).AddStudentToFullListStudents(stud);
//            }
//            Close();
//        }

//        private void DeleteStudent(object sender, RoutedEventArgs e)
//        {
//            ((MainViewModel)MainViewModel).RemoveStudentToFullListStudents(Student);
//            Close();
//        }

//        private void GroupChoose_Selected(object sender, RoutedEventArgs e)
//        {
//            Student.CurrentGroup = (Group)GroupChoose.SelectedValue;
//        }
//    }
//}
