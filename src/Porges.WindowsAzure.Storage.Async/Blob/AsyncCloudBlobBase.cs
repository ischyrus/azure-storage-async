using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Porges.WindowsAzure.Storage.Async.Blob
{
    public class AsyncCloudBlobBase<T> : AsyncListBlobItemBase<T>, IAsyncCloudBlob
        where T: class, ICloudBlob
    {
        public AsyncCloudBlobBase(T blob) : base(blob)
        { }
        
        public string Name { get { return Inner.Name; } }
        
        public AsyncCloudBlobClient ServiceClient
        {
            get { return new AsyncCloudBlobClient(Inner.ServiceClient); }
        }

        public int StreamWriteSizeInBytes
        {
            get { return Inner.StreamWriteSizeInBytes; }
            set { Inner.StreamWriteSizeInBytes = value; }
        }

        public int StreamMinimumReadSizeInBytes
        {
            get { return Inner.StreamMinimumReadSizeInBytes; }
            set { Inner.StreamMinimumReadSizeInBytes = value; }
        }

        public BlobProperties Properties { get { return Inner.Properties; } }
        public IDictionary<string, string> Metadata { get { return Inner.Metadata; } }
        public DateTimeOffset? SnapshotTime { get { return Inner.SnapshotTime; } }
        public CopyState CopyState { get { return Inner.CopyState; } }
        public BlobType BlobType { get { return Inner.BlobType; } }

        public string GetSharedAccessSignature(SharedAccessBlobPolicy policy)
        {
            return Inner.GetSharedAccessSignature(policy);
        }

        public string GetSharedAccessSignature(SharedAccessBlobPolicy policy, string groupPolicyIdentifier)
        {
            return Inner.GetSharedAccessSignature(policy, groupPolicyIdentifier);
        }

        public Task UploadFromStream(Stream source, AccessCondition accessCondition = null, BlobRequestOptions requestOptions = null,
                                     OperationContext operationContext = null, CancellationToken? cancellationToken = null)
        {
            return AsyncTaskUtil.RunAsyncCancellable(
                Inner.BeginUploadFromStream(source, accessCondition, requestOptions, operationContext, null, null),
                Inner.EndUploadFromStream,
                cancellationToken);
        }

        public Task DownloadToStream(Stream target, AccessCondition accessCondition = null, BlobRequestOptions requestOptions = null,
                                     OperationContext operationContext = null, CancellationToken? cancellationToken = null)
        {
            return AsyncTaskUtil.RunAsyncCancellable(
                Inner.BeginDownloadToStream(target, accessCondition, requestOptions, operationContext, null, null),
                Inner.EndDownloadToStream,
                cancellationToken);
        }

        public Task DownloadRangeToStream(Stream target, long? offset, long? length, AccessCondition accessCondition = null,
                                          BlobRequestOptions requestOptions = null, OperationContext operationContext = null,
                                          CancellationToken? cancellationToken = null)
        {
            return AsyncTaskUtil.RunAsyncCancellable(
                Inner.BeginDownloadRangeToStream(target, offset, length, accessCondition, requestOptions, operationContext, null, null),
                Inner.EndDownloadRangeToStream,
                cancellationToken);
        }

        public Task<bool> Exists(BlobRequestOptions requestOptions = null, OperationContext operationContext = null,
                                 CancellationToken? cancellationToken = null)
        {
            return AsyncTaskUtil.RunAsyncCancellable<bool>(
                Inner.BeginExists(requestOptions, operationContext, null, null),
                Inner.EndExists,
                cancellationToken);
        }

        public Task FetchAttributes(AccessCondition accessCondition = null, BlobRequestOptions requestOptions = null,
                                    OperationContext operationContext = null, CancellationToken? cancellationToken = null)
        {
            return AsyncTaskUtil.RunAsyncCancellable(
                Inner.BeginFetchAttributes(accessCondition, requestOptions, operationContext, null, null),
                Inner.EndFetchAttributes,
                cancellationToken);
        }


        public Task SetMetadata(AccessCondition accessCondition = null, BlobRequestOptions requestOptions = null,
                                OperationContext operationContext = null, CancellationToken? cancellationToken = null)
        {
            return AsyncTaskUtil.RunAsyncCancellable(
                Inner.BeginSetMetadata(accessCondition, requestOptions, operationContext, null, null),
                Inner.EndSetMetadata,
                cancellationToken);
        }

        public Task SetProperties(AccessCondition accessCondition = null, BlobRequestOptions requestOptions = null,
                                  OperationContext operationContext = null, CancellationToken? cancellationToken = null)
        {
            return AsyncTaskUtil.RunAsyncCancellable(
                Inner.BeginSetProperties(accessCondition, requestOptions, operationContext, null, null),
                Inner.EndSetProperties,
                cancellationToken);
        }

        public Task<string> StartCopyFromBlob(Uri source, AccessCondition sourceAccessCondition, AccessCondition destAccessCondition, BlobRequestOptions requestOptions = null,
                                              OperationContext operationContext = null, CancellationToken? cancellationToken = null)
        {
            return AsyncTaskUtil.RunAsyncCancellable<string>(
                Inner.BeginStartCopyFromBlob(source, sourceAccessCondition, destAccessCondition, requestOptions, operationContext, null, null),
                Inner.EndStartCopyFromBlob,
                cancellationToken);
        }

        public Task AbortCopy(string copyId, AccessCondition accessCondition = null, BlobRequestOptions requestOptions = null,
                              OperationContext operationContext = null, CancellationToken? cancellationToken = null)
        {
            return AsyncTaskUtil.RunAsyncCancellable(
                Inner.BeginAbortCopy(copyId, accessCondition, requestOptions, operationContext, null, null),
                Inner.EndAbortCopy,
                cancellationToken);
        }

        public Task<string> AcquireLease(TimeSpan? timespan, string proposedLeaseId, AccessCondition accessCondition = null,
                                         BlobRequestOptions requestOptions = null, OperationContext operationContext = null,
                                         CancellationToken? cancellationToken = null)
        {
            return AsyncTaskUtil.RunAsyncCancellable<string>(
                Inner.BeginAcquireLease(timespan, proposedLeaseId, accessCondition, requestOptions, operationContext, null, null),
                Inner.EndAcquireLease,
                cancellationToken);
        }

        public Task<TimeSpan> BreakLease(TimeSpan? breakPeriod = null, AccessCondition accessCondition = null,
                                         BlobRequestOptions requestOptions = null, OperationContext operationContext = null,
                                         CancellationToken? cancellationToken = null)
        {
            return AsyncTaskUtil.RunAsyncCancellable<TimeSpan>(
                Inner.BeginBreakLease(breakPeriod, accessCondition, requestOptions, operationContext, null, null),
                Inner.EndBreakLease,
                cancellationToken);
        }

        public Task RenewLease(AccessCondition accessCondition, BlobRequestOptions requestOptions = null,
                               OperationContext operationContext = null, CancellationToken? cancellationToken = null)
        {
            return AsyncTaskUtil.RunAsyncCancellable(
                Inner.BeginRenewLease(accessCondition, requestOptions, operationContext, null, null),
                Inner.EndRenewLease,
                cancellationToken);
        }

        public Task<string> ChangeLease(string proposedLeaseId, AccessCondition accessCondition, BlobRequestOptions requestOptions = null,
                                        OperationContext operationContext = null, CancellationToken? cancellationToken = null)
        {
            return AsyncTaskUtil.RunAsyncCancellable<string>(
                Inner.BeginChangeLease(proposedLeaseId, accessCondition, requestOptions, operationContext, null, null),
                Inner.EndChangeLease,
                cancellationToken);
        }

        public Task ReleaseLease(AccessCondition accessCondition, BlobRequestOptions requestOptions = null,
                                 OperationContext operationContext = null, CancellationToken? cancellationToken = null)
        {
            return AsyncTaskUtil.RunAsyncCancellable(
                Inner.BeginReleaseLease(accessCondition, requestOptions, operationContext, null, null),
                Inner.EndReleaseLease,
                cancellationToken);
        }


        public Task Delete(DeleteSnapshotsOption deleteSnapshotsOption = DeleteSnapshotsOption.None, AccessCondition accessCondition = null,
                           BlobRequestOptions requestOptions = null, OperationContext operationContext = null,
                           CancellationToken? cancellationToken = null)
        {
            return AsyncTaskUtil.RunAsyncCancellable(
                Inner.BeginDelete(deleteSnapshotsOption, accessCondition, requestOptions, operationContext, null, null),
                Inner.EndDelete,
                cancellationToken);
        }

        public Task<bool> DeleteIfExists(DeleteSnapshotsOption deleteSnapshotsOption = DeleteSnapshotsOption.None, AccessCondition accessCondition = null,
                                         BlobRequestOptions requestOptions = null, OperationContext operationContext = null,
                                         CancellationToken? cancellationToken = null)
        {
            return AsyncTaskUtil.RunAsyncCancellable<bool>(
                Inner.BeginDeleteIfExists(deleteSnapshotsOption, accessCondition, requestOptions, operationContext, null, null),
                Inner.EndDeleteIfExists,
                cancellationToken);
        }
    }
}