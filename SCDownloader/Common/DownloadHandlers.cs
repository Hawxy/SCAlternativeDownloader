using System;
using System.Net;
using System.Threading;
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

        public async Task DownloadFile(IProgress<double> bytesRecieved, IProgress<double> progress, Uri address, string location, CancellationToken cancellationToken)
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    client.Proxy.Credentials = CredentialCache.DefaultNetworkCredentials;

                    client.DownloadProgressChanged += (o, e) =>
                    {
                        bytesRecieved.Report(e.BytesReceived);
                        progress.Report(e.ProgressPercentage);
                    };
                    client.DownloadFileCompleted += (o, e) =>
                    {
                        if (e.Cancelled)
                        {
                            client.Dispose();
                        }
                    };
                    using (cancellationToken.Register(client.CancelAsync))
                    {
                        await client.DownloadFileTaskAsync(address, location);
                    }

                }
            }
            catch (WebException ex) when (ex.Status == WebExceptionStatus.RequestCanceled)
            {
                // ignore this exception
            }
        }
    }
}
