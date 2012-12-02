using Microsoft.WindowsAzure.Storage.Blob;

namespace Porges.WindowsAzure.Storage.Async.Blob
{
    public class AsyncCloudBlobDirectory : AsyncListBlobItemBase<CloudBlobDirectory>
    {

        public AsyncCloudBlobDirectory(CloudBlobDirectory item): base(item)
        {
        }

        public AsyncCloudBlobDirectory GetSubdirectoryReference(string name)
        {
            return new AsyncCloudBlobDirectory(Inner.GetSubdirectoryReference(name));
        }

        public string Prefix
        {
            get { return Inner.Prefix; }
        }
    }
}