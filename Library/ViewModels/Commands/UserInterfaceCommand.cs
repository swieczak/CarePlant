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
            WarningViewModel WarningViewModel = new WarningViewModel();
            Warning Warning = new Warning(WarningViewModel);

            LogInViewModel.logInfo.ID = DataAccess.logging(LogInViewModel.logInfo);
            LogInViewModel.logInfo.Password = "";
            if (LogInViewModel.logInfo.ID != 0 && LogInViewModel.logInfo.Nick!="")
            {
                //LoginWindow.Close();
                Userinterface.ShowDialog();
            }
            else if(LogInViewModel.logInfo.Nick == "")
            {
                DialogBox.Show("Nor nick, nor password cannot be empty!");
            }
            else 
            {
                DialogBox.Show("Password or (and) nick are incorrect");
                //DialogBox.Show(String.Concat("LogInViewModel.logInfo.ID: ", LogInViewModel.logInfo.ID, 
                //    "\nLogInViewModel.logInfo.Nick: ", LogInViewModel.logInfo.Nick,
                //    "\nLogInViewModel.logInfo.Password", LogInViewModel.logInfo.Password));
            }
        }
    }
}
