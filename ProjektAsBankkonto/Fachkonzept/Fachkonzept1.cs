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
        /********************************* Kunde *********************************/
        public bool saveKunde(Kunde kunde)
        {
            int? kundeNr = kunde.KundeNr;
            if(kundeNr == null) {
                return this.Datenhaltung.addKunde(kunde);
            } else {
                return this.Datenhaltung.editKunde(kunde);
            }
        }
        public bool deleteKunde(Kunde kunde)
        {
            return this.Datenhaltung.deleteKunde(kunde);
        }
        public Kunde getKunde(int kundeNr)
        {
            return this.Datenhaltung.fetchKunde(kundeNr);
        }
        public Dictionary<int, Kunde> getAllKunden()
        {
            return this.Datenhaltung.fetchAllKunden();
        }
        public Dictionary<int, Kunde> getRangeOfKunden(int nr, int offset)
        {
            return this.Datenhaltung.fetchRangeOfKunden(nr, offset);
        }
        public int getCountKunden()
        {
            return this.Datenhaltung.getCountKunden();
        }
        /********************************* Filiale *********************************/

        public bool saveFiliale(Filiale filiale)
        {
            int? kundeNr = filiale.FilialeNr;
            if (kundeNr == null)
            {
                return this.Datenhaltung.addFiliale(filiale);
            }
            else
            {
                return this.Datenhaltung.editFiliale(filiale);
            }
        }
        public bool deleteFiliale(Filiale filiale)
        {
            return this.Datenhaltung.deleteFiliale(filiale);
        }
        public Filiale getFiliale(int filialeNr)
        {
            return this.Datenhaltung.fetchFiliale(filialeNr);
        }
        public Dictionary<int, Filiale> getAllFilialen()
        {
            return this.Datenhaltung.fetchAllFilialen();
        }
        public Dictionary<int, Filiale> getRangeOfFilalen(int nr, int offset)
        {
            return this.Datenhaltung.fetchRangeOfFilialen(nr, offset);
        }
        public int getCountFilialen()
        {
            return this.Datenhaltung.getCountFilialen();
        }
        /********************************* Konto *********************************/
        public bool addKonto(Konto konto)
        {
            if (this.Datenhaltung.checkKontoNrExists(konto.KontoNr))
            {
                return false;
            }
            else
            {
                return this.Datenhaltung.addKonto(konto);
            }
        }
        public bool updateKonto(string kontoNr, Konto konto)
        {
            if (kontoNr != konto.KontoNr && this.Datenhaltung.checkKontoNrExists(konto.KontoNr))
            {
                return false;
            }
            else
            {
                return this.Datenhaltung.editKonto(kontoNr, konto);
            }
        }
        public bool deleteKonto(Konto konto)
        {
            return this.Datenhaltung.deleteKonto(konto);
        }
        public Dictionary<string, Konto> getAllKonten(Kunde kunde)
        {
            return this.Datenhaltung.fetchAllKonten(kunde);
        }
        public int getCountKontent(Kunde kunde)
        {
            return this.Datenhaltung.getCountKonten(kunde);
        }
    }
}
