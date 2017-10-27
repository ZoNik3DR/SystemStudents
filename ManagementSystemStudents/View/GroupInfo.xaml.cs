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
    /// Interaction logic for GroupInfo1.xaml
    /// </summary>
    public partial class GroupInfo : Window
    {


        public GroupInfo()
        {
            InitializeComponent();
        }

        public void OnInit(MainViewModel obj, Group sender)
        {

            if (sender == null)
            {
                this.DeleteButton.Visibility = System.Windows.Visibility.Hidden;
                this.DisbandButton.Visibility = System.Windows.Visibility.Hidden;
            }
            AddGroupViewModel AddGroupViewModel = new AddGroupViewModel(obj, sender, this);
            this.DataContext = AddGroupViewModel;
            this.Show();
            
        }

    }
}
