using System.Collections.Generic;

using ProjektAsBankkonto.Datenhaltung.Model;

namespace ProjektAsBankkonto.Interfaces.Datenhaltung
{
    interface IDatenhaltungKonto
    {
       
        bool addKonto(Konto konto);
        bool editKonto(Konto konto);
        bool deleteKonto(Konto konto);
        Konto fetchKonto(string kontoNr);
        Konto[] fetchAllKonten();
        bool checkKontoNrExists();
        int getCountKonten();
    }
}