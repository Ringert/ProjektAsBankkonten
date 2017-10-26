using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ProjektAsBankkonto.Datenhaltung.Model
{
    public class Konto
    {
        //Wird zur Minimierung der Datenhaltungszugriffe genutzt
        public static Dictionary<string, Konto> Instances = new Dictionary<string, Konto>();
        private string kontoNr;

        public string KontoNr 
        {
            get
            {
                return this.kontoNr;
            } 
            set
            {
                Regex rgx = new Regex(@"[^\d]", RegexOptions.IgnoreCase);
                MatchCollection matches = rgx.Matches(value);
                if (value.Length != 8 || matches.Count > 0)
                {
                    throw new FormatException();
                }

                this.kontoNr = value;
            }
        }
    }
}
