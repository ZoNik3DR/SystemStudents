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
    /// Interaction logic for AddSubject.xaml
    /// </summary>
    public partial class AddSubject : Window
    {


        public AddSubject()
        {
            InitializeComponent();
        }

        public void OnInit(MainViewModel MainViewModel, AddGroupViewModel PrevContext)
        {
            AddSubjectViewModel view = new AddSubjectViewModel(MainViewModel, PrevContext,this);
            this.DataContext = view;

        }

    }
}
