using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjektAsBankkonto.Datenhaltung.Enums;

namespace ProjektAsBankkonto.Datenhaltung.Model
{
    class Kunde
    {
        public int KundenNr { get; set; }
        public string Strasse { get; set; }
        public string Plz { get; set; }
        public string Ort { get; set; }
        public Laender Land { get; set; }
        public Geschlechter Geschlecht { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public DateTime Geburtsdatum 
        { 
            get
            {
                return this.Geburtsdatum;        
            } set
            {
                DateTime now    =   DateTime.Now;
                int age         =   now.Year - value.Year;

                if (now.Month < value.Month || (now.Month == value.Month && now.Day < value.Day))
                    age--;
                if(age < 18)
                    throw new FormatException();
            
                this.Geburtsdatum = value;
            } 
        }
    }
}
