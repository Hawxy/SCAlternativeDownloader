using System.ComponentModel;

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
