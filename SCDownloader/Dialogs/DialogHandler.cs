using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;

namespace SCDownloader.Dialogs
{
    public class DialogHandler
    {
        public async Task ShowErrorDialog(string errorMessage)
        {
            var view = new ErrorDialog
            {
                DataContext = new ErrorVM(errorMessage)
            };
            
            await DialogHost.Show(view, "MainWindowDialog");
        }

        public async Task<bool> ShowQuestionDialog(string questionMessage)
        {
            var view = new YesNoDialog
            {
                DataContext = new YesNoVM(questionMessage)
            };

            var result = await DialogHost.Show(view, "MainWindowDialogHost");

            if (result is bool b)
            {
                return b;
            }
            return false;

        }
    }
}
