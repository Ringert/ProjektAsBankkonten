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
using System.Windows.Navigation;
using System.Windows.Shapes;

using ProjektAsBankkonto.Interfaces.Fachkonzept;
using ProjektAsBankkonto.Datenhaltung.Model;
using ProjektAsBankkonto.Datenhaltung.Enums;
using System.Data;

namespace ProjektAsBankkonto.Benutzeroberflaeche
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public IFachkonzept Fachkonzept { get; set; }
        public MainWindow(IFachkonzept fachkonzept)
        {
            this.Fachkonzept = fachkonzept;
            InitializeComponent();
            this.refreshKunden();
            this.listViewKunden.SelectionMode = SelectionMode.Single;
        }

        private void refreshKunden(object sender, EventArgs e)
        {
            this.refreshKunden();
        }

        private void refreshKunden()
        {
            int currentSelectedIndex = listViewKunden.SelectedIndex;
            this.listViewKunden.Items.Clear();
            Dictionary<int, Kunde> kunden = this.Fachkonzept.getAllKunden();
            foreach (KeyValuePair<int, Kunde> item in kunden)
            {
                this.listViewKunden.Items.Add(item.Value);
            }
            listViewKunden.SelectedIndex = currentSelectedIndex;
            refreshKonten();
        }
        private void refreshKonten(object sender, EventArgs e)
        {
            this.refreshKonten();
        }
        private void refreshKonten()
        {
            int currentSelectedIndex = listViewKonten.SelectedIndex;
            this.listViewKonten.Items.Clear();
            if(this.listViewKunden.SelectedItem == null)
            {
                return;
            }
            this.kontoKundeValue.Text = ((Kunde)this.listViewKunden.SelectedItem).Vorname + ", " + ((Kunde)this.listViewKunden.SelectedItem).Nachname;
            Dictionary<string, Konto> konten = this.Fachkonzept.getAllKonten((Kunde)this.listViewKunden.SelectedItem);
            foreach (KeyValuePair<string, Konto> item in konten)
            {
                this.listViewKonten.Items.Add(item.Value);
            }
            listViewKonten.SelectedIndex = currentSelectedIndex;
        }

        private void listViewKunden_SelectionChanged(object sender, RoutedEventArgs e)
        {
            Kunde kunde = this.listViewKunden.SelectedItem as Kunde;
            if (kunde != null) {
                this.kundeNameValue.Text = ((char)kunde.Geschlecht == 'm' ? "Herr" : "Frau") + " " + kunde.Vorname + " " + kunde.Nachname;
                this.birthValue.Text = kunde.Geburtsdatum.Day + "." + kunde.Geburtsdatum.Month + "." + kunde.Geburtsdatum.Year;
                this.streetValue.Text = kunde.Strasse;
                this.cityValue.Text = kunde.Plz + ", " + kunde.Ort;
                this.landValue.Text = kunde.Land.ToString();
            } else {
                this.kundeNameValue.Text = "";
                this.birthValue.Text = "";
                this.streetValue.Text = "";
                this.cityValue.Text = "";
                this.landValue.Text = "";
            }
            refreshKonten();
        }

        private void listViewKunde_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            Kunde kunde = ((FrameworkElement)e.OriginalSource).DataContext as Kunde;
            if (kunde != null)
            {
                Window kundeEdit = new KundeEdit(this, kunde);
                kundeEdit.Show();
                kundeEdit.Closed += refreshKunden;
            }
            
        }
        private void KundeAddButton_Click(object sender, RoutedEventArgs e)
        {
            Window kundeEdit = new KundeEdit(this);
            kundeEdit.Show();
            kundeEdit.Closed += refreshKunden;
        }

        private void KundeDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Kunde kunde = this.listViewKunden.SelectedItem as Kunde;
            if (kunde == null)
            {
                MessageBox.Show("Sie müssen zuerst einen Kunden auswählen!");
                return;
            }
            this.Fachkonzept.deleteKunde(kunde);
            this.refreshKunden();
        }

        private void KontoAddButton_Click(object sender, RoutedEventArgs e)
        {
            if(this.listViewKunden.SelectedItem == null)
            {
                MessageBox.Show("Sie müssen zuerst einen Kunden auswählen!");
                return;
            }
            Window kontoEdit = new KontoEdit(this, (Kunde)this.listViewKunden.SelectedItem);
            kontoEdit.Show();
            kontoEdit.Closed += refreshKonten;
        }
        private void KontoDelButton_Click(object sender, RoutedEventArgs e)
        {
            if(this.listViewKonten.SelectedItem == null)
            {
                MessageBox.Show("Sie müssen zuerst einen Konto auswählen!");
                return;
            }
            this.Fachkonzept.deleteKonto((Konto)this.listViewKonten.SelectedItem);
            refreshKonten();
        }

        private void listViewKonten_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Konto konto = this.listViewKonten.SelectedItem as Konto;
            if (konto != null)
            {
                this.kontonummerValue.Text = konto.KontoNr;
                this.filialNameValue.Text = konto.Filiale.Name;
                this.filialAdrValue.Text = konto.Filiale.Strasse;
                this.filialeCityValue.Text = konto.Filiale.Plz + ", " + konto.Filiale.Ort;
                this.filiallandValue.Text = konto.Filiale.Land.ToString();
            }
            else
            {
                this.kontonummerValue.Text = "";
                this.filialNameValue.Text = "";
                this.filialAdrValue.Text = "";
                this.filialeCityValue.Text = "";
                this.filiallandValue.Text = "";
            }
        }
    }
}
