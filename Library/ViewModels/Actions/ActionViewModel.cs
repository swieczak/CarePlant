using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarePlant.Model;
using CarePlant.Model.DAL;
using System.ComponentModel;
using System.Windows.Input;

namespace CarePlant.ViewModels
{
    public class ActionViewModel : System.ComponentModel.INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

    
        private DataAccess dataAccess;
       
        public ActionViewModel()
        {
            dataAccess = new DataAccess();
        }


        private List<Todo> urgentTodos;
        private List<Todo> soonTodos;
        private Todo selectedTodo;

        public List<Todo> UrgentTodos
        {
            get
            {
                if (urgentTodos == null)
                    split();
                
                return urgentTodos;
            }
            set
            {
                urgentTodos = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(UrgentTodos)));
            }
        }


        public Todo SelectedTodo
        {
            get { return selectedTodo;  }
            set 
            { 
                if(value != null)
                selectedTodo = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedTodo)));
                Console.WriteLine(selectedTodo);
            }
        }


        public List<Todo> SoonTodos
        {
            get
            {
                if (soonTodos == null)
                    split();

                return soonTodos;
            }
            set
            {
                soonTodos = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SoonTodos)));
            }
        }




        private void split()
        {
            List<Todo> wszystkie = dataAccess.GetToDoList(LogInViewModel.logInfo);
            urgentTodos = new List<Todo>();
            soonTodos = new List<Todo>();
            foreach(Todo i in wszystkie)
            {
                if (i.isUrgent())
                    urgentTodos.Add(i);
                else
                    soonTodos.Add(i);
            }
            UrgentTodos = urgentTodos;
            SoonTodos = soonTodos;

        }



        private ICommand _perform = null;
        public ICommand Perform
        {
            get
            {
                return _perform ?? (_perform = new BaseClass.RelayCommand(
                    (p) =>
                    {
                        selectedTodo.Perform();
                        this.split();
                    }
                    ,
                    p => true)
                    );
            }
        }
        private ICommand _delay = null;
        public ICommand Delay
        {
            get
            {
                return _delay ?? (_delay = new BaseClass.RelayCommand(
                    (p) =>
                    {
                        selectedTodo.Delay();
                        this.split();
                    }
                    ,
                    p => true)
                    );
            }
        }












    }
}
