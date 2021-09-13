using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;

namespace CarePlant.Model.DAL
{
    using CarePlant.Model;
    using System.Security.Cryptography.X509Certificates;

    class DataAccess
    {
        static MySqlConnectionStringBuilder connStringBuilder;
        static MySqlConnection connection;

        private string GET_FAMILIES_QUERY = "SELECT `id_rodziny`, `nazwa` FROM `rodziny` ";

        public DataAccess()
        {
            // Connection string
            connStringBuilder = new MySqlConnectionStringBuilder();

            connStringBuilder.UserID = "root";
            connStringBuilder.Password = "";
            connStringBuilder.Server = "localhost";
            connStringBuilder.Database = "kwiotki";
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
                MySqlCommand command = new MySqlCommand($"SELECT * FROM gatunki_ext WHERE id_rodziny = {idRodziny}", connection);

                connection.Open();
                var dataReader = command.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        species.Add(new Species(
                            dataReader["nazwa_gatunku"].ToString(), (int)dataReader["id_gatunki"], dataReader["nazwa_rodziny"].ToString(), (int)dataReader["id_rodziny"]
                            ) );
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
                MySqlCommand command1 = new MySqlCommand($"SELECT `user`, `imie`, `nazwisko` FROM `osoby` WHERE `user` = '{signInfo.Nick}'", connection); //OR `imie` = {signInfo.Name} OR `nazwisko` = {signInfo.Surname}", connection);
                //komenda dodająca użytkownika (nie wiem czy działa)
                MySqlCommand command2 = new MySqlCommand($"INSERT INTO `osoby` (user, imie, nazwisko, password) VALUES('{signInfo.Nick}', '{signInfo.Name}', '{signInfo.Surname}', MD5('{signInfo.Password}'))", connection);

                connection.Open();
                var dataReader = command1.ExecuteReader();
                if (dataReader.HasRows)
                {
                    connection.Close();
                    
                    return signed;
                    // dodaj rekord do tablicu osoby
                    //var dataExecutor = command2.metodawywołującakomendę();
                }
                connection.Close();
                connection.Open();
                command2.ExecuteNonQueryAsync();
                connection.Close();
            }
            signed = true;
            return signed;
        }


        public static int logging(LogInInfo loginInfo)
        {
            int signed = 0;

            using (connection = new MySqlConnection(connStringBuilder.ToString()))
            {
                //komenda sprawdzająca istnienie danych w bazie
                MySqlCommand command1 = new MySqlCommand($"SELECT `id_osoby` FROM `osoby` WHERE `user` = '{loginInfo.Nick}' AND `password` = MD5('{loginInfo.Password}')", connection);
                connection.Open();
                var dataReader = command1.ExecuteReader();
                if (dataReader.HasRows)
                {
                    dataReader.Read();
                    signed = (int)dataReader["id_osoby"];
                }
                /*else
                {
                    System.Windows.MessageBox.Show("Nieudane logowanie!");
                }*/
                connection.Close();
            }
            return signed;
        }


        public List<Flower> GetFlowers(LogInInfo loginInfo)
        {
            List<Flower> Flowers = new List<Flower>();
            using (connection = new MySqlConnection(connStringBuilder.ToString()))
            {
                
                //komenda sprawdzająca istnienie danych w bazie
                MySqlCommand command1 = new MySqlCommand($"SELECT * FROM zestaw_szczegolowy WHERE fk_osoby ={loginInfo.ID}", connection);
                
                connection.Open();
                var dataReader = command1.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                        Flowers.Add(new Flower(dataReader["nazwa"].ToString(), (int)dataReader["id_zestaw"], 
                            dataReader["gatunek"].ToString(), (int)dataReader["id_gatunku"], 
                            dataReader["nazwa_rodziny"].ToString(), (int)dataReader["id_rodziny"]));
                }
                connection.Close();
            }
            return Flowers;
        }

        public List<Todo> GetToDoList(LogInInfo loginInfo)
        {
            List<Todo> Todos = new List<Todo>();
            using(connection = new MySqlConnection(connStringBuilder.ToString()))
            {
                MySqlCommand command1 = new MySqlCommand($"SELECT * FROM todosy WHERE fk_osoby ={loginInfo.ID} ORDER BY ost_wykonanie", connection);

                connection.Open();
                var dataReader = command1.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                        Todos.Add(new Todo((int)dataReader["id_akcji"], dataReader["opis_akcji"].ToString(),(int)dataReader["id_zestaw"], dataReader["nazwa"].ToString(),
                            (DateTime)dataReader["ost_wykonanie"], (int)dataReader["kiedy_wykonac"] ));
                }
                connection.Close();
            }

            return Todos;
        }
        
        public bool AddFlower(LogInInfo loginInfo, Species species, String name)
        {
            if (species == null || name == null || name == "")
                return false;
            using (connection = new MySqlConnection(connStringBuilder.ToString()))
            {
                MySqlCommand command1 = new MySqlCommand($"SELECT * FROM `kto_ma_co` WHERE `fk_osoby` = {loginInfo.ID} AND `fk_kwiat` = { species.ID} AND `nazwa` = '{name}'", connection); //OR `imie` = {signInfo.Name} OR `nazwisko` = {signInfo.Surname}", connection);

                MySqlCommand command = new MySqlCommand($"INSERT INTO `kto_ma_co` (fk_osoby, fk_kwiat, nazwa) VALUES( { loginInfo.ID }, { species.ID}, '{name}')", connection);

                connection.Open();
                var dataReader = command1.ExecuteReader();
                if (dataReader.HasRows)
                {
                    connection.Close();
                    return false;
                }
                connection.Close();

                connection.Open();
                command.ExecuteNonQueryAsync();
                connection.Close();
            }
            return true;
        }

        public bool DeleteFlower(Flower flower)
        {
            
            using (connection = new MySqlConnection(connStringBuilder.ToString()))
            {
                MySqlCommand command = new MySqlCommand($"DELETE FROM kto_ma_co WHERE id_zestaw = {flower.ID};", connection);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            return true;
        }

        public bool EditFlower(Flower flower, Species species, String nazwa)
        {

            using (connection = new MySqlConnection(connStringBuilder.ToString()))
            {
                MySqlCommand command = new MySqlCommand($"UPDATE kto_ma_co SET nazwa = '{nazwa}', fk_kwiat = {species.ID} WHERE id_zestaw = {flower.ID} ", connection);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            return true;
        }

        public void Perform(int flowerID, int actionID)
        {
            using (connection = new MySqlConnection(connStringBuilder.ToString()))
            {
                MySqlCommand command = new MySqlCommand($"INSERT INTO `dzienniki_akcji` ( `fk_akcji`, `fk_zestaw`) VALUES ('{actionID}', '{flowerID}');", connection);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }

        }

        public void Delay(int flowerID, int actionID, int interval)
        {
            using (connection = new MySqlConnection(connStringBuilder.ToString()))
            {
                MySqlCommand command = new MySqlCommand($"UPDATE `zadania` SET `kiedy_wykonac` = {interval} WHERE fk_zestaw = {flowerID} AND fk_akcji = {actionID};", connection);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }

        }

    }
}
