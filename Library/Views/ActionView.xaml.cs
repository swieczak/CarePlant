using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarePlant.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CarePlant.Views
{
    /// <summary>
    /// Logika interakcji dla klasy ActionView.xaml
    /// </summary>
    public partial class ActionView : UserControl
    {
        public ActionView()
        {
            InitializeComponent();

        }

        private void urgentList_Selected(object sender, RoutedEventArgs e)
        {
            if(urgentList.SelectedItem != null)
            soonList.UnselectAll();
        }

        private void soonList_Selected(object sender, RoutedEventArgs e)
        {
            if (soonList.SelectedItem != null)
                urgentList.UnselectAll();
        }
    }
}
