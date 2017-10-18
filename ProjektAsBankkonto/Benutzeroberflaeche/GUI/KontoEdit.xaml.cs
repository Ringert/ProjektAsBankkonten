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
namespace ProjektAsBankkonto.Benutzeroberflaeche
{
    /// <summary>
    /// Interaktionslogik für KontoEdit.xaml
    /// </summary>
    public partial class KontoEdit : Window
    {
        public Window MainWindow { get; set; }
        public KontoEdit(Window mainwindow)
        {
            this.MainWindow = mainwindow;
            InitializeComponent();
        }

        private void comboBoxFiliale_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
