using System.Collections.Generic;

using ProjektAsBankkonto.Datenhaltung.Model;

namespace ProjektAsBankkonto.Interfaces.Fachkonzept
{
    public interface IFachkonzeptKonto
    {
        bool addKonto(Konto konto);
        bool updateKonto(string kontoNr, Konto konto);
        bool deleteKonto(Konto konto);
        Dictionary<string, Konto> getAllKonten(Kunde kunde);
        int getCountKontent(Kunde kunde)
    }
}