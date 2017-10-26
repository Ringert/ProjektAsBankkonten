using System.Collections.Generic;

using ProjektAsBankkonto.Datenhaltung.Model;

namespace ProjektAsBankkonto.Interfaces.Datenhaltung
{
    public interface IDatenhaltungKonto
    {
       
        bool addKonto(ref Konto konto);
        bool editKonto(Konto konto);
        bool deleteKonto(Konto konto);
        Konto fetchKonto(string kontoNr);
        Dictionary<string, Konto> fetchAllKonten();
        Dictionary<string, Konto> fetchRangeOfKonten(int nr, int offset);
        bool checkKontoNrExists(string kontoNr);
        int getCountKonten();
    }
}