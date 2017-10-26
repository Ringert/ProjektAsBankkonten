using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektAsBankkonto.Datenhaltung.Model
{
    public class Filiale
    {
        //Wird zur Minimierung der Datenhaltungszugriffe genutzt
        public static Dictionary<int, Filiale> Instances = new Dictionary<int, Filiale>();

        private string blz;

        public int FilialeNr { get; set; }
        public string Blz
        {
            get
            {
                return this.blz;
            }
            set
            {
                this.blz = value;
            }
        }
    }
}
