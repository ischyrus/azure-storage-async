using System;
using System.Diagnostics.Contracts;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Shared.Protocol;

namespace Porges.WindowsAzure.Storage.Async.Blob
{
    public class AsyncCloudBlobClient
    {
        private readonly CloudBlobClient _inner;
        public AsyncCloudBlobClient(CloudBlobClient client)
        {
            _inner = client;
        }

        public string DefaultDelimiter
        {
            [Pure]
            get { return _inner.DefaultDelimiter; }
            set { _inner.DefaultDelimiter = value; }
        }

        [Pure]
        public StorageCredentials Credentials
        {
            get { return _inner.Credentials; }
        }


        public AsyncCloudBlobClient(CloudStorageAccount account) : this(account.CreateCloudBlobClient())
        { }

        public AsyncCloudBlobClient(Uri baseUri) : this(new CloudBlobClient(baseUri))
        {}


        public AsyncCloudBlobClient(Uri baseUri, StorageCredentials credentials) : this(new CloudBlobClient(baseUri, credentials))
        { }

        public Task<ServiceProperties> GetServiceProperties(BlobRequestOptions requestOptions = null, OperationContext operationContext = null, CancellationToken? cancellationToken = null)
        {
            return AsyncTaskUtil.RunAsyncCancellable<ServiceProperties>(
                _inner.BeginGetServiceProperties(requestOptions, operationContext, null, null),
                _inner.EndGetServiceProperties,
                cancellationToken);
        }

        public Task SetServiceProperties(ServiceProperties properties, BlobRequestOptions requestOptions = null, OperationContext operationContext = null, CancellationToken? cancellationToken = null)
        {
            return AsyncTaskUtil.RunAsyncCancellable(
                _inner.BeginSetServiceProperties(properties, requestOptions, operationContext, null, null),
                _inner.EndSetServiceProperties,
                cancellationToken);
        }

        private Task<ContainerResultSegment> ListContainersSegmented(string prefix, ContainerListingDetails detailsIncluded, int? maxResults, BlobContinuationToken continuationToken, BlobRequestOptions options, OperationContext operationContext, CancellationToken? cancellationToken)
        {
            return AsyncTaskUtil.RunAsyncCancellable<ContainerResultSegment>(
                _inner.BeginListContainersSegmented(prefix, detailsIncluded, maxResults, continuationToken, options, operationContext, null, null),
                _inner.EndListContainersSegmented,
                cancellationToken);
        }

        public IObservable<AsyncCloudBlobContainer> ListContainers(string prefix, ContainerListingDetails detailsIncluded, int? maxResults, BlobRequestOptions options, OperationContext operationContext)
        {
            return Observable.Create<AsyncCloudBlobContainer>(
            async (observer, ct) =>
            {
                var containerToken = new BlobContinuationToken();
                while (containerToken != null)
                {
                    var results = await ListContainersSegmented(prefix, detailsIncluded, maxResults, containerToken, options, operationContext, ct);
                    foreach (var result in results.Results)
                    {
                        observer.OnNext(new AsyncCloudBlobContainer(result));
                    }

                    containerToken = results.ContinuationToken;
                }
            });
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

        public async Task<IAsyncCloudBlob> GetBlobReferenceFromServer(Uri blobUri, AccessCondition accessCondition = null, BlobRequestOptions requestOptions = null, OperationContext operationContext = null, CancellationToken? cancellationToken = null)
        {
            var blob = await AsyncTaskUtil.RunAsyncCancellable<ICloudBlob>(
                _inner.BeginGetBlobReferenceFromServer(blobUri, accessCondition, requestOptions, operationContext, null, null),
                _inner.EndGetBlobReferenceFromServer,
                cancellationToken);

            return AsyncCloudBlobHelpers.FromICloudBlob(blob);
        }

        [Pure]
        public AsyncCloudBlobContainer GetContainerReference(string containerName)
        {
            return new AsyncCloudBlobContainer(_inner.GetContainerReference(containerName));
        }
    }
}
