using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ProjektAsBankkonto.Datenhaltung.Model
{
    class Filiale
    {
        public int FilialeNr { get; set; }
        public string Blz
        {
            get
            {
                return this.Blz;
            }
            set
            {
                Regex rgx = new Regex(@"[^\d]", RegexOptions.IgnoreCase);
                MatchCollection matches = rgx.Matches(value);
                if(value.Length != 8 && matches.Count > 0)
                {
                    throw new FormatException();
                }

                this.Blz = value;
            }
        }
    }
}
