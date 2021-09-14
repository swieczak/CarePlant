using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarePlant.Commands;
using System.ComponentModel;
using CarePlant.Views;
using CarePlant.Model.DAL;
using System.Threading.Tasks;

namespace CarePlant.ViewModels
{
    using Model;
    public class MyPlantViewModel : INotifyPropertyChanged
    {
        private DataAccess dataAccess;
        public event PropertyChangedEventHandler PropertyChanged;

        private Flower flower;
        private List<ActionLog> actionLogs = new List<ActionLog>();
        private Details details;
        public string Name
        {
            get { return flower.Name; }
            set
            {
                flower.Name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
            }
        }

        public List<ActionLog> ActionLogs
        {
            get 
            {
                return actionLogs;
            }
            set
            {
                actionLogs = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
            }
        }
        /*
        decoration;
        moistruizel;
        */
        public string Position
        {
            get { return details.position; }
            set
            {
                details.position = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Position)));
            }
        }
        public string Watering
        {
            get { return details.watering; }
            set
            {
                details.watering = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Watering)));
            }
        }
        public string Subsoil
        {
            get { return details.subsoil; }
            set
            {
                details.subsoil = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Subsoil)));
            }
        }
        public string Fertilization
        {
            get { return details.fertilization; }
            set
            {
                details.fertilization = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Fertilization)));
            }
        }
        public string Difficulty
        {
            get { return details.difficulty; }
            set
            {
                details.difficulty= value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Difficulty)));
            }
        }
        public string Decoration
        {
            get { return details.decoration; }
            set
            {
                details.decoration = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Decoration)));
            }
        }
        public string Moistruizel
        {
            get { return details.moistruizel; }
            set
            {
                details.moistruizel = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Moistruizel)));
            }
        }





        public MyPlantWindow myPlantWindow { get; set; }
        public MyPlantViewModel()
        {
            dataAccess = new DataAccess();
            flower = PlantViewModel.currentFlower;
            ActionLogs = dataAccess.GetActionLogs(flower);
            details = dataAccess.GetDetails(flower);
        }




    }
   
}
