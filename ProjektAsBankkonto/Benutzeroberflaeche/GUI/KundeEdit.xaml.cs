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
        public Window MainWindow { get; set; }
        public KundeEdit(Window mainwindow)
        {
            this.MainWindow = mainwindow;
            InitializeComponent();
            this.comboBoxGeschlecht.ItemsSource = Enum.GetValues(typeof(Geschlechter)).Cast<Geschlechter>();
            this.comboBoxLand.ItemsSource = Enum.GetValues(typeof(Laender)).Cast<Laender>();
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void buttonKundeSave_Click(object sender, RoutedEventArgs e)
        {
            Kunde Kunde                 =   new Kunde();
            Kunde.Vorname               =   this.textBoxVorname.Text;
            Kunde.Nachname              =   this.textBoxNachname.Text;
            Kunde.Strasse               =   this.textBoxStrasse.Text;
            Kunde.Plz                   =   this.textBoxPlz.Text;
            Kunde.Ort                   =   this.textBoxOrt.Text;
            DateTime? geburtsdatum      =   this.datepickerGeburtsdatum.SelectedDate;
            if (geburtsdatum == null)
                throw new FormatException();
            Kunde.Geburtsdatum          =   Convert.ToDateTime(geburtsdatum);
            Kunde.Geschlecht            =   (Geschlechter)this.comboBoxGeschlecht.SelectedValue;
        }
    }
}
