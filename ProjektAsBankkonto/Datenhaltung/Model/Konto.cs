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
        private string kontoNr;
        public Filiale Filiale;
        public Kunde Kunde;

        public string KontoNr 
        {
            get
            {
                return this.kontoNr;
            } 
            set
            {
                Regex rgx = new Regex(@"^[\d]{8}$", RegexOptions.IgnoreCase);
                MatchCollection matches = rgx.Matches(value);
                if (matches.Count == 1)
                {
                    throw new FormatException();
                }

                this.kontoNr = value;
            }
        }
    }
}
