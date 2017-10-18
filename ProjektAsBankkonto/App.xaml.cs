using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using ProjektAsBankkonto.Benutzeroberflaeche;
using ProjektAsBankkonto.Fachkonzept;
using ProjektAsBankkonto.Datenhaltung;
using ProjektAsBankkonto.Interfaces.Fachkonzept;

namespace ProjektAsBankkonto
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        private IFachkonzept Fachkonzept;
        private void init(object sender, StartupEventArgs e)
        {
            this.Fachkonzept = new Fachkonzept1(new SQLiteController());
            // Create the startup window
            MainWindow wnd = new MainWindow(this.Fachkonzept);
            wnd.Show();
        }

    }
}
