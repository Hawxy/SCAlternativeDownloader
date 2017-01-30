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

using System.IO;
using System.Windows.Forms;

namespace SCPatchDownloader
{
    public class Utilities
    {
        //remove quotations from a line
        public static object stripQuotations(string line)
        {
            string[] parts = line.Split('"');
            return parts[1];
        }

        //seek to specific line in file
        public static string seekToLine(StreamReader file, string lineContents)
        {
            string line = "";
            while (!line.Contains(lineContents))
            {
                line = file.ReadLine();
            }
            return line;
        }

        //get filestructure
        public static string getFileStructure(string url, bool native, ComboBox relSelector)
        {
            string[] parts = url.Split('/');
            string filename = "";
            if (native)
            {
                for (int i = 7; i < parts.Length - 1; i++)
                {
                    filename += "\\" + parts[i];
                }
                filename = "\\StarCitizen\\" + relSelector.SelectedItem as string + "\\" + filename;
            }
            else
            {
                for (int i = 5; i < parts.Length - 1; i++)
                {
                    filename += "\\" + parts[i];
                }
            }

            return filename;
        }

        //get name of downloading file
        public static string getFileName(string url)
        {
            string[] parts = url.Split('/');
            string filename = parts[parts.Length - 1];
            return filename;
        }

        //general cleanup on form reset
        public static void ResetAllBoxes(Control form)
        {
            foreach (Control control in form.Controls)
            {
                if (control is TextBox)
                {
                    TextBox textBox = (TextBox) control;
                    // textBox.Text = null;
                    textBox.Enabled = true;
                }

                if (control is ComboBox)
                {
                    ComboBox listBox = (ComboBox) control;
                    listBox.SelectedIndex = 0;
                    listBox.Enabled = true;
                }
                if (control is ProgressBar)
                {
                    ProgressBar progressBar = (ProgressBar) control;
                    progressBar.Value = 0;
                }
                if (control is CheckBox)
                {
                    CheckBox checkbox = (CheckBox) control;
                    checkbox.Enabled = true;
                }
            }
        }
    }
}