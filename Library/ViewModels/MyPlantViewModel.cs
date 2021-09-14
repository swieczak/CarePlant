using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarePlant.Commands;
using System.ComponentModel;
using CarePlant.Views;


using System.Threading.Tasks;

namespace CarePlant.ViewModels
{
    using Model;
    public class MyPlantViewModel : INotifyPropertyChanged
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

        public MyPlantWindow myPlantWindow { get; set; }
        public MyPlantViewModel()
        {
        }
    }
   
}
