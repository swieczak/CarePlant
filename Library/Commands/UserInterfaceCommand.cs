using CarePlant.ViewModels;
using CarePlant.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CarePlant.Commands
{
   public class UserInterfaceCommand:ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }
        public LogInWindow LogInWindow { get; set; }
        public UserInterfaceCommand(LogInWindow LogInWindow)
        {
            this.LogInWindow = LogInWindow;
        }
        public void Execute(object parameter)
        {
            UserInterfaceViewModel UserInterfaceViewModel = new UserInterfaceViewModel();
         // LogInWindow.Close();

            UserInterface UserInterface = new UserInterface(UserInterfaceViewModel);
            UserInterface.ShowDialog();
        }
    }
}
