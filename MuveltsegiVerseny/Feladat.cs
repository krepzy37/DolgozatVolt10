using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuveltsegiVerseny
{
    class Feladat
    {
        public string Kerdes { get; set; }
        public int Megoldas { get; set; }
        public int Pontszam { get; set; }
        public string Teamkor { get; set; }

        public Feladat(string f)
        {
            var sor = f.Split(';');
            Kerdes = sor[0];
            Megoldas = int.Parse(sor[1]);
            Pontszam = int.Parse(sor[2]);
            Teamkor = sor[3]; 
        }
    }
}
