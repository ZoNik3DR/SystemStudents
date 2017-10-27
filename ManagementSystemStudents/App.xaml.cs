using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ManagementSystemStudents.ViewModels;

namespace ManagementSystemStudents
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void OnStartup(object sender, StartupEventArgs e)
        {
            List<Group> groups = new List<Group>()
            {
                new Group("65454"), new Group("6555"), new Group("6666")
            };

            List<Student> students = new List<Student>()
            {
                new Student("Test","Test","Test",0), new Student(), new Student(),
                new Student(), new Student(),
            };

            List<Teacher> teachers = new List<Teacher>()
            {
                new Teacher("LectureName1", "OAIP"),
                new Teacher("LectureName2", "ISP"),
                new Teacher("LectureName3", "MATH"),
                new Teacher("LectureName4", "LOGIC"),
            };


            students[0].CurrentGroup = groups[0];
            MainViewModel viewModel = new MainViewModel(groups,students,teachers);
            
            MainWindow view = new MainWindow();
            view.DataContext = viewModel;
            view.Show();

        }
    }
}
