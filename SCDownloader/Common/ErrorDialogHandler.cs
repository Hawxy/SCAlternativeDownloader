using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SCDownloader.Models;

namespace SCDownloader.Common
{
    public class ErrorDialogHandler
    {
        public async Task ShowErrorDialog(string errorMessage)
        {
            var view = new ErrorDialog
            {
                DataContext = new ErrorVM(errorMessage)
            };
            
            await DialogHost.Show(view, "MainWindowDialog");
        }
    }
}
