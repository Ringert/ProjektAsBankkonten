 using System.Collections.Generic;

using ProjektAsBankkonto.Datenhaltung.Model;

namespace ProjektAsBankkonto.Interfaces.Datenhaltung
{
    public interface IDatenhaltungFiliale
    {
        bool addFiliale(Filiale filiale);
        bool editFiliale(Filiale filiale);
        bool deleteFiliale(Filiale filiale);
        Filiale fetchFiliale(int filialeNr);
        Dictionary<int, Filiale> fetchAllFilialen();
        Dictionary<int, Filiale> fetchRangeOfFilialen(int nr, int offset);
        int getCountFilialen();
    }
}