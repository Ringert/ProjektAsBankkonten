using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektAsBankkonto.Interfaces.Datenhaltung
{
    interface IDatenhaltung : IDatenhaltungFiliale, IDatenhaltungKonto, IDatenhaltungKunde
    {
        //Just for Type
    }
}
