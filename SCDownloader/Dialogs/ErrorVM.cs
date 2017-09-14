using System.ComponentModel;

namespace SCDownloader.Dialogs
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
