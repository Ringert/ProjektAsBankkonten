using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using ProjektAsBankkonto;
using ProjektAsBankkonto.Interfaces.Fachkonzept;
using ProjektAsBankkonto.Datenhaltung.Model;
using ProjektAsBankkonto.Datenhaltung.Enums;

namespace ProjektAsBankkonto.Benutzeroberflaeche
{
    /// <summary>
    /// Interaktionslogik für KundeEdit.xaml
    /// </summary>
    public partial class KundeEdit : Window
    {
        public MainWindow MainWindow { get; set; }
        public Kunde Kunde {get; set;}
        public KundeEdit(MainWindow mainWindow)
        {
            init(mainWindow);
            this.Kunde = new Kunde();
        }

        public KundeEdit(MainWindow mainWindow, Kunde kunde)
        {
            this.Kunde = kunde;
            init(mainWindow);
        }

        private void init(MainWindow mainwindow)
        {
            this.MainWindow = mainwindow;
            InitializeComponent();
            this.comboBoxGeschlecht.ItemsSource = Enum.GetValues(typeof(Geschlechter)).Cast<Geschlechter>();
            this.comboBoxLand.ItemsSource = Enum.GetValues(typeof(Laender)).Cast<Laender>();
        }

        private void buttonKundeSave_Click(object sender, RoutedEventArgs e)
        {
            Kunde Kunde = new Kunde();
            Kunde.Vorname = this.textBoxVorname.Text;
            Kunde.Nachname = this.textBoxNachname.Text;
            Kunde.Strasse = this.textBoxStraße.Text;
            Kunde.Plz = this.textBoxPLZ.Text;
            Kunde.Ort = this.textBoxOrt.Text;
            DateTime? geburtsdatum = this.datepickerGeburtsdatum.SelectedDate;
            if (geburtsdatum == null)
                throw new FormatException();
            Kunde.Geburtsdatum = Convert.ToDateTime(geburtsdatum);
            Kunde.Geschlecht = (Geschlechter)this.comboBoxGeschlecht.SelectedValue;

            this.MainWindow.Fachkonzept.saveKunde(Kunde);
            this.Close();
        }
    }
}
