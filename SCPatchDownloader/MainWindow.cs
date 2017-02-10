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

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;
using SCPatchDownloader.Properties;
using static SCPatchDownloader.Utilities;



namespace SCPatchDownloader
{
    public partial class MainWindow : MaterialForm
    {
        private readonly Stopwatch sw = new Stopwatch();
        //stores list of URLs
        private readonly ArrayList urlList = new ArrayList();

        private readonly List<Universe> versionList = new List<Universe>();

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
        private void MainWindow_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Settings.Default.PrvDir))
                textBoxDownloadDirectory.Text = Settings.Default.PrvDir;
            else
                textBoxDownloadDirectory.Text = Directory.GetCurrentDirectory() + "\\SCDownload";
            

            toolTip_Native.SetToolTip(checkBoxNativeFile, "Sorts files into public/test directories instead of using build number. Allows for easy copy/pasting or direct download into program files. Existing files will not be overwritten");
            DownloadPatchList();
        }

        //on Browse Directory click
        private void BrowseDirectoryButtonClick(object sender, EventArgs e)
        {
            var folderDir = new FolderBrowserDialog {ShowNewFolderButton = true};
            if (folderDir.ShowDialog() == DialogResult.OK)
                textBoxDownloadDirectory.Text = folderDir.SelectedPath;
        }

        //download button
        private void DownloadStartButtonClick(object sender, EventArgs e)
        {
            buttonCancel.Enabled = true;
            buttonDownloadStart.Enabled = false;
            comboReleaseSelector.Enabled = false;
            buttonSelectRelease.Enabled = false;
            textBoxDownloadDirectory.Enabled = false;
            buttonBrowseDirectory.Enabled = false;
            checkBoxNativeFile.Enabled = false;
            DownloadGameFiles();
        }


        private async Task DownloadGameFiles()
        {
            bool native = checkBoxNativeFile.Checked;
            string downloadLocation = textBoxDownloadDirectory.Text;
            int fileNum = 1;
            int totfileNum = urlList.Count;

            var security = new DirectorySecurity();
            security.AddAccessRule(new FileSystemAccessRule(
                new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl,
                InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit,
                PropagationFlags.NoPropagateInherit, AccessControlType.Allow));

            if (urlList.Count > 0 && !string.IsNullOrWhiteSpace(downloadLocation))
                try
                {
                    foreach (string file in urlList)
                    {
                        string filename = GetFileName(file);
                        labelCurrentStatus.Text = "Downloading file " + fileNum + " of " + totfileNum;
                        string dest = downloadLocation + GetFileStructure(file, native, comboReleaseSelector);
                        fulldir = Path.Combine(dest, filename);
                        if (!File.Exists(fulldir))
                        {
                            Directory.CreateDirectory(dest, security);
                            using (client = new WebClient())
                            {
                                client.Proxy.Credentials = CredentialCache.DefaultNetworkCredentials;
                                client.DownloadProgressChanged += (send, x) => FileDownloadProgress(x.BytesReceived);
                                client.DownloadFileCompleted += Client_InstallFileCompleted;
                                sw.Start();
                                await client.DownloadFileTaskAsync(new Uri(file), fulldir);
                            }
                        }
                        fileNum += 1;
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
            else
                MessageBox.Show("Please provide a valid download location", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            if (fileNum - 1 == urlList.Count)
            {
                MessageBox.Show("Download Complete!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetAllBoxes(this);
                labelCurrentStatus.Text = "Download Complete!";
                buttonCancel.Enabled = false;
                buttonSelectRelease.Enabled = true;
                buttonBrowseDirectory.Enabled = true;
                labelMegaBytes.Text = "N/A MB/s";
            }
                
        }

        //download speed calculator
        private void FileDownloadProgress(long bytesReceived)
        {
            labelMegaBytes.Text = $"{bytesReceived / 1024d / 1024d / sw.Elapsed.TotalSeconds:0.00} MB/s";
        }


        //handle cancelled download
        private void Client_InstallFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                if (fulldir != null) File.Delete(fulldir);
                client.Dispose();
            }
        }

        //load available game version on application startup
        private async Task DownloadPatchList()
        {
            string fileLocation = "LauncherInfo.txt";
            try
            {
                using (client = new WebClient())
                {
                    client.Proxy.Credentials = CredentialCache.DefaultNetworkCredentials;
                    client.DownloadProgressChanged += Client_ProgressChanged;
                    await client.DownloadFileTaskAsync(
                        new Uri("http://manifest.robertsspaceindustries.com/Launcher/_LauncherInfo"), fileLocation);
                    labelCurrentStatus.Text = "Loading Manifest...Complete";
                }
                if (File.Exists(fileLocation))
                {
                    StreamReader reader = File.OpenText(fileLocation);
                    //line in file
                    string line = reader.ReadLine();
                    string[] parts = line.Split('=');
                    parts = parts[1].Split(',');
                    //for each item in parts
                    foreach (string word in parts)
                    {
                        //get substring of each word
                        string versionName = word.Substring(1);
                        //get version name
                        while (!line.StartsWith(versionName + "_fileIndex"))
                            line = reader.ReadLine();
                        parts = line.Split('=');
                        string filePrefix = parts[1].Substring(1);
                        //store version name and prefix in universe object
                        Universe currentUniverse;
                        currentUniverse.versionName = versionName;
                        currentUniverse.fileIndex = filePrefix;
                        versionList.Add(currentUniverse);
                        //add version names to list
                        comboReleaseSelector.Items.Add(versionName);
                    }

                    if (comboReleaseSelector.Items.Count > 0)
                        comboReleaseSelector.SelectedIndex = 0;

                    reader.Close();
                }
                File.Delete(fileLocation);
            }
            catch (WebException)
            {
                MessageBox.Show("Connection Timed out", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //application load
        private void Client_ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBarStatus.Value = e.ProgressPercentage;
        }

        //download file list when version is selected
        private async void SelectReleaseButtonClick(object sender, EventArgs e)
        {
            string requestedUniverse = comboReleaseSelector.SelectedItem as string;
            string universeFileList = "";
            buttonDownloadStart.Enabled = true;
            var fileList = new ArrayList();

            //file handling
            string fileName = "fileList.json";
            var baseURLS = new ArrayList();

            if (!string.IsNullOrEmpty(requestedUniverse))
                foreach (Universe universe in versionList)
                    if (requestedUniverse.Equals(universe.versionName))
                        universeFileList = universe.fileIndex;
            try
            {
                //get file list
                if (!string.IsNullOrEmpty(universeFileList))
                {
                    using (client = new WebClient())
                    {
                        client.Proxy.Credentials = CredentialCache.DefaultNetworkCredentials;
                        labelCurrentStatus.Text = "Downloading file list";
                        client.DownloadProgressChanged += Client_ProgressChanged;
                        await client.DownloadFileTaskAsync(new Uri(universeFileList), fileName);
                    }
                    StreamReader reader = File.OpenText(fileName);
                    var writer = new StreamWriter("SC-URLs.txt", false);

                    SeekToLine(reader, "file_list");
                    string line = reader.ReadLine();
                    while (!line.Contains("],"))
                    {
                        fileList.Add(StripQuotations(line));
                        line = reader.ReadLine();
                    }

                    labelCurrentStatus.Text = fileList.Count + " files are ready for download";

                    //find prefix
                    line = SeekToLine(reader, "key_prefix");
                    string[] parts = line.Split((char) 34);
                    string prefix = parts[3];

                    //base url
                    SeekToLine(reader, "webseed_urls");
                    line = reader.ReadLine();
                    while (!line.Contains("]"))
                    {
                        baseURLS.Add(StripQuotations(line));
                        line = reader.ReadLine();
                    }

                    foreach (object i in fileList)
                    {
                        var rand = new Random();
                        int randomBase = rand.Next(0, baseURLS.Count - 1);

                        urlList.Add(baseURLS[randomBase] + "/" + prefix + "/" + i);
                        await writer.WriteLineAsync(baseURLS[randomBase] + "/" + prefix + "/" + i);
                    }
                    writer.Close();
                    reader.Close();

                    File.Delete(fileName);
                    File.Delete("SC-URLs.txt");
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

        //cancel download
        private void CancelButtonClick(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to cancel?", "Cancel Download",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                client.CancelAsync();
                ResetAllBoxes(this);
                labelCurrentStatus.Text = "Download Cancelled";
                buttonCancel.Enabled = false;
                buttonSelectRelease.Enabled = true;
                buttonBrowseDirectory.Enabled = true;
                labelMegaBytes.Text = "N/A MB/s";
            }
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
    }
}