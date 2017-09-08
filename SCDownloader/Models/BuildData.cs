using System.Collections.Generic;
using Newtonsoft.Json;

namespace SCDownloader.Models
{
    class BuildData
    {
        [JsonProperty("byte_count_total")]
        public ulong ByteCountTotal { get; set; }
        [JsonProperty("file_count_total")]
        public int FileCount { get; set; }
        [JsonProperty("file_list")]
        public List<string> FileList { get; set; }
        [JsonProperty("key_prefix")]
        public string KeyPrefix { get; set; }
        [JsonProperty("webseed_urls")]
        public List<string> WebseedURLs { get; set; }
        [JsonIgnore]
        public MainWindow.ControlStates StoredControlState { get; set; } = MainWindow.ControlStates.NormalBuild;
    }
}
