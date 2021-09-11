using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarePlant.Model
{
    public class Users
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public string Surname { get; set; }
        public string Nickname { get; set; }

        public string Password { get; set; }
        public Users(string name, int id, string surname, string nickname, string password)
        {
            Name = name;
            ID = id;
            Surname = surname;
            Nickname = nickname;
            Password = password;

        }

    }
}
