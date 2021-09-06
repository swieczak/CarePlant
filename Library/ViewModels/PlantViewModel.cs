using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarePlant.ViewModels
{
    using Model;
    using Model.DAL;
    public class PlantViewModel
    {
        private DataAccess dataAccess;

        public List <Family> Families
        {
            get
            {
                return dataAccess.getFamilies();
            }
        }

        public PlantViewModel()
        {
            dataAccess = new DataAccess();
        }
    }
}
