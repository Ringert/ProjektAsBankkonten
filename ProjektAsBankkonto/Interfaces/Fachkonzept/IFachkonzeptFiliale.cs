using System.Collections.Generic;

using ProjektAsBankkonto.Datenhaltung.Model;

namespace ProjektAsBankkonto.Interfaces.Fachkonzept
{
    public interface IFachkonzeptFiliale
    {
        bool saveFiliale(ref Filiale filiale);
        bool deleteFiliale(Filiale filiale);
        Filiale getOneFiliale(int filialeNr);
        Dictionary<int, Filiale> getAllFilialen();
        Dictionary<int, Filiale> getRangeOfFilalen(int begin, int nr);
    }
}