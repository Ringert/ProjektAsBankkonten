using System.Collections.Generic;

using ProjektAsBankkonto.Datenhaltung.Model;

namespace ProjektAsBankkonto.Interfaces.Datenhaltung
{
    interface IDatenhaltungKonto : IDatenhaltung
    {
       
        bool insertKonto(Konto konto);
        bool updateKonto(Konto konto);
        bool deleteKonto(Konto konto);
        Konto fetchKonto(string kontoNr);
        Konto[] fetchAllKonten();
        bool checkKontoNrExists();
        int getAnzahlKonten();
        int getGroessteKontoNr();
       
    }
}