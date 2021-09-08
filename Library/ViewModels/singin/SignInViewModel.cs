
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace CarePlant.ViewModels
{
    using System.Windows.Input;
    using Model;
    using ViewModels.BaseClass;
    public class SignInViewModel : System.ComponentModel.INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private Model.DAL.DataAccess dataAccess = new Model.DAL.DataAccess();
        private SignInfo signInfo = new Model.SignInfo("","","","");

        private string _userName;

        public string UserName
        {
            get { return _userName; }
            set 
            { 
                _userName = value;
                signInfo.Name = _userName;
                OnPropertyChanged("UserName");
            }
        }

        private string _userSurname;

        public string UserSurname
        {
            get { return _userSurname; }
            set 
            {
                _userSurname = value;
                signInfo.Surname = _userSurname;
                OnPropertyChanged("UserSurname");
            }
        }


        private string _userNickname;

        public string UserNickname
        {
            get { return _userNickname; }
            set 
            {
                _userNickname = value;
                signInfo.Nick = _userNickname;
                OnPropertyChanged("UserNickname");
            }
        }



        private string _userPassword;

        public string UserPassword
        {
            get { return _userPassword; }
            set 
            {
                _userPassword = value;
                signInfo.Password = _userPassword;
                OnPropertyChanged("UserPassword");
            }
        }

        private ICommand _signing = null;

        //Wlasnosc typu ICommand reprezentującą polecenie,
        //które wykonuje obliczenie wyniku
        //pod warunkiem, że argumenty oraz opracja zostały podane
        public ICommand Signing
        {
            //do stworzenia obiektu polecenie użyjemy pomocniczej klasy RelayCommand
            get
            {
                // jesli nie jest określone polecenie to tworzymy je i zwracamy poprozez 
                //pomocniczy typ RelayCommand
                return _signing ?? (_signing = new BaseClass.RelayCommand(
                    //co wykonuje polecenie
                    //które należy sprecyzować w dalszej części działania
                    (p) => { dataAccess.signing(signInfo); }
                    ,
                    //warunek kiedy może je wykonać
                    p => true)
                    );
            }
        }


        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
