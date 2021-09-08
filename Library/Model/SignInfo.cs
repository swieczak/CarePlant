using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarePlant.Model
{
    public class SignInfo
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Nick { get; set; }
        public string Password { get; set; }


        public SignInfo(string name, string surname, string nick, string password)
        {
            Name = name;
            Surname = surname;
            Nick = nick;
            Password = password;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
