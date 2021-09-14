using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using CarePlant.Model.DAL;

using CarePlant.Model.DAL;

namespace CarePlant.Model
{
    public class Todo
    {
        private int idAkcji;
        private String nazwaAkcji;
        private int idKwiat;
        private String nazwaKwiat;
        private DateTime ostWyk;
        private int nastWyk;

        public Todo(int IDAkcji, String NazwaAkcji, int IDKwiat, String NazwaKwiat, DateTime OstWyk, int NastWyk)
        {
            idAkcji = IDAkcji;
            nazwaAkcji = NazwaAkcji;
            idKwiat = IDKwiat;
            nazwaKwiat = NazwaKwiat;
            ostWyk = OstWyk.Date;
            nastWyk = NastWyk;
        }

        public int IDAkcji 
        {
            get { return idAkcji; }
            set { idAkcji = value; }
        }
        public int IDFlower
        {
            get { return idKwiat; }
            set { idKwiat = value; }
        }

        public DateTime next()
        {
            return ostWyk.AddDays(nastWyk).Date;
        }

        public override string ToString()
        {
            return $"{nazwaKwiat}: {nazwaAkcji}, {this.next().ToString("dd.MM.yyyy r.")}";
        }

        public bool isUrgent()
        {
            int result = DateTime.Compare(ostWyk.AddDays(nastWyk), DateTime.Today.Date);
            return (result < 0);
        }

        public void Perform()
        {
            DataAccess dataAccess = new DataAccess();
            dataAccess.Perform(this.idKwiat, this.idAkcji);

            int result = DateTime.Compare(ostWyk.AddDays(nastWyk), DateTime.Today.Date);
            if(result > 0)
                dataAccess.Delay(this.idKwiat, this.idAkcji, (this.nastWyk + DateTime.Compare(ostWyk, DateTime.Today.Date)));
        }

        public void Delay()
        {
            DataAccess dataAccess = new DataAccess();
            dataAccess.Delay(this.idKwiat, this.idAkcji, (this.nastWyk+ (int)(nastWyk/2)));
        }

    }
}
