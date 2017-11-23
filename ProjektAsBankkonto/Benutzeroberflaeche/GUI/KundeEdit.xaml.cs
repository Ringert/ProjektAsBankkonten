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
            this.textBoxVorname.Text = this.Kunde.Vorname;
            this.textBoxNachname.Text = this.Kunde.Nachname;
            this.textBoxStraße.Text = this.Kunde.Strasse;
            this.textBoxPLZ.Text = this.Kunde.Plz;
            this.textBoxOrt.Text = this.Kunde.Ort;
            this.datepickerGeburtsdatum.SelectedDate = this.Kunde.Geburtsdatum;
            this.comboBoxGeschlecht.SelectedValue = this.Kunde.Geschlecht;
            this.comboBoxLand.SelectedValue = this.Kunde.Land;
        }

        public KundeEdit(MainWindow mainWindow, Kunde kunde)
        {
            init(mainWindow);
            this.Kunde = kunde;
            this.textBoxVorname.Text = this.Kunde.Vorname;
            this.textBoxNachname.Text = this.Kunde.Nachname;
            this.textBoxStraße.Text = this.Kunde.Strasse;
            this.textBoxPLZ.Text = this.Kunde.Plz;
            this.textBoxOrt.Text = this.Kunde.Ort;
            this.datepickerGeburtsdatum.SelectedDate = this.Kunde.Geburtsdatum;
            this.comboBoxGeschlecht.SelectedValue = this.Kunde.Geschlecht;
            this.comboBoxLand.SelectedValue = this.Kunde.Land;
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
            try
            {
                this.Kunde.Vorname = this.textBoxVorname.Text;
                this.Kunde.Nachname = this.textBoxNachname.Text;
                this.Kunde.Strasse = this.textBoxStraße.Text;
                this.Kunde.Plz = this.textBoxPLZ.Text;
                this.Kunde.Ort = this.textBoxOrt.Text;
                DateTime? geburtsdatum = this.datepickerGeburtsdatum.SelectedDate;
                if (geburtsdatum == null)
                    throw new FormatException("Sie haben kein Datum eingegeben");
                this.Kunde.Geburtsdatum = Convert.ToDateTime(geburtsdatum);
                this.Kunde.Geschlecht = (Geschlechter)this.comboBoxGeschlecht.SelectedValue;
                this.Kunde.Land = (Laender)this.comboBoxLand.SelectedValue;

                this.MainWindow.Fachkonzept.saveKunde(this.Kunde);
                this.Close();
            } catch (FormatException ex) {
                MessageBox.Show("Ein Fehler beim speichern ist aufgetreten!\nBitte überprüfen Sie ihre eingaben.\nFehler:\n " + ex.Message);
            }
            
        }
    }
}
