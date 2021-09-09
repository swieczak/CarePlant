﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarePlant.Model
{
    public class Species

    {
        public string Name { get; set; }
        public int ID { get; set; }
        public Family Family { get; set; }


        public Species(string name, int id, string familyName, int familyID )
        {
            Name = name;
            ID = id;
            Family = new Family (familyName, familyID);

        }

        public override string ToString()
        {
            return Name;
        }

    }
}
