using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagementSystemStudents.ViewModels;
using System.Collections.ObjectModel;

namespace ManagementSystemStudents.HelpClasses
{
    public class AsyncLoadCollection
    {
        AddStudentViewModel startpage;
        AddStudentViewModel nextPage;
        AddStudentViewModel prevPage;
        AddStudentViewModel endPage;

        public AddStudentViewModel StartPage => startpage;
        public AddStudentViewModel NextPage => nextPage;
        public AddStudentViewModel PrevPage => prevPage;
        public AddStudentViewModel EndPage => endPage;


        ObservableCollection<Student> StudentsList;
        
        public AsyncLoadCollection(ObservableCollection<Student> List)
        {
            StudentsList = List;
        }

        public async void Init(AddStudentViewModel current)
        {
            await Task.Run(() =>
           {
               if (StudentsList.Count != 0)
               {
                   startpage = new AddStudentViewModel(current.Main, StudentsList[0], current.Wind);
                   endPage = new AddStudentViewModel(current.Main, StudentsList[StudentsList.Count - 1], current.Wind);
                   LoadNext(current);
               }
               else current.Wind.Dispatcher.BeginInvoke((Action)(() =>
               {
                 current.Wind.NextButton.IsEnabled = false;
                 current.Wind.FirstButton.IsEnabled = false;
                 current.Wind.LastButton.IsEnabled = false;
                 current.Wind.PrevButton.IsEnabled = false;

               }));
           });
        }

        public async void LoadNext(AddStudentViewModel current)
        {
            await Task.Run(() =>
           {
                   int index = StudentsList.IndexOf(current.Student);
                   if (index + 1 == StudentsList.Count)
                   {
                       current.Wind.Dispatcher.BeginInvoke((Action)(() => current.Wind.NextButton.IsEnabled = false));
                       nextPage = null;
                   }
                   else
                   {
                       nextPage = new AddStudentViewModel(current.Main, StudentsList[index + 1], current.Wind);
                       current.Wind.Dispatcher.BeginInvoke((Action)(() => current.Wind.NextButton.IsEnabled = true));
                   }

                   if (index - 1 < 0)
                   {
                       current.Wind.Dispatcher.BeginInvoke((Action)(() => current.Wind.PrevButton.IsEnabled = false));
                       prevPage = null;
                   }
                   else
                   {
                       prevPage = new AddStudentViewModel(current.Main, StudentsList[index - 1], current.Wind);
                       current.Wind.Dispatcher.BeginInvoke((Action)(() => current.Wind.PrevButton.IsEnabled = true));
                   }
           });
        }

    }
}
