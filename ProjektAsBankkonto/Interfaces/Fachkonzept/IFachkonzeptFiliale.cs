using System.Collections.Generic;

using ProjektAsBankkonto.Datenhaltung.Model;

namespace ProjektAsBankkonto.Interfaces.Datenhaltung
{
    interface IFachkonzeptFiliale
    {
        bool saveFiliale(Filiale filiale);
        bool deleteFiliale(Filiale filiale);
        Filiale getOneFiliale(int filialeNr);
        Dctionary<int, Filale> getAllFilialen();
        Dctionary<int, Filale> getRangeOfFilalen(int begin, int nr);
    }
}