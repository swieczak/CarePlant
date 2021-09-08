using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CarePlant.ViewModels
{
    using Model;
    using Model.DAL;
    using System.ComponentModel;
    using ViewModels.BaseClass;
    public class PlantViewModel: System.ComponentModel.INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private DataAccess dataAccess;
        private Family currentFamily;
        private List<Species> species;

        public PlantViewModel()
        {
            dataAccess = new DataAccess();
        }


        public List <Family> Families
        {
            get
            {
                return dataAccess.getFamilies();
            }
        }

        public Family CurrentFamily
        {
            get { return currentFamily; }
            set
            {
                currentFamily = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentFamily)));
                Console.WriteLine("currentFamily set");
                Species = dataAccess.getSpecies(currentFamily.ID);
            }
        }
                
        public List<Species> Species
        {
            get
            {
                if (currentFamily != null)
                    return dataAccess.getSpecies(currentFamily.ID);
                else
                    return new List<Species>();
            }
            set
            {
                species = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Species)));
                Console.WriteLine("Species set");
            }
        }







































    }
}
