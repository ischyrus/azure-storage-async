using System;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Porges.WindowsAzure.Storage.Async.Blob
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class AsyncCloudBlobContainer 
    {
        private readonly CloudBlobContainer _inner;

        public AsyncCloudBlobContainer(CloudBlobContainer container)
        {
            _inner = container;
        }

        public string Name
        {
            get { return _inner.Name; }
        }

        public Task Create(BlobRequestOptions options = null, OperationContext operationContext = null, CancellationToken? cancellationToken = null)
        {
            return AsyncTaskUtil.RunAsyncCancellable(_inner.BeginCreate(options, operationContext, null, null), _inner.EndCreate, cancellationToken);
        }

        public Task Delete(AccessCondition accessCondition = null, BlobRequestOptions options = null, OperationContext operationContext = null, CancellationToken? cancellationToken = null)
        {
            return AsyncTaskUtil.RunAsyncCancellable(_inner.BeginDelete(accessCondition, options, operationContext, null, null), _inner.EndDelete, cancellationToken);
        }

        public Task<bool> CreateIfNotExists(BlobRequestOptions options = null, OperationContext operationContext = null, CancellationToken? cancellationToken = null)
        {
            return AsyncTaskUtil.RunAsyncCancellable<bool>(_inner.BeginCreateIfNotExists(options, operationContext, null, null), _inner.EndCreateIfNotExists, cancellationToken);
        }

        public Task<bool> DeleteIfExists(AccessCondition accessCondition = null, BlobRequestOptions options = null, OperationContext operationContext = null, CancellationToken? cancellationToken = null)
        {
            return AsyncTaskUtil.RunAsyncCancellable<bool>(_inner.BeginDeleteIfExists(accessCondition, options, operationContext, null, null), _inner.EndDeleteIfExists, cancellationToken);
        }


        private Task<BlobResultSegment> ListBlobsSegmented(string prefix, bool useFlatBlobListing, BlobListingDetails blobListingDetails, int? maxResults, BlobContinuationToken continuationToken, BlobRequestOptions options, OperationContext operationContext, CancellationToken? cancellationToken)
        {
            return AsyncTaskUtil.RunAsyncCancellable<BlobResultSegment>(
                _inner.BeginListBlobsSegmented(prefix, useFlatBlobListing, blobListingDetails, maxResults, continuationToken, options, operationContext, null, null),
                _inner.EndListBlobsSegmented,
                cancellationToken);
        }

        public IObservable<IAsyncListBlobItem> ListBlobs(string prefix, bool useFlatBlobListing, BlobListingDetails blobListingDetails = BlobListingDetails.None, int? maxResults = null, BlobRequestOptions options = null, OperationContext operationContext = null)
        {
            return Observable.Create<IAsyncListBlobItem>(
            async (observer, ct) =>
            {
                var containerToken = new BlobContinuationToken();
                while (containerToken != null)
                {
                    var results = await ListBlobsSegmented(prefix, useFlatBlobListing, blobListingDetails, maxResults, containerToken, options, operationContext, ct);
                    foreach (var result in results.Results)
                    {
                        observer.OnNext(AsyncListBlobItemHelpers.FromIListBlobItem(result));
                    }

                    containerToken = results.ContinuationToken;
                }
            });
        }
    }
}