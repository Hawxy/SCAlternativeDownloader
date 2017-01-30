//Copyright 2017 Hawx & Zephyr Auxiliary Services

//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//You may obtain a copy of the License at

//http://www.apache.org/licenses/LICENSE-2.0

//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.

//Some components of this file are based on work by NimmoG

//Move various functions out of main program and into seperate class
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
