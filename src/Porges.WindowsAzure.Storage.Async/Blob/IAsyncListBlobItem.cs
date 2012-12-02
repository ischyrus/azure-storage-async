using System;

namespace Porges.WindowsAzure.Storage.Async.Blob
{
    public interface IAsyncListBlobItem
    {
        Uri Uri { get; }
        AsyncCloudBlobDirectory Parent { get; }
        AsyncCloudBlobContainer Container { get; }
    }
}