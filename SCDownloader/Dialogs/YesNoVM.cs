using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SCDownloader.Dialogs
{
    public class YesNoVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string QuestionText { get; set; }

        public YesNoVM(string questionText)
        {
            QuestionText = questionText;
        }
    }
}
