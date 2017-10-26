using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjektAsBankkonto.Datenhaltung.Enums;

namespace ProjektAsBankkonto.Datenhaltung.Model
{
    public class Kunde
    {
        //Wird zur Minimierung der Datenhaltungszugriffe genutzt
        public static Dictionary<int, Kunde> Instances = new Dictionary<int, Kunde>();

        private DateTime geburtsdatum;
        private string plz;
        private string strasse;

        public int KundeNr { get; set; }
        public string Strasse
        {
            get
            {
                return this.strasse;
            }
            set
            {
                //TODO: höchsten 100 Zeichen
                this.strasse = value;
            }
        }

        public string Plz {
            get {
                return this.plz;
            }
            set {
                //TODO: höchsten 10 Zeichen
                this.plz = value;
            }
        }
        public string Ort { get; set; }
        public Laender Land { get; set; }
        public Geschlechter Geschlecht { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public DateTime Geburtsdatum 
        { 
            get
            {
                return this.geburtsdatum;        
            } set
            {

                DateTime now    =   DateTime.Now;
                int age         =   now.Year - value.Year;

                if (now.Month < value.Month || (now.Month == value.Month && now.Day < value.Day))
                    age--;
                if(age < 18)
                    throw new FormatException();
            
                this.geburtsdatum = value;
            } 
        }
    }
}
