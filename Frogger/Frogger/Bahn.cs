using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frogger
{
    public class Bahn
    {
        private int spawnRate;
        private int breite;
        private int hoehe;

        public int SpawnRate
        {
            get
            {
                return spawnRate;
            }
            set
            {
                spawnRate = value;
            }
        }
        public int Hoehe
        {
            get 
            { 
            return hoehe;
            }
            set
            {
                hoehe = value;
            }
        }
        public int Breite
        {
            get
            {
                return breite;
            }
            set
            {
                breite = value;
            }
        }


    }
}
