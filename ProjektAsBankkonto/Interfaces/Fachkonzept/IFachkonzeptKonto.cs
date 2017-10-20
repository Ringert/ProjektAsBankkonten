using System.Collections.Generic;

using ProjektAsBankkonto.Datenhaltung.Model;

namespace ProjektAsBankkonto.Interfaces.Datenhaltung
{
    interface IFachkonzeptKonto : IFachkonzept
    {
        bool saveKonto(Konto konto);
        bool deleteKonto(Konto konto);
        Filiale getOneKonto(string kontoNr);
        Dctionary<string, Konto> getAllKonten();
        Dctionary<string, Konto> getRangeOfKonten(int begin, int nr);
    }
}