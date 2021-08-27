
using CarePlant.ViewModels;
using CarePlant.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CarePlant.Commands
{
   public class LogInSectionCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public MainWindow MainWindow { get; set; }
        public LogInSectionCommand(MainWindow MainWindow)
        {
            this.MainWindow = MainWindow;
        }
        public void Execute(object parameter)
        {
            LogInViewModel LoginViewModel = new LogInViewModel();
            
            LogInWindow LoginWindow = new LogInWindow(LoginViewModel);
            MainWindow.Close();
            LoginWindow.ShowDialog();
        }
    }
}
