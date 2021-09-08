using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarePlant.Model
{
    public class LogInInfo
    {
        public string Nick { get; set; }
        public string Password { get; set; }


        public LogInInfo(string nick, string password)
        {
            Nick = nick;
            Password = password;
        }

        public override string ToString()
        {
            return Nick;
        }
    }
}
