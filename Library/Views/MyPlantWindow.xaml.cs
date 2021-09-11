using CarePlant.ViewModels;
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

namespace CarePlant.Views
{
    /// <summary>
    /// Interaction logic for MyPlantWindow.xaml
    /// </summary>
    public partial class MyPlantWindow : Window
    {
        public MyPlantViewModel myPlantViewModel { get; set; }
        public MyPlantWindow(MyPlantViewModel MyPlantViewModel)
        {
            InitializeComponent();
            this.myPlantViewModel = MyPlantViewModel;
            DataContext =MyPlantViewModel;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
