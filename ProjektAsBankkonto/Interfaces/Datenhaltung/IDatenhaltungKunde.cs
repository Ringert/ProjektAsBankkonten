using System.Collections.Generic;

using ProjektAsBankkonto.Datenhaltung.Model;

namespace ProjektAsBankkonto.Interfaces.Datenhaltung
{
    interface IDatenhaltungKunde
    {
        
        bool insertKunde(Kunde kunde);
        bool updateKunde(Kunde kunde);
        bool deleteKunde();
        Kunde fetchKunde(int kundeNr);
        Kunde[] fetchAllKunden();
        int getAnzahlKunden();
        int getGroessteKundenNr();
   
    }
}