using System.Collections.Generic;

using ProjektAsBankkonto.Datenhaltung.Model;

namespace ProjektAsBankkonto.Interfaces.Fachkonzept
{
    public interface IFachkonzeptKunde
    {
        bool saveKunde(Kunde kunde);
        bool deleteKunde(Kunde kunde);
        Kunde getKunde(int kundeNr);
        Dictionary<int, Kunde> getAllKunden();
        Dictionary<int, Kunde> getRangeOfKunden(int nr, int offset);
        int getCountKunden();
    }
}