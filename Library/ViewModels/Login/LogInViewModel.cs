using System;
using CarePlant.Commands;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

using CarePlant.Views;

namespace CarePlant.ViewModels
{
    public class LogInViewModel : BaseViewModel
    {
        public LogInWindow LogInWindow { get; set; }
        public LogInViewModel()
        {
            this.LogInWindow = LogInWindow;
        }
        public UserInterfaceCommand UserInterfaceSectionCommand => new UserInterfaceCommand(LogInWindow);

    }
}
