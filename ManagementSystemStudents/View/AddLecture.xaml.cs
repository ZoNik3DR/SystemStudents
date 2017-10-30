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

namespace ManagementSystemStudents
{
    /// <summary>
    /// Interaction logic for AddLecture.xaml
    /// </summary>
    public partial class AddLecture : Window
    {
        public object obj;

        public AddLecture()
        {
            InitializeComponent();
            
        }

        public void OnInitAdd(object obj)
        {
            this.DataContext = new Lecture("LectureName","SubjectName");
            this.obj = obj; 
        }
        private void SaveClick(object sender, RoutedEventArgs e)
        {
            Lecture lecture = (Lecture)(this.DataContext);
            ((MainViewModel)obj).LecturesList.Add(lecture);
            Close();
        }
    }
}
