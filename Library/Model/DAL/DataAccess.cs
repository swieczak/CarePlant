using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;

namespace CarePlant.Model.DAL
{
    using CarePlant.Model;
    class DataAccess
    {
        MySqlConnectionStringBuilder connStringBuilder;
        MySqlConnection connection;

        private string GET_FAMILIES_QUERY = "SELECT `id_rodziny`, `nazwa` FROM `rodziny` ";

        public DataAccess()
        {
            // Connection string
            connStringBuilder = new MySqlConnectionStringBuilder();

            connStringBuilder.UserID = "root";
            connStringBuilder.Password = "";
            connStringBuilder.Server = "localhost";
            connStringBuilder.Database = "kwiatki";
            connStringBuilder.Port = 3306;

        }

        public List <Family> getFamilies()
        {
            List <Family> families = new List<Family>();
            using(connection = new MySqlConnection(connStringBuilder.ToString()))
            {
                MySqlCommand command = new MySqlCommand(GET_FAMILIES_QUERY, connection);
                connection.Open();
                var dataReader = command.ExecuteReader();
                if(dataReader.HasRows)
                {
                    while(dataReader.Read())
                    {
                        families.Add(new Family(dataReader["nazwa"].ToString(), (int)dataReader["id_rodziny"]));
                    }
                }
            }

            return families;
        }

        public List<Species> getSpecies(int idRodziny)
        {
            List<Species> species = new List<Species>();
            using (connection = new MySqlConnection(connStringBuilder.ToString()))
            {
                MySqlCommand command = new MySqlCommand($"SELECT `id_gatunki`, `nazwa`, `fk_rodziny` FROM `gatunki` WHERE `fk_rodziny` = {idRodziny}", connection);

                connection.Open();
                var dataReader = command.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        species.Add(new Species(dataReader["nazwa"].ToString(), (int)dataReader["id_gatunki"], (int)dataReader["fk_rodziny"] ) );
                    }
                }
            }

            return species;
        }


        public bool signing(SignInfo signInfo)
        {
            bool signed = false;
            /*Console.WriteLine(signInfo.Name);
            Console.WriteLine(signInfo.Surname);
            Console.WriteLine(signInfo.Nick);
            Console.WriteLine(signInfo.Password);*/

            using (connection = new MySqlConnection(connStringBuilder.ToString()))
            {
                //komenda sprawdzająca istnienie danych w bazie
                MySqlCommand command1 = new MySqlCommand($"SELECT `user`, `imie`, `nazwisko` FROM `osoby` WHERE `user` = {signInfo.Nick} OR `imie` = {signInfo.Name} OR `nazwisko` = {signInfo.Surname}", connection);
                //komenda dodająca użytkownika (nie wiem czy działa)
                MySqlCommand command2 = new MySqlCommand($"INSERT INTO `osoby` (user, imie, nazwisko, password) VALUES('{signInfo.Nick}', '{signInfo.Name}', '{signInfo.Surname}', '{signInfo.Password}')", connection);

                connection.Open();
                var dataReader = command1.ExecuteReader();
                if (dataReader.HasRows)
                {
                    return signed;
                    // dodaj rekord do tablicu osoby
                    //var dataExecutor = command2.metodawywołującakomendę();
                }
            }
            signed = true;
            return signed;
        }


        public bool logging(LoginInfo loginInfo)
        {
            bool signed = false;

            using (connection = new MySqlConnection(connStringBuilder.ToString()))
            {
                //komenda sprawdzająca istnienie danych w bazie
                MySqlCommand command1 = new MySqlCommand($"SELECT `user`,`password` FROM `osoby` WHERE `user` = {loginInfo.Nick} AND `password` = {loginInfo.Password}", connection);

                connection.Open();
                var dataReader = command1.ExecuteReader();
                if (dataReader.HasRows)
                {
                    signed = true;
                }
            }
            return signed;
        }

    }
}
