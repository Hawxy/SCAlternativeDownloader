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
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Newtonsoft.Json;
using SCDownloader.BoilerClasses;
using SCDownloader.Common;
using SCDownloader.Properties;

namespace SCDownloader.Models
{
    internal class MainWindowVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        //User download directory
        public string UserDirectory { get; set; }

        private void SetUserDirectory()
        {
            if (!string.IsNullOrEmpty(Settings.Default.PrvDir))
                UserDirectory = Settings.Default.PrvDir;
            else
                UserDirectory = Directory.GetCurrentDirectory() + @"\SCDownload";
        }

        //Collection of LauncherInfo
        public ObservableCollection<LauncherInfo> LauncherInfoCollection { get; set; } =
            new ObservableCollection<LauncherInfo>();

        private LauncherInfo _selectedLauncherInfo;

        public LauncherInfo SelectedLauncherInfo
        {
            get => _selectedLauncherInfo;
            set
            {
                _selectedLauncherInfo = value;
                //Maybe find a better way to do this in the future
                _ = SelectedLauncherInfoChanged();
            }
        }


        //Collection of build data
        public ObservableCollection<BuildData> BuildDataCollection { get; set; } =
            new ObservableCollection<BuildData>();

        public BuildData SelectedBuildData { get; set; }

        //Current status of Program
        public string CurrentStatus { get; set; } = "N/A";

        public MainWindowVM()
        {
            _progress = new Progress<double>(PrimaryDownloadProgressChanged);
            _fileProgress = new Progress<double>(FileProgressChanged);
            _bytesRecieved = new Progress<double>(DownloadSpeedChanged);
            SetUserDirectory();
            _ = DownloadPatchList();
        }

        //Progress of main progress bar
        private readonly IProgress<double> _progress;

        public double PrimaryProgressValue { get; set; }
        private void PrimaryDownloadProgressChanged(double v) => PrimaryProgressValue = v;

        //Progress of file progress bar
        private readonly IProgress<double> _fileProgress;

        public double FileProgress { get; set; }
        private void FileProgressChanged(double f) => FileProgress = f;

        //Current file label
        public string CurrentFileText { get; set; } = "...";

        //Speed label
        private readonly Stopwatch sw = new Stopwatch();

        public string SpeedText { get; set; } = "N/A MB/s";
        private readonly IProgress<double> _bytesRecieved;

        private void DownloadSpeedChanged(double b) => SpeedText =
            $"{b / 1024 / 1024 / sw.Elapsed.TotalSeconds:0.00} MB/s";

        //Download manifest on program startup
        public async Task DownloadPatchList()
        {
            try
            {
                string launcherInfoStr = await new DownloadHandlers().DownloadString(_progress,
                    "http://manifest.robertsspaceindustries.com/Launcher/_LauncherInfo");
                CurrentStatus = "Loading Manifest Complete";

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
                                    LauncherInfoCollection.Add(new LauncherInfo { UniverseType = item });
                                }
                            }
                            else
                            {
                                var info = LauncherInfoCollection.FirstOrDefault(x => lineitems[0]
                                    .StartsWith(x.UniverseType));
                                foreach (var field in fields)
                                {
                                    if (lineitems[0].EndsWith(field.Name, StringComparison.CurrentCultureIgnoreCase))
                                    {
                                        field.SetValue(info, lineitems[1]);
                                    }
                                }
                            }
                        }
                        if (LauncherInfoCollection.Count > 0)
                        {
                            SelectedLauncherInfo = LauncherInfoCollection[0];
                        }
                    }
                }
            }
            catch (WebException)
            {
                //TODO error dialog
                await new ErrorDialogHandler().ShowErrorDialog("WebException");
                Application.Current.Shutdown();
            }
        }

        public async Task SelectedLauncherInfoChanged()
        {
            string requestedUniverse = SelectedLauncherInfo.UniverseType;
            if (BuildDataCollection.ToDictionary(x => x.UniverseType)
                .TryGetValue(requestedUniverse, out BuildData checkedBuildData))
            {
                SelectedBuildData = checkedBuildData;
                CurrentStatus = $"{checkedBuildData.FileCount} files are ready for download";
                //TODO re-implement window states
                //SetWindowState(checkedBuildData.StoredControlState);
                return;
            }
            try
            {
                //SetWindowState(ControlStates.Busy);

                if (!string.IsNullOrEmpty(requestedUniverse))
                {
                    var buildDataURL = SelectedLauncherInfo.FileIndex;
                    string buildDataString = await new DownloadHandlers().DownloadString(_progress, buildDataURL);
                    BuildData buildData = JsonConvert.DeserializeObject<BuildData>(buildDataString);
                    buildData.UniverseType = requestedUniverse;
                    AddNewBuildInfo(buildData);
                }
            }
            catch (WebException e)
            {
                //TODO error dialog
                //MessageBox.Show("File list failed to download", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddNewBuildInfo(BuildData buildData)
        {
            BuildDataCollection.Add(buildData);
            CurrentStatus = $"{buildData.FileCount} files are ready for download";
            SelectedBuildData = buildData;
            //SetWindowState(buildData.StoredControlState);
        }

        private bool isDownloading;
        public bool IsReadytoDownload = true;

        private ICommand _downloadCommand;

        public ICommand DownloadCommand
        {
            get
            {
                if (_downloadCommand == null)
                    _downloadCommand = new RelayCommand(c => !isDownloading && IsReadytoDownload,
                        c => DownloadGameFiles());
                return _downloadCommand;
            }
        }


        //checkbox
        public bool notNative { get; set; } = true;

        public bool isNativeCheckEnabled { get; set; } = true;

        //the full path of the file
        private string _fulldir;

        //Download the selected build
        private async Task DownloadGameFiles()
        {
            double fileNum = 1;
            double totalFileNum = SelectedBuildData.FileCount;
            var randomws = new Random();

            string coreFileStructure = Path.Combine(UserDirectory,
                Utilities.GetFileStructure(notNative, SelectedBuildData.UniverseType, SelectedBuildData.KeyPrefix));

            var security = new DirectorySecurity();
            security.AddAccessRule(new FileSystemAccessRule(
                new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl,
                InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit,
                PropagationFlags.NoPropagateInherit, AccessControlType.Allow));

            try
            {
                isDownloading = true;
                _cancelDownloadSource = new CancellationTokenSource();
                foreach (string file in SelectedBuildData.FileList)
                {
                    FileProgress = 0;
                    CurrentStatus = $"Downloading file {fileNum} of {totalFileNum}";
                    var wsurl = new Uri(
                        new Uri(SelectedBuildData.WebseedURLs[randomws.Next(SelectedBuildData.WebseedURLs.Count)]),
                        SelectedBuildData.KeyPrefix.TrimStart('/'));
                    var downloadurl = new Uri($"{wsurl}/{file}");

                    CurrentFileText = file;
                    _fulldir = Path.Combine(coreFileStructure, file);

                    if (!File.Exists(_fulldir))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(_fulldir), security);
                        sw.Start();
                        await new DownloadHandlers().DownloadFile(_bytesRecieved, _fileProgress, downloadurl, _fulldir,
                            _cancelDownloadSource.Token);
                        if (_cancelDownloadSource.Token.IsCancellationRequested)
                        {
                            if (_fulldir != null)
                            {
                                File.Delete(_fulldir);
                            }
                            return;
                        }

                    }
                    fileNum += 1;

                    //Main progress bar is now based around overall status
                    PrimaryProgressValue = 100 * fileNum / totalFileNum;
                    sw.Reset();
                }
            }
            catch (DirectoryNotFoundException)
            {
                // MessageBox.Show("Unable to write to location, do you have permission?","DirectoryNotFoundException",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (IOException x)
            {
                //  MessageBox.Show("Unable to write to disk. Do you have enough space? Full Exception: " + x,"IOException", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (WebException x)
            {
                // MessageBox.Show($"Download failure, unable to continue. \nFull exception: {x.Message}", "WebException",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if ((int)fileNum - 1 == SelectedBuildData.WebseedURLs.Count)
            {
                // MessageBox.Show("Download Complete!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //SetWindowState(ControlStates.Default);
                CurrentStatus = "Download Complete!";
            }
        }

        //Download Cancel
        private ICommand _cancelCommand;

        public ICommand CancelCommand
        {
            get
            {
                if (_cancelCommand == null)
                    _cancelCommand = new RelayCommand(c => isDownloading, c => CancelDownload());
                return _cancelCommand;
            }
        }

        private CancellationTokenSource _cancelDownloadSource;

        private void CancelDownload()
        {
            // DialogResult result = MessageBox.Show("Are you sure you want to cancel?", "Cancel Download",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            _cancelDownloadSource.Cancel();
            CurrentStatus = "Download Cancelled";
            isDownloading = false;
        }
    }
}