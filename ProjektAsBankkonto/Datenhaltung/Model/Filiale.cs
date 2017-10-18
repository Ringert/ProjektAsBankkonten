using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektAsBankkonto.Datenhaltung.Model
{
    class Filiale
    {
        //Wird zur Minimierung der Datenhaltungszugriffe genutzt
        public static List<Filiale> Instances = new List<Filiale>();
        public int FilialeNr { get; set; }
        public string Blz
        {
            get
            {
                return this.Blz;
            }
            set
            {
                this.Blz = value;
            }
        }
    }
}
