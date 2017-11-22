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
        }

        private void refreshKunden(object sender, EventArgs e)
        {
            this.refreshKunden();
        }

        private void refreshKunden()
        {
            this.listViewKunden.Items.Clear();
            Dictionary<int, Kunde> kunden = this.Fachkonzept.getAllKunden();
            foreach(KeyValuePair<int, Kunde> item in kunden)
            {
                this.listViewKunden.Items.Add(item.Value);
            }
        
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

        private void KundeDelButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void KontoAddButton_Click(object sender, RoutedEventArgs e)
        {
            Window kontoEdit = new KontoEdit(this);
            kontoEdit.Show();
        }

        private void KontoDelButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
