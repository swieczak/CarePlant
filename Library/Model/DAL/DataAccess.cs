using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;

namespace CarePlant.Model.DAL
{
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
                MySqlCommand command = new MySqlCommand($"SELECT `id_rodziny`, `nazwa`, `fk_rodziny` FROM `rodziny` WHERE `fk_rodziny` = {idRodziny}", connection);
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




    }
}
