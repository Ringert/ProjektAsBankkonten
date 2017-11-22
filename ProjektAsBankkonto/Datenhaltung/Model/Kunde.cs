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
        private DateTime geburtsdatum;
        private string plz;
        private string strasse;
        private string ort;
        private string vorname;
        private string nachname;
        public Dictionary<string, Konto> Konten = new Dictionary<string, Konto>();

        public int KundeNr { get; set; }
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
                    throw new FormatException("Der Straßenname ist zu lang: max. 100 Zeichen");
                }
                this.strasse = value;
            }
        }

        public string Plz {
            get {
                return this.plz;
            }
            set {
                if (value.Length > 10 && value.Length < 3)
                {
                    throw new FormatException("Die Postleitzahl entspricht nicht den Vorgaben: zwischen 3 und 10 Zeichen");
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
                    throw new FormatException("Der Ortsname ist zu lang: max 100 Zeichen");
                }
                this.ort = value;
            }
        }
        public Laender Land { get; set; }
        public Geschlechter Geschlecht { get; set; }
        public string Vorname
        {
            get
            {
                return this.vorname;
            }
            set
            {
                if (value.Length > 255)
                {
                    throw new FormatException("Der Vorname ist zu lang: max. 255 Zeichen");
                }
                this.vorname = value;
            }
        }
        public string Nachname
        {
            get
            {
                return this.nachname;
            }
            set
            {
                if (value.Length > 255)
                {
                    throw new FormatException("Der Nachname ist zu lang: max. 255 Zeichen");
                }
                this.nachname = value;
            }
        }
        public DateTime Geburtsdatum 
        { 
            get
            {
                return this.geburtsdatum;        
            }
            set
            {

                DateTime now    =   DateTime.Now;
                int age         =   now.Year - value.Year;

                if (now.Month < value.Month || (now.Month == value.Month && now.Day < value.Day))
                    age--;
                if(age < 18)
                    throw new FormatException("Ein Kontoinhaber muss mind. 18 Jahre alt sein");
            
                this.geburtsdatum = value;
            } 
        }
    }
}
