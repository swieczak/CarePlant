using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarePlant.Model
{
    public class Flower
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public Species Species { get; set; }

        public Flower(string name, int id, string species, int speciesID, string familyName, int familyID)
        {
            Name = name;
            ID = id;
            Species = new Species(species, speciesID, familyName, familyID);
        }

        public override string ToString()
        {
            return $"{Name} ({Species})";
        }

        

    }
}
