using System.Collections.Generic;

using ProjektAsBankkonto.Datenhaltung.Model;

namespace ProjektAsBankkonto.Interfaces.Datenhaltung
{
    interface IDatenhaltungKunde
    {
        
        bool addKunde(Kunde kunde);
        bool editKunde(Kunde kunde);
        bool deleteKunde();
        Kunde fetchKunde(int kundeNr);
        Kunde[] fetchAllKunden();
        int getCountKunden();
    }
}