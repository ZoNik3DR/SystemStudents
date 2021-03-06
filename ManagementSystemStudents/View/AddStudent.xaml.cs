﻿using System;
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
using System.ComponentModel;

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

        public AddStudentViewModel OnInit(MainViewModel obj, Student sender)
        {
            if (sender == null)
                this.DeleteButton.Visibility = System.Windows.Visibility.Hidden;
            AddStudentViewModel AddStudentViewModel = new AddStudentViewModel(obj, sender, this);
            this.DataContext = AddStudentViewModel;
            this.Show();
            return AddStudentViewModel;
        }

        public void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            (DataGridMarks.Items as IEditableCollectionView).CommitEdit();
            
        }
    }
}

