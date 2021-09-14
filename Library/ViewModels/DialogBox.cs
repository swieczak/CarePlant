using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
namespace CarePlant.ViewModels
{
    public class DialogBox
    {
        public static void Show(string text) 
        {
            MessageBox.Show(text);
        }        
    }
}
