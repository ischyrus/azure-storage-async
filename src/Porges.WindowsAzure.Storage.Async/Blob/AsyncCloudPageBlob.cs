using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Porges.WindowsAzure.Storage.Async.Blob
{
    public class AsyncCloudPageBlob : AsyncCloudBlobBase<CloudPageBlob>
    {
        public AsyncCloudPageBlob(CloudPageBlob page) : base(page)
        { }

        public Task ClearPages(long startOffset, long length,
            AccessCondition accessCondition = null, BlobRequestOptions options = null, OperationContext operationContext = null, CancellationToken? cancellationToken = null)
        {
            return AsyncTaskUtil.RunAsyncCancellable(
                Inner.BeginClearPages(startOffset, length, accessCondition, options, operationContext, null, null),
                Inner.EndClearPages,
                cancellationToken);
        }

        public Task Resize(long size,
            AccessCondition accessCondition = null, BlobRequestOptions options = null, OperationContext operationContext = null, CancellationToken? cancellationToken = null)
        {
            return AsyncTaskUtil.RunAsyncCancellable(
                Inner.BeginResize(size, accessCondition, options, operationContext, null, null),
                Inner.EndResize,
                cancellationToken);
        }


        public Task WritePages(Stream pageData, long startOffset, string contentMD5 = null,
            AccessCondition accessCondition = null, BlobRequestOptions options = null, OperationContext operationContext = null, CancellationToken? cancellationToken = null)
        {
            return AsyncTaskUtil.RunAsyncCancellable(
                Inner.BeginWritePages(pageData, startOffset, contentMD5, accessCondition, options, operationContext, null, null),
                Inner.EndWritePages,
                cancellationToken);
        }
    }
}