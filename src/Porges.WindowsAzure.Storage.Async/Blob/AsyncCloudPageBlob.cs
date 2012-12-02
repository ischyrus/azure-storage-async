using Microsoft.WindowsAzure.Storage.Blob;

namespace Porges.WindowsAzure.Storage.Async.Blob
{
    public class AsyncCloudPageBlob : AsyncCloudBlobBase<CloudPageBlob>
    {
        public AsyncCloudPageBlob(CloudPageBlob page) : base(page)
        {
        }
    }
}