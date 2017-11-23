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
    /// Interaktionslogik für KontoEdit.xaml
    /// </summary>
    public partial class KontoEdit : Window
    {
        public MainWindow MainWindow { get; set; }
        private Konto konto;
        private Kunde kunde;
        public KontoEdit(MainWindow mainwindow, Kunde kunde)
        {
            this.konto = new Konto();
            this.kunde = kunde;
            this.konto.Kunde = kunde;
            this.MainWindow = mainwindow;
            InitializeComponent();
            this.labelInhaberName.Text = ((char)this.kunde.Geschlecht == 'm' ? "Herr" : "Frau") + " " + this.kunde.Vorname + " " + this.kunde.Nachname;
            this.comboBoxFiliale.ItemsSource = this.MainWindow.Fachkonzept.getAllFilialen();
        }

        private void comboBoxFiliale_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filiale filiale = ((KeyValuePair<int, Filiale>)this.comboBoxFiliale.SelectedItem).Value;
            if (filiale != null)
            {
                this.labelBlzNummer.Text = filiale.Blz;
                this.labelStraße.Text = filiale.Strasse;
                this.labelOrt.Text = filiale.Plz + ", " + filiale.Ort;
                this.labelLand.Text = filiale.Land.ToString();
            }
        }
        private void buttonSpeichern_Click(object sender, RoutedEventArgs e)
        {
            if (this.comboBoxFiliale.SelectedItem == null)
            {
                MessageBox.Show("Sie müssen eine Filiale auswählen.");
                return;
            }
            try
            {
                Filiale filiale = ((KeyValuePair<int, Filiale>)this.comboBoxFiliale.SelectedItem).Value;
                this.konto.Filiale = filiale;
                this.konto.KontoNr = this.textBoxKontonummer.Text;
                this.MainWindow.Fachkonzept.addKonto(konto);
                this.Close();
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Ein Fehler beim speichern ist aufgetreten!\nBitte überprüfen Sie ihre eingaben.\nFehler:\n " + ex.Message);
            }
            
        }
    }
}
