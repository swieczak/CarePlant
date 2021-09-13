using CarePlant.Commands;
using CarePlant.Core;
using CarePlant.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarePlant.ViewModels
{
    public class WarningViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string errorText;

        public string ErrorText
        {
            get { return errorText; }
            set 
            { 
                errorText = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ErrorText)));
            }
        }

        public Warning Warning { get; set; }
        public WarningViewModel()
        {
        }
    }
}