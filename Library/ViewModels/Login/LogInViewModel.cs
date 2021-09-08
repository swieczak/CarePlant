using System;
using CarePlant.Commands;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

using CarePlant.Views;

namespace CarePlant.ViewModels
{
    using System.Windows.Input;
    using CarePlant.Model.DAL;
    using Model;
    using ViewModels.BaseClass;
    public class LogInViewModel : System.ComponentModel.INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private Model.DAL.DataAccess dataAccess = new Model.DAL.DataAccess();
        static public LogInInfo logInfo = new LogInInfo("", "");


        private string _userNick;

        public string UserNick
        {
            get { return _userNick; }
            set
            {
                _userNick = value;
                logInfo.Nick = _userNick;
                OnPropertyChanged("UserNick");
            }
        }

        private string _userPass;

        public string UserPass
        {
            get { return _userPass; }
            set
            {
                _userPass = value;
                logInfo.Password = _userPass;
                OnPropertyChanged("UserPass");
            }
        }




        public LogInWindow LogInWindow { get; set; }
        public LogInViewModel()
        {
            this.LogInWindow = LogInWindow;
        }
        public UserInterfaceCommand UserInterfaceSectionCommand => new UserInterfaceCommand(LogInWindow);


        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
