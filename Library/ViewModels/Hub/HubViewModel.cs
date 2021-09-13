using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;
using CarePlant.Model;
using CarePlant.Model.DAL;
namespace CarePlant.ViewModels
{
    public class HubViewModel: System.ComponentModel.INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private DataAccess dataAccess;
        List<Flower> flowers = new List<Flower>();
        List<Todo> todos = new List<Todo>();
        public HubViewModel()
        {
            dataAccess = new DataAccess();
            split();
        }



        public List<Flower> Flowers
        {
            get
            {

                flowers = dataAccess.GetFlowers(LogInViewModel.logInfo);
                return flowers;
            }
            set
            {
                flowers = value;
            }
        }

        public List<Todo> Todos
        {
            get
            {
                
                return todos;
            }
            set
            {
                todos = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Todos)));
            }
        }

        private void split()
        {
            List<Todo> wszystkie = dataAccess.GetToDoList(LogInViewModel.logInfo);
            List<Todo> urgentTodos = new List<Todo>();
            foreach (Todo i in wszystkie)
            {
                if (i.isUrgent())
                    urgentTodos.Add(i);
            }
            Todos = urgentTodos;
        }

        private ICommand _performAll = null;
        public ICommand PerformAll
        {
            get
            {
                return _performAll ?? (_performAll = new BaseClass.RelayCommand(
                    (p) =>
                    {
                        foreach (Todo oneTodo in Todos)
                            oneTodo.Perform();
                        this.split();
                    }
                    ,
                    p => true)
                    );
            }
        }





    }
}
