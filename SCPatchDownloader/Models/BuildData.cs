using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCPatchDownloader.Models
{
    class BuildData
    {
        public ulong byte_count_total { get; set; }
        public int file_count_total { get; set; }
        public List<string> file_list { get; set; }
        public string key_prefix { get; set; }
        public List<string> webseed_urls { get; set; }
    }
}
