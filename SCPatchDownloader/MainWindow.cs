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

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;
using Newtonsoft.Json;
using Ookii.Dialogs;
using SCPatchDownloader.Models;
using SCPatchDownloader.Properties;
using static SCPatchDownloader.Utilities;


namespace SCPatchDownloader
{
    public partial class MainWindow : MaterialForm
    {
        private readonly Dictionary<string, LauncherInfo> launcherInfoDict = new Dictionary<string, LauncherInfo>();

        private readonly Dictionary<string, BuildData> buildDataDict = new Dictionary<string, BuildData>();

        private BuildData selectedBuildData = new BuildData();

        private readonly Stopwatch sw = new Stopwatch();
        //stores list of URLs
        private readonly ArrayList urlList = new ArrayList();
        
        private WebClient client;
        private string fulldir;

        public MainWindow()
        {
            InitializeComponent();

            MaterialSkinManager materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900,
                Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
        }

        //loading application
        private async void MainWindow_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Settings.Default.PrvDir))
                textBoxDownloadDirectory.Text = Settings.Default.PrvDir;
            else
                textBoxDownloadDirectory.Text = Directory.GetCurrentDirectory() + "\\SCDownload";


            toolTip_Native.SetToolTip(checkBoxNativeFile,
                "Sorts files into public/test directories instead of using build number. Allows for easy copy/pasting or direct download into program files. Existing files will not be overwritten");
            await DownloadPatchList();
        }

        //on Browse Directory click
        private void BrowseDirectoryButtonClick(object sender, EventArgs e)
        {
            var folderDir = new VistaFolderBrowserDialog {ShowNewFolderButton = true};
            if (folderDir.ShowDialog() == DialogResult.OK)
                textBoxDownloadDirectory.Text = folderDir.SelectedPath;
        }

        //load available game version on application startup
        private async Task DownloadPatchList()
        {
            try
            {
                string launcherInfoStr;
                using (client = new WebClient())
                {
                    client.Proxy.Credentials = CredentialCache.DefaultNetworkCredentials;
                    client.DownloadProgressChanged += Client_ProgressChanged;
                    launcherInfoStr = await client.DownloadStringTaskAsync(new Uri("http://manifest.robertsspaceindustries.com/Launcher/_LauncherInfo"));
                    labelCurrentStatus.Text = "Loading Manifest...Complete";
                }
                if (!string.IsNullOrEmpty(launcherInfoStr))
                {
                    using (StringReader reader = new StringReader(launcherInfoStr))
                    {
                        string line;
                        var fields = typeof(LauncherInfo).GetProperties();
                        while (!string.IsNullOrEmpty(line = reader.ReadLine())) 
                        {
                            string[] lineitems = Array.ConvertAll(line.Split('='), p => p.Trim());
                            if (lineitems[0] == "universes")
                            {
                                string[] universes = Array.ConvertAll(lineitems[1].Split(','), p => p.Trim());
                                foreach (var item in universes)
                                {
                                    launcherInfoDict.Add(item, new LauncherInfo());
                                }
                            }
                            else
                            {
                                var info = launcherInfoDict.FirstOrDefault(x => lineitems[0].StartsWith(x.Key)).Value;
                                foreach (var field in fields)
                                {
                                    if (lineitems[0].EndsWith(field.Name, StringComparison.CurrentCultureIgnoreCase))
                                    {
                                        field.SetValue(info, lineitems[1]);
                                    }
                                }
                            }
                        }
                    }
                    comboReleaseSelector.Items.AddRange(launcherInfoDict.Keys.ToArray());
                }

                if (comboReleaseSelector.Items.Count > 0)
                     comboReleaseSelector.SelectedIndex = 0;
            }
            catch (WebException)
            {
                MessageBox.Show("Unable to download manifest. Exiting.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        private async void comboReleaseSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            string requestedUniverse = comboReleaseSelector.SelectedItem as string;
            if (buildDataDict.TryGetValue(requestedUniverse, out BuildData checkedBuildData))
            {
                selectedBuildData = checkedBuildData;
                labelCurrentStatus.Text = $"{checkedBuildData.file_count_total} files are ready for download";
                buttonDownloadStart.Enabled = true;
                return;
            }

            try
            {
                //get file list
                if (!string.IsNullOrEmpty(requestedUniverse))
                {
                    var buildDataURL = launcherInfoDict[requestedUniverse].FileIndex;
                    string buildDataString;
                    using (client = new WebClient())
                    {
                        client.Proxy.Credentials = CredentialCache.DefaultNetworkCredentials;
                        labelCurrentStatus.Text = "Downloading file list";
                        client.DownloadProgressChanged += Client_ProgressChanged;
                        buildDataString = await client.DownloadStringTaskAsync(new Uri(buildDataURL));
                    }
                    BuildData buildData = JsonConvert.DeserializeObject<BuildData>(buildDataString);
                    AddNewBuildInfo(buildData, requestedUniverse);
                }
                else
                {
                    MessageBox.Show("Unable to find file list", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (WebException)
            {
                MessageBox.Show("File list failed to download", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddNewBuildInfo(BuildData buildData, string requestedUniverse)
        {
            buildDataDict.Add(requestedUniverse, buildData);
            labelCurrentStatus.Text = $"{buildData.file_count_total} files are ready for download";
            selectedBuildData = buildData;
            buttonDownloadStart.Enabled = true;
        }

        //cancel download
        private void CancelButtonClick(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to cancel?", "Cancel Download",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                client.CancelAsync();
                labelCurrentStatus.Text = "Download Cancelled";
            }
        }

        //download button
        private async void DownloadStartButtonClick(object sender, EventArgs e)
        {
            buttonCancel.Enabled = true;
            buttonDownloadStart.Enabled = false;
            comboReleaseSelector.Enabled = false;
            textBoxDownloadDirectory.Enabled = false;
            buttonBrowseDirectory.Enabled = false;
            checkBoxNativeFile.Enabled = false;
            await DownloadGameFiles();
        }


        private async Task DownloadGameFiles()
        {

            bool notnative = checkBoxNativeFile.Checked;
            string downloadLocation = textBoxDownloadDirectory.Text;
            string selectedUniverse = comboReleaseSelector.SelectedItem as string;

            int fileNum = 1;
            int totfileNum = selectedBuildData.file_count_total;
            var randomws = new Random();

            string coreFileStructure = Path.Combine(downloadLocation, GetFileStructure(notnative, selectedUniverse, selectedBuildData.key_prefix));

            var security = new DirectorySecurity();
            security.AddAccessRule(new FileSystemAccessRule(
                new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl,
                InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit,
                PropagationFlags.NoPropagateInherit, AccessControlType.Allow));

            if (!string.IsNullOrWhiteSpace(downloadLocation))
                try
                {
                    foreach (string file in selectedBuildData.file_list)
                    {
                        labelCurrentStatus.Text = $"Downloading file {fileNum} of {totfileNum}";
                        var wsurl = new Uri(new Uri(selectedBuildData.webseed_urls[randomws.Next(selectedBuildData.webseed_urls.Count)]), selectedBuildData.key_prefix.TrimStart('/'));
                        var downloadurl = new Uri(wsurl, file.TrimStart('/'));

                        //string filename = GetFileName(file);
                       // string filedir = GetCoreDirectory(file);

                        labelCurrentFile.Text = file;
                        fulldir = Path.Combine(coreFileStructure, file);
                        if (!File.Exists(fulldir))
                        {
                            Directory.CreateDirectory(Path.GetDirectoryName(fulldir), security);
                            using (client = new WebClient())
                            {
                                client.Proxy.Credentials = CredentialCache.DefaultNetworkCredentials;
                                client.DownloadProgressChanged += FileDownloadProgress;
                                client.DownloadFileCompleted += Client_InstallFileCompleted;
                                sw.Start();
                                await client.DownloadFileTaskAsync(downloadurl, fulldir);
                            }
                        }
                        fileNum += 1;
                        //overall status bar
                        progressBarStatus.Value = 100 * fileNum / totfileNum;
                        sw.Reset();
                    }
                }
                catch (DirectoryNotFoundException)
                {
                    MessageBox.Show("Unable to write to location, do you have permission?",
                        "DirectoryNotFoundException",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (IOException x)
                {
                    MessageBox.Show("Unable to write to disk. Do you have enough space? Full Exception: " + x,
                        "IOException", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (WebException x)
                {
                    //Handle the cancel event
                    if (x.Message == "The request was aborted: The request was canceled.")
                    {
                        return;
                    }
                    MessageBox.Show($"Download failure, unable to continue. \nFull exception: {x.Message}", "WebException",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            else
                MessageBox.Show("Please provide a valid download location", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            if (fileNum - 1 == urlList.Count)
            {
                MessageBox.Show("Download Complete!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetAllBoxes(this);
                labelCurrentStatus.Text = "Download Complete!";
            }
        }

        //download speed calculator + progress bar
        private void FileDownloadProgress(object sender, DownloadProgressChangedEventArgs e)
        {
            labelMegaBytes.Text = $"{e.BytesReceived / 1024d / 1024d / sw.Elapsed.TotalSeconds:0.00} MB/s";
            progressBarFile.Maximum = (int)e.TotalBytesToReceive;
            progressBarFile.Value = (int)e.BytesReceived;
        }

        //handle cancelled download
        private void Client_InstallFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                client.Dispose();
                if (fulldir != null) File.Delete(fulldir);
                labelMegaBytes.Text = "N/A MB/s";
                ResetAllBoxes(this);
            }
        }

        //Change progress bar value on download progress change
        private void Client_ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBarStatus.Value = e.ProgressPercentage;
        }

        //save directory on form close
        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.PrvDir = textBoxDownloadDirectory.Text;
            Settings.Default.Save();
        }

        private void GitHubButtonClick(object sender, EventArgs e)
        {
            Process.Start("https://github.com/Hawxy/SCAlternativePatcher/");
        }

        //game versions
        private struct Universe
        {
            public string versionName;
            public string fileIndex;
        }

        private void customBuildSelect_Click(object sender, EventArgs e)
        {
            using (var selectFileDialog = new OpenFileDialog{Title = "Select build JSON", Filter = "JSON files|*.json"})
            {
                if (selectFileDialog.ShowDialog() == DialogResult.OK)
                {
                    buttonDownloadStart.Enabled = true;
                    comboReleaseSelector.Enabled = false;
                    checkBoxNativeFile.Checked = false;
                    checkBoxNativeFile.Enabled = false;
                    comboReleaseSelector.Items.Clear();
                    comboReleaseSelector.Items.Add(Path.GetFileNameWithoutExtension(selectFileDialog.FileName));
                    comboReleaseSelector.SelectedIndex = 0;
                    urlList.Clear();
                    //ProcessFileList(selectFileDialog.FileName);
                }
            }

        }
    }
}