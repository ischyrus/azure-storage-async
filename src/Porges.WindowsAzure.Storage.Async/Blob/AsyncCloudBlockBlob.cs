using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Porges.WindowsAzure.Storage.Async.Blob
{
    public class AsyncCloudBlockBlob : AsyncCloudBlobBase<CloudBlockBlob>
    {
        public AsyncCloudBlockBlob(CloudBlockBlob block) : base(block)
        {
        }

        public Task<IEnumerable<ListBlockItem>> DownloadBlockList(BlockListingFilter blockListingFilter,
            AccessCondition accessCondition = null, BlobRequestOptions options = null, OperationContext operationContext = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            return AsyncTaskUtil.RunAsyncCancellable<IEnumerable<ListBlockItem>>(
                Inner.BeginDownloadBlockList(blockListingFilter, accessCondition, options, operationContext, null, null),
                Inner.EndDownloadBlockList,
                cancellationToken);
        }

        public Task PutBlock(string blockId, Stream blockData, string contentMD5 = null,
            AccessCondition accessCondition = null, BlobRequestOptions options = null, OperationContext operationContext = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            return AsyncTaskUtil.RunAsyncCancellable(
                Inner.BeginPutBlock(blockId, blockData, contentMD5, accessCondition, options, operationContext, null, null),
                Inner.EndPutBlock,
                cancellationToken);
        }

        public Task PutBlockList(IEnumerable<string> blockList,
            AccessCondition accessCondition = null, BlobRequestOptions options = null, OperationContext operationContext = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            return AsyncTaskUtil.RunAsyncCancellable(
                Inner.BeginPutBlockList(blockList, accessCondition, options, operationContext, null, null),
                Inner.EndPutBlockList,
                cancellationToken);
        }
    }
}