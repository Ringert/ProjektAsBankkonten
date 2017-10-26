using System.Collections.Generic;

using ProjektAsBankkonto.Datenhaltung.Model;

namespace ProjektAsBankkonto.Interfaces.Fachkonzept
{
    public interface IFachkonzeptKonto
    {
        bool saveKonto(ref Konto konto);
        bool deleteKonto(Konto konto);
        Filiale getOneKonto(string kontoNr);
        Dictionary<string, Konto> getAllKonten();
        Dictionary<string, Konto> getRangeOfKonten(int begin, int nr);
    }
}