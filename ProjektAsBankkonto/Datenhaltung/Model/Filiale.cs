using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using ProjektAsBankkonto.Datenhaltung.Enums;

namespace ProjektAsBankkonto.Datenhaltung.Model
{
    public class Filiale
    {
        //Wird zur Minimierung der Datenhaltungszugriffe genutzt
        public static Dictionary<int, Filiale> Instances = new Dictionary<int, Filiale>();

        private string blz;
        private string plz;
        private string strasse;
        private string ort;
        private string name;

        public int FilialeNr { get; set; }
        public string Blz
        {
            get
            {
                return this.blz;
            }
            set
            {
                Regex rgx = new Regex(@"^[\d]{8}$", RegexOptions.IgnoreCase);
                MatchCollection matches = rgx.Matches(value);
                if (matches.Count != 1)
                {
                    throw new FormatException();
                }

                this.blz = value;
            }
        }
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                if (value.Length > 255)
                {
                    throw new FormatException();
                }
                this.name = value;
            }
        }
        public string Strasse
        {
            get
            {
                return this.strasse;
            }
            set
            {
                if (value.Length > 100)
                {
                    throw new FormatException();
                }
                this.strasse = value;
            }
        }

        public string Plz
        {
            get
            {
                return this.plz;
            }
            set
            {
                if (value.Length > 10 && value.Length < 3)
                {
                    throw new FormatException();
                }
                this.plz = value;
            }
        }
        public string Ort
        {
            get
            {
                return this.ort;
            }
            set
            {
                if (value.Length > 100)
                {
                    throw new FormatException();
                }
                this.ort = value;
            }
        }
        public Laender Land { get; set; }
    }
}
