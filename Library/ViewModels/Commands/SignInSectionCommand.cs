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
   public class SignInSectionCommand:ICommand
    {
        public event EventHandler CanExecuteChanged;
        private readonly SignInViewModel _viewModel;
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public MainWindow MainWindow { get; set; }
        public SignInSectionCommand(MainWindow MainWindow)
        {
            this.MainWindow = MainWindow;
        }
        public void Execute(object parameter)
        {
            SignInViewModel signViewModel = new SignInViewModel();

            SignInWindow signinWindow = new SignInWindow(signViewModel);
            MainWindow.Close();
            signinWindow.ShowDialog();
        }
    }
}
