using System;
using System.Net;
using System.Threading.Tasks;

namespace SCDownloader.Common
{
    internal class DownloadHandlers
    {
        public async Task<string> DownloadString(IProgress<double> progress, string address)
        {
            Uri Uri = new Uri(address);
            string downloadedItem;
            using (WebClient client = new WebClient())
            {
                client.Proxy.Credentials = CredentialCache.DefaultNetworkCredentials;
                client.DownloadProgressChanged += (o, e) =>
                {
                    progress.Report(e.ProgressPercentage);
                };

                downloadedItem = await client.DownloadStringTaskAsync(Uri);
            }
            return downloadedItem;
        }
    }
}
