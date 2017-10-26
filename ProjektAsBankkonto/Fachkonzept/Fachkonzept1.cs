using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProjektAsBankkonto.Interfaces.Fachkonzept;
using ProjektAsBankkonto.Interfaces.Datenhaltung;
using ProjektAsBankkonto.Datenhaltung.Model;

namespace ProjektAsBankkonto.Fachkonzept
{
    class Fachkonzept1 : IFachkonzept
    {
        public IDatenhaltung Datenhaltung { get; set; }

        public Fachkonzept1 (IDatenhaltung datenhaltung)
        {
            this.Datenhaltung = datenhaltung;
        }

        public bool saveKunde(ref Kunde kunde)
        {
            int? kundeNr = kunde.KundeNr;
            if(kundeNr == null) {
                return this.Datenhaltung.addKunde(ref kunde);
            } else {
                return this.Datenhaltung.editKunde(kunde);
            }
        }
    }
}
