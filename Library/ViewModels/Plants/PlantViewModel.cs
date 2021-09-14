using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CarePlant.Commands;


namespace CarePlant.ViewModels
{
    using CarePlant.Views;
    using Model;
    using Model.DAL;
    using System.ComponentModel;
    //using ViewModels.BaseClass;
    public class PlantViewModel: System.ComponentModel.INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private DataAccess dataAccess;

        private List<Family> families;
        private Family currentFamily;

        private List<Species> species;
        private Species currentSpecies;

        private List<Flower> flowers;
        private Flower currentFlower;

        private String currentName;
        public PlantViewModel()
        {
            dataAccess = new DataAccess();
        }


        public List <Family> Families
        {
            get
            {
                if( families == null)
                {
                    families = dataAccess.getFamilies();
                }
                return families;
            }
        }

        public Family CurrentFamily
        {
            get { return currentFamily; }
            set
            {
                currentFamily = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentFamily)));
                Species = dataAccess.getSpecies(currentFamily.ID);
                Console.WriteLine("currentFamily set");
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
       // /*
        public Species CurrentSpecies
        {
            get { return currentSpecies; }
            set
            {
                currentSpecies = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentSpecies)));
                Console.WriteLine("currentSpecies set");
            }
        }



        public List<Flower> Flowers
        {
            get
            {
                if (flowers == null)
                {
                    flowers = dataAccess.GetFlowers(LogInViewModel.logInfo);
                }
                return flowers;
            }
            set
            {
                flowers = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Flowers)));
            }
        }

        public String CurrentName
        {
            get { return currentName; }
            set
            {
                currentName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentName)));
                Console.WriteLine("currentName set");
            }
        }


        public Flower CurrentFlower
        {
            get { return currentFlower; }
            set
            {
                currentFlower = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentFlower)));
                Console.WriteLine("currentFlower set");
                if (currentFlower != null)
                { 
                    foreach(Family fam in families)
                    {
                        if(currentFlower.Species.Family.ID == fam.ID)
                        {
                            Console.WriteLine("Found eqal fam");
                            CurrentFamily = fam;
                            //Species = dataAccess.getSpecies(currentFamily.ID);
                        }
                    }
                    Console.WriteLine("Searching for eqal spec");

                    CurrentName = currentFlower.Name;

                    foreach (Species spec in species)
                    {
                        if (currentFlower.Species.ID == spec.ID)
                        {
                            Console.WriteLine("Found eqal spec");
                            CurrentSpecies = spec;
                        }
                    }
                }

            }
        }

        private ICommand _add = null;
        public ICommand Add
        {
            get
            {
                return _add ?? (_add = new BaseClass.RelayCommand(
                    (p) => 
                    { 
                        dataAccess.AddFlower(LogInViewModel.logInfo, currentSpecies, currentName); 
                        Flowers = dataAccess.GetFlowers(LogInViewModel.logInfo); 
                    }
                    ,
                    p => true)
                    );
            }
        }
        private ICommand _del = null;
        public ICommand Del
        {
            get
            {
                return _del ?? (_del = new BaseClass.RelayCommand(
                    (p) =>
                    {
                        if (currentFlower != null)
                        {
                            dataAccess.DeleteFlower(currentFlower);
                            Flowers = dataAccess.GetFlowers(LogInViewModel.logInfo);
                        }
                        // else wyświetl komunikat wybierz roślinę z listy
                    }
                    ,
                    p => true)
                    );
            }
        }
        private ICommand _edi = null;
        public ICommand Edi
        {
            get
            {
                return _edi ?? (_edi = new BaseClass.RelayCommand(
                    (p) =>
                    {
                        if (currentFlower != null)
                        {
                            if (currentFlower.Name != currentName || currentFlower.Species != currentSpecies)
                            {
                                dataAccess.EditFlower(currentFlower, currentSpecies, currentName);
                                Flowers = dataAccess.GetFlowers(LogInViewModel.logInfo);
                            }
                        }
                        // else wyświetl komunikat wybierz roślinę z listy
                    }
                    ,
                    p => true)
                    );
            }
        }


        private ICommand _act = null;
        public ICommand Act
        {
            get
            {
                return _act ?? (_act = new BaseClass.RelayCommand(
                    (p) =>
                    {
                        if (currentFlower != null)
                        {
                            int idAkcji = int.Parse((string)p);
                            List<Todo> todosy = dataAccess.GetToDoList(LogInViewModel.logInfo);
                            bool undone = true;
                            foreach (Todo todo in todosy)
                            {
                                if (todo.IDAkcji == idAkcji && todo.IDFlower == currentFlower.ID)
                                {
                                    todo.Perform();
                                    undone = false;
                                }
                            }
                            if (undone)
                                dataAccess.Perform(currentFlower.ID, idAkcji);

                            //Wyświetl komunikat o zapisanej akcji


                        }
                        // else wyświetl komunikat wybierz roślinę z listy

                    }
                    ,
                    p => true)
                    );
            }
        }


        // */



































    }
}
