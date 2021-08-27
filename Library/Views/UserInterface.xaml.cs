using System;
using CarePlant.ViewModels;
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

namespace CarePlant.Views
{
    /// <summary>
    /// Logika interakcji dla klasy UserInterface.xaml
    /// </summary>
    public partial class UserInterface : Window
    {
        public UserInterfaceViewModel userinterfaceViewModel { get; set; }
        public UserInterface(UserInterfaceViewModel UserInterfaceViewModel)
        {
            InitializeComponent();
            this.userinterfaceViewModel = UserInterfaceViewModel;
            DataContext = UserInterfaceViewModel;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
