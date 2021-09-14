using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarePlant.Model
{
    public class ActionLog
    {
        private int idAkcji;
        private String nazwaAkcji;
        private int idKwiat;
        private DateTime czas;

        public ActionLog(int IDAkcji, String NazwaAkcji, int IDKwiat, DateTime Czas)
        {
            idAkcji = IDAkcji;
            nazwaAkcji = NazwaAkcji;
            idKwiat = IDKwiat;
            czas = Czas.Date;
        }
        public override string ToString()
        {
            return $"{czas.ToString("dd.MM.yyyy")} {nazwaAkcji}";
        }
    }
}
