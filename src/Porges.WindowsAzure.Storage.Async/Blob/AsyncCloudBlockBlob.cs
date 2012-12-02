using Microsoft.WindowsAzure.Storage.Blob;

namespace Porges.WindowsAzure.Storage.Async.Blob
{
    public class AsyncCloudBlockBlob : AsyncCloudBlobBase<CloudBlockBlob>
    {
        public AsyncCloudBlockBlob(CloudBlockBlob block) : base(block)
        { }
    }
}