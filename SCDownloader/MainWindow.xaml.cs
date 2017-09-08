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

using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows;
using MaterialDesignThemes.Wpf;
using SCDownloader.Models;
using SCDownloader.Properties;

namespace SCDownloader
{
    public partial class MainWindow 
    {
        public string UserDirectory { get; set; }
        public enum ControlStates
        {
            Default,
            Busy,
            DownloadStart,
            NormalBuild,
            CustomBuild
        }

        private readonly Dictionary<string, LauncherInfo> launcherInfoDict = new Dictionary<string, LauncherInfo>();

        private readonly Dictionary<string, BuildData> buildDataDict = new Dictionary<string, BuildData>();

        private BuildData selectedBuildData = new BuildData();

        private readonly Stopwatch sw = new Stopwatch();

        private WebClient client;

        private string fulldir;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void DirectoryBox_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(Settings.Default.PrvDir))
                DirectoryBox.Text = Settings.Default.PrvDir;
            else
                DirectoryBox.Text = Directory.GetCurrentDirectory() + @"\SCDownload";
        }
    }
}
