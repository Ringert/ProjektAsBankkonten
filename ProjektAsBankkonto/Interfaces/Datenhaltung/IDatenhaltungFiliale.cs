using System.Collections.Generic;

using ProjektAsBankkonto.Datenhaltung.Model;

namespace ProjektAsBankkonto.Interfaces.Datenhaltung
{
    interface IDatenhaltungFiliale : IDatenhaltung
    {
        bool insertFiliale(Filiale filiale);
        bool updateFiliale(Filiale filiale);
        bool deleteFiliale(Filiale filiale);
        Filiale fetchFiliale(int filialeNr);
        Filiale[] fetchAllFilialen();
        int getAnzahlFilialen();
        int getGroessteFilialeNr();
   
    }
}