using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using ManagementSystemStudents.ViewModels;
using System.Collections.ObjectModel;

namespace ManagementSystemStudents
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public class Database
        {
            List<Group> groups;
            List<Student> students;
            List<Lecture> Lectures;

            static void LoadDb()
            {

            }

            
            static void SaveToDb(ObservableCollection<Student> students, ObservableCollection<Group> groups, ObservableCollection<Lecture> Lectures)
            {
                
            }
            
            



        }



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

            List<Lecture> teachers = new List<Lecture>()
            {
                new Lecture("LectureName1", "OAIP"),
                new Lecture("LectureName2", "ISP"),
                new Lecture("LectureName3", "MATH"),
                new Lecture("LectureName4", "LOGIC"),
            };


            students[0].CurrentGroup = groups[0];
            MainViewModel viewModel = new MainViewModel(groups,students,teachers);
            
            MainWindow view = new MainWindow();
            view.DataContext = viewModel;
            view.Show();

        }

        private void OnClose(object sender, StartupEventArgs e)
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

            List<Lecture> lectures = new List<Lecture>()
            {
                new Lecture("LectureName1", "OAIP"),
                new Lecture("LectureName2", "ISP"),
                new Lecture("LectureName3", "MATH"),
                new Lecture("LectureName4", "LOGIC"),
            };


            students[0].CurrentGroup = groups[0];
            MainViewModel viewModel = new MainViewModel(groups, students, lectures);

            MainWindow view = new MainWindow();
            view.DataContext = viewModel;
            view.Show();

        }
    }
}
