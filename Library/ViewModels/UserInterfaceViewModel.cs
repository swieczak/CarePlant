using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarePlant.Core;

namespace CarePlant.ViewModels
{
    public class UserInterfaceViewModel : ObservableObjects
    {
        public RelayCommand BaseViewCommand { get; set; }
        public RelayCommand ActionViewCommand { get; set; }
        public RelayCommand PlantViewCommand { get; set; }

        public HubViewModel Basevm { get; set; }
        public ActionViewModel Actionvm { get; set; }
        public PlantViewModel Plantvm { get; set; }
        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        public UserInterfaceViewModel()
        {
            Basevm = new HubViewModel();
            Actionvm = new ActionViewModel();
            Plantvm = new PlantViewModel();
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
            PlantViewCommand = new RelayCommand(o =>
            {
                CurrentView = Plantvm;
            }
            );
        }   
    }
}
