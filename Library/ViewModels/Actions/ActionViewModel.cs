using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarePlant.Model;
using CarePlant.Model.DAL;

namespace CarePlant.ViewModels
{
    public class ActionViewModel
    {
        private DataAccess dataAccess;
       
        public ActionViewModel()
        {
            dataAccess = new DataAccess();
        }


        private List<Todo> urgentTodos;
        private List<Todo> soonTodos;

        public List<Todo> UrgentTodos
        {
            get
            {
                if (urgentTodos == null)
                    split();
                
                return urgentTodos;
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
        }




        public void split()
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

        }


    }
}
