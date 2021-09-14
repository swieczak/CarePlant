using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarePlant.Model
{
    public class Details
    {
        public String position;
        public String watering;
        public String subsoil;
        public String fertilization;
        public String difficulty;
        public String decoration;
        public String moistruizel;

        public Details(String Position, String Watering, String Subsoil, String Fertilization, String Difficulty, String Decoration, String Moistruizel)
        {
            position = Position;
            watering = Watering;
            subsoil = Subsoil;
            fertilization = Fertilization;
            difficulty = Difficulty;
            decoration = Decoration;
            moistruizel = Moistruizel;
        }
        public Details() { }

    }
}
