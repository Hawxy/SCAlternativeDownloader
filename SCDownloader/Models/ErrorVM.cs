using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCDownloader.Models
{
    public class ErrorVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string ErrorText { get; set; }

        public ErrorVM(string errorText)
        {
            ErrorText = errorText;
            
        }
    }
}
