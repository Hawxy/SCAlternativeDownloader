using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCPatchDownloader.Models
{
    public class LauncherInfo
    {
        public List<string> universes { get; set; }
        public string Public_universeServer { get; set; }
        public string Public_version { get; set; }
        public string Public_fileIndex { get; set; }
        public string Test_universeServer { get; set; }
        public string Test_version { get; set; }
        public string Test_fileIndex { get; set; }

    }
}
