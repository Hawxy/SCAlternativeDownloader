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
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
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
                UserDirectory= Settings.Default.PrvDir;
            else
                UserDirectory = Directory.GetCurrentDirectory() + @"\SCDownload";
        }

        //Collection of LauncherInfo
        public ObservableCollection<LauncherInfo> LauncherInfoCollection { get; set; } = new ObservableCollection<LauncherInfo>();

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
        public ObservableCollection<BuildData> BuildDataCollection { get; set; } = new ObservableCollection<BuildData>();
        public BuildData SelectedBuildData { get; set; }

        //Current status of Program
        public string CurrentStatus { get; set; } = "N/A";

        public MainWindowVM()
        {
            _progress = new Progress<double>(PrimaryDownloadProgressChanged);
            SetUserDirectory();
           _ = DownloadPatchList();
        }

        //Progress of main progress bar
        private readonly IProgress<double> _progress;
        public double PrimaryProgressValue { get; set; }
        private void PrimaryDownloadProgressChanged(double v) => PrimaryProgressValue = v;
        
        //Download manifest on program startup
        public async Task DownloadPatchList()
        {
            try
            {
                string launcherInfoStr = await new DownloadHandlers().DownloadString(_progress, "http://manifest.robertsspaceindustries.com/Launcher/_LauncherInfo");
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
                                    LauncherInfoCollection.Add(new LauncherInfo{UniverseType = item});
                                }
                            }
                            else
                            {
                                var info = LauncherInfoCollection.FirstOrDefault(x => lineitems[0].StartsWith(x.UniverseType));
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
                // MessageBox.Show("Unable to download manifest. Exiting.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Current.Shutdown();
            }
        }

        public async Task SelectedLauncherInfoChanged()
        {
            string requestedUniverse = SelectedLauncherInfo.UniverseType;
            if (BuildDataCollection.ToDictionary(x => x.UniverseType).TryGetValue(requestedUniverse, out BuildData checkedBuildData))
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
    }
}
