using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarePlant.Core;

namespace CarePlant.ViewModels
{
    class UserViewModel : ObservableObjects
    {
        public RelayCommand BaseViewCommand { get; set; }
        public RelayCommand ActionViewCommand { get; set; }

        public HubViewModel Basevm { get; set; }
        public ActionViewModel Actionvm { get; set; }
        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        public UserViewModel() 
        {
            Basevm = new HubViewModel();
            Actionvm = new ActionViewModel();
            CurrentView = Basevm;

            BaseViewCommand = new RelayCommand(o =>
            {
                CurrentView = Basevm;
            }
            );

            ActionViewCommand = new RelayCommand(o =>
            {
                CurrentView = Actionvm;
            }
);
        }
    }
}
