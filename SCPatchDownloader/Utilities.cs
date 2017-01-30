using System.Windows.Forms;

namespace SCPatchDownloader
{
    public class Utilities
    {
        public static void ResetAllBoxes(Control form)
        {
            foreach (Control control in form.Controls)
            {
                if (control is TextBox)
                {
                    TextBox textBox = (TextBox)control;
                   // textBox.Text = null;
                    textBox.Enabled = true;
                }

                if (control is ComboBox)
                {
                    ComboBox listBox = (ComboBox)control;
                    listBox.SelectedIndex = 0;
                    listBox.Enabled = true;
                }
                if (control is ProgressBar)
                {
                    ProgressBar progressBar = (ProgressBar)control;
                    progressBar.Value = 0;
                }
                

            }
        }
    }
}
