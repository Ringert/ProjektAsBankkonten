using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProjektAsBankkonto.Interfaces.Fachkonzept;
using ProjektAsBankkonto.Interfaces.Datenhaltung;

namespace ProjektAsBankkonto.Fachkonzept
{
    class Fachkonzept1 : IFachkonzept
    {
        public IDatenhaltung Datenhaltung { get; set; }

        public Fachkonzept1 (IDatenhaltung datenhaltung)
        {
            this.Datenhaltung = datenhaltung;
        }
    }
}
