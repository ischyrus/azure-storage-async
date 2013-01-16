using System;
using System.Diagnostics.Contracts;

namespace Porges.WindowsAzure.Storage.Async.Blob
{
    public interface IAsyncListBlobItem
    {
        [Pure]
        Uri Uri { get; }

        [Pure]
        AsyncCloudBlobDirectory Parent { get; }

        [Pure]
        AsyncCloudBlobContainer Container { get; }
    }
}