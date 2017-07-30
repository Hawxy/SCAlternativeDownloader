﻿//Copyright 2017 Hawx & Zephyr Auxiliary Services
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

//Move various functions out of main program and into seperate class

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using MaterialSkin.Controls;

namespace SCPatchDownloader
{
    public class Utilities
    {
        //the bit that goes between the download dir and the files
        public static string GetFileStructure(bool notnative, string selectedUniverse, string keyprefix)
        {
            string filestructure;
            if (notnative)
            {
                filestructure = $"StarCitizen\\{selectedUniverse}";
            }
            else
            {
                string[] keysplit = keyprefix.Split('/');
                filestructure = Path.Combine(keysplit[1], keysplit[2]);
            }
            return filestructure;
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