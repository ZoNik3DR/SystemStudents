using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagementSystemStudents.Commands;

namespace ManagementSystemStudents.ViewModels
{
    public class AddSubjectViewModel : ViewModelBase
    {
        private AddSubject wind;

        private AddGroupViewModel addGroup;
        public AddGroupViewModel AddGroup => addGroup;

        private MainViewModel main;
        public MainViewModel Main => main;

        private Teacher selectedLecture;
        public Teacher SelectedLecture
        {
            get { return selectedLecture; }
            set
            {
                selectedLecture = value;
                OnPropertyChanged("SelectedLecture");
            }
        }

        private Term selectedTerm;
        public Term SelectedTerm
        {
            get { return selectedTerm; }
            set
            {
                selectedTerm = value;
                OnPropertyChanged("SelectedTerm");
            }
        }

        public AddSubjectViewModel(MainViewModel main, AddGroupViewModel GroupInfo,AddSubject wind)
        {
            addGroup = GroupInfo;
            this.wind = wind;
            this.main = main;
        }

        private RelayCommand save;
        public RelayCommand Save
        {
            get
            {
                return save ??
                  (save = new RelayCommand(obj =>
                  {
                      selectedTerm.Add(selectedLecture);
                      wind.Close();
                      
                  }));
            }
        }
    }
}
