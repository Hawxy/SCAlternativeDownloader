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
using System.Collections.Generic;
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
using Microsoft.Win32;
using Newtonsoft.Json;
using Ookii.Dialogs.Wpf;
using SCDownloader.BoilerClasses;
using SCDownloader.Common;
using SCDownloader.Dialogs;
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

        //Collection of build data
        public ObservableCollection<BuildData> BuildDataCollection { get; set; } =
            new ObservableCollection<BuildData>();

        //ComboBox Selection
        private BuildData _selectedBuildData;
        public BuildData SelectedBuildData
        {
            get => _selectedBuildData;
            set
            {
                _selectedBuildData = value;
                CurrentStatus = $"{value.FileCount} files are ready for download";
                isNativeCheckEnabled = !value.IsCustom;
            }
        }

        //Current status of Program
        private const string DefaultCurrentStatus = "N/A";
        public string CurrentStatus { get; set; } = DefaultCurrentStatus;

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
        private const string DefaultCurrentFileText = "...";
        public string CurrentFileText { get; set; } = DefaultCurrentFileText;

        //Speed label
        private readonly Stopwatch sw = new Stopwatch();

        private const string DefaultSpeedText = "N/A MB/s";
        public string SpeedText { get; set; } = DefaultSpeedText;
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
                        List<LauncherInfo> LauncherInfoCollection = new List<LauncherInfo>();
                        var fields = typeof(LauncherInfo).GetProperties();
                        string line;
                        while (!string.IsNullOrEmpty(line = reader.ReadLine()))
                        {
                            //TODO: Clean this up and get rid of the properties I don't need
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
                            foreach (var i in LauncherInfoCollection)
                            {
                               await DownloadBuildData(i);
                            }
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                await new DialogHandler().ShowErrorDialog($"WebException: {ex.Message}");
                Application.Current.Shutdown();
            }
        }

        private async Task DownloadBuildData(LauncherInfo info)
        {
            var buildDataURL = info.FileIndex;
            string buildDataString = await new DownloadHandlers().DownloadString(_progress, buildDataURL);
            BuildData buildData = JsonConvert.DeserializeObject<BuildData>(buildDataString);
            AddNewBuildInfo(buildData, info.UniverseType);

        }

        private void AddNewBuildInfo(BuildData buildData, string universeType)
        {
            buildData.UniverseType = universeType;
            BuildDataCollection.Add(buildData);
            if (SelectedBuildData == null)
                SelectedBuildData = buildData;
            else if (buildData.IsCustom)
                SelectedBuildData = buildData;
        }

        private bool _isDownloading;

        public bool IsDownloading
        {
            get => _isDownloading;
            set
            {
                _isDownloading = value;
                IsSecondaryControlsEnabled = !value;
                if (!SelectedBuildData.IsCustom)
                    isNativeCheckEnabled = !value;
            }
        }

        public bool IsSecondaryControlsEnabled { get; set; } = true;
        public bool IsReadyToDownload = true;

        private ICommand _downloadCommand;

        public ICommand DownloadCommand
        {
            get
            {
                if (_downloadCommand == null)
                    _downloadCommand = new RelayCommand(c => !IsDownloading && IsReadyToDownload,
                        c => DownloadGameFiles());
                return _downloadCommand;
            }
        }

        //checkbox
        public bool notNative { get; set; } = true;

        public bool isNativeCheckEnabled { get; set; } = true;

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
                IsDownloading = true;
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
                    string _fulldir = Path.Combine(coreFileStructure, file);

                    if (!File.Exists(_fulldir))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(_fulldir), security);
                        sw.Start();
                        await new DownloadHandlers().DownloadFile(_bytesRecieved, _fileProgress, downloadurl, _fulldir,_cancelDownloadSource.Token);
                        if (_cancelDownloadSource.Token.IsCancellationRequested)
                        {
                            if (File.Exists(_fulldir))
                            {
                                File.Delete(_fulldir);
                            }
                            CurrentStatus = "Download Cancelled";
                            IsDownloading = false;
                            CurrentFileText = DefaultCurrentFileText;
                            SpeedText = DefaultSpeedText;
                            return;
                        }
                    }
                    fileNum += 1;

                    //Main progress bar is now based around overall status
                    PrimaryProgressValue = 100 * fileNum / totalFileNum;
                    sw.Reset();
                }
            }
            catch (DirectoryNotFoundException x)
            {
                await new DialogHandler().ShowErrorDialog($"DirectoryNotFoundException: Something has gone seriously wrong here. {x.Message}");
            }
            catch (IOException x)
            {
                await new DialogHandler().ShowErrorDialog($"Unable to write to disk. Full exception: {x.Message}");
            }
            catch (WebException x)
            {
                await new DialogHandler().ShowErrorDialog($"Download failure, unable to continue. Full exception: {x.Message}");
            }
            if ((int)fileNum - 1 == SelectedBuildData.WebseedURLs.Count)
            {
                // MessageBox.Show("Download Complete!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    _cancelCommand = new RelayCommand(c => IsDownloading, c => CancelDownload());
                return _cancelCommand;
            }
        }

        private CancellationTokenSource _cancelDownloadSource;

        private async void CancelDownload()
        {
            bool DialogResult = await new DialogHandler().ShowQuestionDialog("Are you sure you want to cancel?");
            if (DialogResult)
            {
                _cancelDownloadSource.Cancel();
            }
        }

        private ICommand _customBuildCommand;

        public ICommand CustomBuildCommand
        {
            get
            {
                if(_customBuildCommand == null)
                    _customBuildCommand = new RelayCommand(c => !IsDownloading, c=> CustomBuildAdd());
                return _customBuildCommand;
            }
        }

        private async Task CustomBuildAdd()
        {
            var selectFileDialog = new OpenFileDialog {Title = "Select build JSON", Filter = "JSON files|*.json"};
            if (selectFileDialog.ShowDialog() == true)
            {
                try
                {
                    var data = JsonConvert.DeserializeObject<BuildData>(File.ReadAllText(selectFileDialog.FileName));
                    string buildnumber = data.KeyPrefix.Split('/')[2];
                    data.IsCustom = true;
                    AddNewBuildInfo(data, buildnumber);
                }
                catch (Exception ex) when (ex is JsonException || ex is IndexOutOfRangeException)
                {
                    await new DialogHandler().ShowErrorDialog("Invalid build file");
                }
            }

        }

        private ICommand _pickDirectoryCommand;

        public ICommand PickDirectoryCommand
        {
            get
            {
                if(_pickDirectoryCommand == null)
                    _pickDirectoryCommand = new RelayCommand(c => !IsDownloading, c=> PickDirectory());
                return _pickDirectoryCommand;
            }
        }

        private void PickDirectory()
        {
            var folderDir = new VistaFolderBrowserDialog { ShowNewFolderButton = true };
            if (folderDir.ShowDialog() == true)
                UserDirectory = folderDir.SelectedPath;
        }

        private ICommand _githubCommand;

        public ICommand GithubCommand
        {
            get
            {
                if(_githubCommand == null)
                    _githubCommand = new RelayCommand(c => true, c=> OpenGithub());
                return _githubCommand;
            }
        }

        private void OpenGithub()
        {
            Process.Start("https://github.com/Hawxy/SCAlternativeDownloader");
        }
    }
}