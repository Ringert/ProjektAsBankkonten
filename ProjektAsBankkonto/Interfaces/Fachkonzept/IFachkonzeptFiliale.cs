using System.Collections.Generic;

using ProjektAsBankkonto.Datenhaltung.Model;

namespace ProjektAsBankkonto.Interfaces.Fachkonzept
{
    public interface IFachkonzeptFiliale
    {
        bool saveFiliale(Filiale filiale);
        bool deleteFiliale(Filiale filiale);
        Filiale getFiliale(int filialeNr);
        Dictionary<int, Filiale> getAllFilialen();
        Dictionary<int, Filiale> getRangeOfFilalen(int nr, int offset);
        int getCountFilialen();
    }
}