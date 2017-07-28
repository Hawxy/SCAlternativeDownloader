//Copyright 2017 Hawx & Zephyr Auxiliary Services
//https://github.com/Hawxy/SCAlternativeDownloader

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
using MaterialSkin.Controls;

namespace SCPatchDownloader
{
    public class Utilities
    {
        //remove quotations from a line
        public static string StripQuotations(string line)
        {
            string[] parts = line.Split('"');
            return parts[1];
        }

        //seek to specific line in file
        public static string SeekToLine(StreamReader file, string lineContents)
        {
            string line = "";
            while (!line.Contains(lineContents))
            {
                line = file.ReadLine();
            }
            return line;
        }

        //get entire file structure
        //cleanup this section at some point
        public static string GetFileStructure(string url, bool native, ComboBox relSelector)
        {
            string[] parts = url.Split('/');
            string filename = "";
            if (native)
            {
                for (int i = 7; i < parts.Length - 1; i++)
                {
                    filename += "\\" + parts[i];
                }
                filename = "\\StarCitizen\\" + relSelector.SelectedItem + "\\" + filename;
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

        //last 3 parts of directory to display in the UI
        public static string GetCoreDirectory(string url)
        {
            string[] parts = url.Split('/');
            string filedir = "";
            for (int i = parts.Length - 3; i < parts.Length - 1; i++)
            {
                filedir += "\\" + parts[i];
            }
            return filedir.Remove(0, 1);
        }

        //get name of downloading file
        public static string GetFileName(string url)
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
                if (control is MaterialSingleLineTextField x)
                {
                    x.Enabled = true;
                }
                if (control is ComboBox c)
                {
                    c.SelectedIndex = 0;
                    c.Enabled = true;
                }
                if (control is ProgressBar p)
                {
                    p.Value = 0;
                }
                if (control is CheckBox b)
                {
                    b.Enabled = true;
                }
                //Gotta do this manually
                if (control.Name.Equals("ButtonCancel"))
                {
                    control.Enabled = false;
                }
                if (control.Name.Equals("buttonSelectRelease") || control.Name.Equals("buttonBrowseDirectory"))
                {
                    control.Enabled = true;
                }
                if (control.Name.Equals("labelCurrentFile"))
                {
                    control.Text = "...";
                }
                if (control.Name.Equals("labelMegaBytes.Text"))
                {
                    control.Text = "N/A MB/s";
                }
            }
        }
    }
}