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
        }

        private void buttonKundeAdd_Click(object sender, RoutedEventArgs e)
        {
            Window kundeEdit = new KundeEdit(this);
            kundeEdit.Show();
        }

        private void buttonKontoAdd_Click(object sender, RoutedEventArgs e)
        {
            Window kontoEdit = new KontoEdit(this);
            kontoEdit.Show();
        }
    }
}
