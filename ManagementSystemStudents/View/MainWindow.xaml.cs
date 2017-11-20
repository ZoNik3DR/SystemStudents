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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ManagementSystemStudents.ViewModels;


namespace ManagementSystemStudents
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void CloseClick(object sender, RoutedEventArgs e)
        {
            ((MainViewModel)this.DataContext).Exit.CanExecute(null);
            this.Close();
        }

        private void PopUpButton(object sender, RoutedEventArgs e)
        {
            Popup.IsOpen = true;
        }
    }
}
