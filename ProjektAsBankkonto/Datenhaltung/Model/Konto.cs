using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ProjektAsBankkonto.Datenhaltung.Model
{
    class Konto
    {
        //Wird zur Minimierung der Datenhaltungszugriffe genutzt
        public static List<Konto> Instances = new List<Konto>();
        public string KontoNr 
        {
            get
            {
                return this.KontoNr;
            } 
            set
            {
                Regex rgx = new Regex(@"[^\d]", RegexOptions.IgnoreCase);
                MatchCollection matches = rgx.Matches(value);
                if (value.Length != 8 || matches.Count > 0)
                {
                    throw new FormatException();
                }

                this.KontoNr = value;
            }
        }
    }
}
