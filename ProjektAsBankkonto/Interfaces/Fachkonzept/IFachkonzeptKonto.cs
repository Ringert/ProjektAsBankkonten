using System.Collections.Generic;

using ProjektAsBankkonto.Datenhaltung.Model;

namespace ProjektAsBankkonto.Interfaces.Fachkonzept
{
    public interface IFachkonzeptKonto
    {
        bool saveKonto(ref Konto konto);
        bool deleteKonto(Konto konto);
        Filiale getOneKonto(string kontoNr);
        Dictionary<string, Konto> getAllKonten(Kunde kunde);
        Dictionary<string, Konto> getRangeOfKonten(Kunde kunde, int begin, int nr);
    }
}