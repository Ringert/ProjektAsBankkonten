using System.Collections.Generic;

using ProjektAsBankkonto.Datenhaltung.Model;

namespace ProjektAsBankkonto.Interfaces.Fachkonzept
{
    public interface IFachkonzeptKunde
    {
        bool saveKunde(ref Kunde kunde);
        //bool deleteKunde(Kunde kunde);
        //Filiale getOneKunde(int kundeNr);
        //Dictionary<int, Kunde> getAllKunden();
        //Dictionary<int, Kunde> getRangeOfKunden(int begin, int nr);

    }
}