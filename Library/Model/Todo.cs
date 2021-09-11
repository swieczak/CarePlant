using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public DateTime Next()
        {
            return ostWyk.AddDays(nastWyk).Date;
        }

        public override string ToString()
        {
            return $"{nazwaKwiat}: {nazwaAkcji}, {this.Next().ToString("dd.MM.yyyy r.")}";
        }

        public bool isUrgent()
        {
            int result = DateTime.Compare(ostWyk.AddDays(nastWyk), DateTime.Today.Date);
            return (result < 0);
        }

    }
}
