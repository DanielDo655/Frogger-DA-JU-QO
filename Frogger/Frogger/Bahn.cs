using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frogger
{
    public class Bahn
    {
        private int spawnRate;
        private int spawnZaehler;
        private int breite;
        private int hoehe;
        private int speed;

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
        public int SpawnZaehler
        {
            get
            {
                return hoehe;
            }
            set
            {
                spawnZaehler = value;
            }
        }
        public int Speed
        {
            get
            {
                return speed;
            }
            set
            {
                speed = value;
            }
        }
    }
}
