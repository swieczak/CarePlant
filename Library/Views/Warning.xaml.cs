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
using CarePlant.ViewModels;

namespace CarePlant.Views
{
    /// <summary>
    /// Logika interakcji dla klasy Warning.xaml
    /// </summary>
    public partial class Warning : Window
    {
        public WarningViewModel warningViewModel{ get; set; }
        public Warning(WarningViewModel WarningViewModel)
        {
            InitializeComponent();
            this.warningViewModel = WarningViewModel;
            DataContext = WarningViewModel;
        }
        private void ACK(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
