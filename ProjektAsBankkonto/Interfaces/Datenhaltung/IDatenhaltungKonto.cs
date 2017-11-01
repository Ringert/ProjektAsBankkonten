using System.Collections.Generic;

using ProjektAsBankkonto.Datenhaltung.Model;

namespace ProjektAsBankkonto.Interfaces.Datenhaltung
{
    public interface IDatenhaltungKonto
    {
       
        bool addKonto(Konto konto);
        bool editKonto(string kontoNr, Konto konto);
        bool deleteKonto(Konto konto);
        Dictionary<string, Konto> fetchAllKonten(Kunde kunde);
        bool checkKontoNrExists(string kontoNr);
        int getCountKonten(Kunde kunde);
    }
}