using CarePlant.Model.DAL;
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
        public LogInWindow LoginWindow { get; set; }
        public UserInterfaceCommand(LogInWindow LogInWindow)
        {
            this.LoginWindow = LogInWindow;
        }
        public void Execute(object parameter)
        {
            UserInterfaceViewModel UserInterfaceViewModel = new UserInterfaceViewModel();
            UserInterface Userinterface = new UserInterface(UserInterfaceViewModel);

            LogInViewModel.logInfo.ID = DataAccess.logging(LogInViewModel.logInfo);
            if(LogInViewModel.logInfo.ID != 0)
            {
                //LoginWindow.Close();
                Userinterface.ShowDialog();
            }





        }
    }
}
