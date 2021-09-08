using CarePlant.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarePlant.ViewModels
{
   public class MainViewModel:INotifyPropertyChanged
    {
        public MainWindow MainWindow { get; set; }
        public MainViewModel(MainWindow MainWindow)
        {
            this.MainWindow = MainWindow;
        }
        public LogInSectionCommand LogInSectionCommand => new LogInSectionCommand(MainWindow);
        public SignInSectionCommand SignInSectionCommand => new SignInSectionCommand(MainWindow);
       
    }
}
