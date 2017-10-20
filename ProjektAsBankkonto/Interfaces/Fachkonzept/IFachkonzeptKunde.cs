using System.Collections.Generic;

using ProjektAsBankkonto.Datenhaltung.Model;

namespace ProjektAsBankkonto.Interfaces.Fachkonzept
{
    interface IFachkonzeptKunde
    {
        bool saveKunde(Kunde kunde);
        bool deleteKunde(Kunde kunde);
        Filiale getOneKunde(int kundeNr);
        Dctionary<int, Kunde> getAllKunden();
        Dctionary<int, Kunde> getRangeOfKunden(int begin, int nr);

    }
}