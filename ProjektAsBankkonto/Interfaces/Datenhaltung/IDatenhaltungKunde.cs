using System.Collections.Generic;

using ProjektAsBankkonto.Datenhaltung.Model;

namespace ProjektAsBankkonto.Interfaces.Datenhaltung
{
    public interface IDatenhaltungKunde
    {
        
        bool addKunde(Kunde kunde);
        bool editKunde(Kunde kunde);
        bool deleteKunde(Kunde kunde);
        Kunde fetchKunde(int kundeNr);
        Dictionary<int, Kunde> fetchAllKunden();
        Dictionary<int, Kunde> fetchRangeOfKunden(int nr, int offset);
        int getCountKunden();
    }
}