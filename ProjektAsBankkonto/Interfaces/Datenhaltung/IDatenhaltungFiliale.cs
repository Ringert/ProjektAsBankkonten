using System.Collections.Generic;

using ProjektAsBankkonto.Datenhaltung.Model;

namespace ProjektAsBankkonto.Interfaces.Datenhaltung
{
    interface IDatenhaltungFilialeng
    {
        bool addiliale(Filiale filiale);
        bool editFiliale(Filiale filiale);
        bool deleteFiliale(Filiale filiale);
        Filiale fetchFiliale(int filialeNr);
        Filiale[] fetchAllFilialen();
        int getCountFilialen();
    }
}