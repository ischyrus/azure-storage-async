using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using PureAttribute = System.Diagnostics.Contracts.PureAttribute;

namespace Porges.WindowsAzure.Storage.Async.Blob
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public interface IAsyncCloudBlob : IAsyncListBlobItem
    {
        /// <inheritdoc cref="ICloudBlob.Name" />
        string Name { [Pure] get; }

        /// <inheritdoc cref="ICloudBlob.ServiceClient" />
        AsyncCloudBlobClient ServiceClient { [Pure] get; }

        /// <inheritdoc cref="ICloudBlob.StreamWriteSizeInBytes" />
        int StreamWriteSizeInBytes { [Pure] get; set; }

        /// <inheritdoc cref="ICloudBlob.StreamMinimumReadSizeInBytes" />
        int StreamMinimumReadSizeInBytes { [Pure] get; set; }

        /// <inheritdoc cref="ICloudBlob.Properties" />
        BlobProperties Properties { [Pure] get; }

        /// <inheritdoc cref="ICloudBlob.Metadata" />
        IDictionary<string, string> Metadata { [Pure] get; }

        /// <inheritdoc cref="ICloudBlob.SnapshotTime" />
        DateTimeOffset? SnapshotTime { [Pure] get; }

        /// <inheritdoc cref="ICloudBlob.CopyState" />
        CopyState CopyState { [Pure] get; }

        /// <inheritdoc cref="ICloudBlob.BlobType" />
        BlobType BlobType { [Pure] get; }

        /// <inheritdoc cref="ICloudBlob.GetSharedAccessSignature(Microsoft.WindowsAzure.Storage.Blob.SharedAccessBlobPolicy)" />
        [Pure] 
        string GetSharedAccessSignature(SharedAccessBlobPolicy policy);

        /// <inheritdoc cref="ICloudBlob.GetSharedAccessSignature(Microsoft.WindowsAzure.Storage.Blob.SharedAccessBlobPolicy, string)" />
        [Pure] 
        string GetSharedAccessSignature(SharedAccessBlobPolicy policy, string groupPolicyIdentifier);

        /// <inheritdoc cref="ICloudBlob.UploadFromStream" />
        Task UploadFromStream(Stream source,
            AccessCondition accessCondition = null,
            BlobRequestOptions requestOptions = null,
            OperationContext operationContext = null,
            CancellationToken? cancellationToken = null);

        /// <inheritdoc cref="ICloudBlob.DownloadToStream" />
        Task DownloadToStream(Stream target,
            AccessCondition accessCondition = null,
            BlobRequestOptions requestOptions = null,
            OperationContext operationContext = null,
            CancellationToken? cancellationToken = null);

        /// <inheritdoc cref="ICloudBlob.DownloadRangeToStream" />
        Task DownloadRangeToStream(Stream target, long? offset, long? length,
            AccessCondition accessCondition = null,
            BlobRequestOptions requestOptions = null,
            OperationContext operationContext = null,
            CancellationToken? cancellationToken = null);

        /// <inheritdoc cref="ICloudBlob.Exists" />
        Task<bool> Exists(
            BlobRequestOptions requestOptions = null,
            OperationContext operationContext = null,
            CancellationToken? cancellationToken = null);

        /// <inheritdoc cref="ICloudBlob.FetchAttributes" />
        Task FetchAttributes(
            AccessCondition accessCondition = null,
            BlobRequestOptions requestOptions = null,
            OperationContext operationContext = null,
            CancellationToken? cancellationToken = null);

        /// <inheritdoc cref="ICloudBlob.SetMetadata" />
        Task SetMetadata(
            AccessCondition accessCondition = null,
            BlobRequestOptions requestOptions = null,
            OperationContext operationContext = null,
            CancellationToken? cancellationToken = null);

        /// <inheritdoc cref="ICloudBlob.SetProperties" />
        Task SetProperties(
            AccessCondition accessCondition = null,
            BlobRequestOptions requestOptions = null,
            OperationContext operationContext = null,
            CancellationToken? cancellationToken = null);

        /// <inheritdoc cref="ICloudBlob.StartCopyFromBlob" />
        Task<string> StartCopyFromBlob(Uri source, AccessCondition sourceAccessCondition, AccessCondition destAccessCondition,
            BlobRequestOptions requestOptions = null,
            OperationContext operationContext = null,
            CancellationToken? cancellationToken = null);

        /// <inheritdoc cref="ICloudBlob.AbortCopy" />
        Task AbortCopy(string copyId,
            AccessCondition accessCondition = null,
            BlobRequestOptions requestOptions = null,
            OperationContext operationContext = null,
            CancellationToken? cancellationToken = null);

        /// <inheritdoc cref="ICloudBlob.AcquireLease" />
        Task<string> AcquireLease(TimeSpan? timespan, string proposedLeaseId,
            AccessCondition accessCondition = null,
            BlobRequestOptions requestOptions = null,
            OperationContext operationContext = null,
            CancellationToken? cancellationToken = null);

        /// <inheritdoc cref="ICloudBlob.BreakLease" />
        Task<TimeSpan> BreakLease(TimeSpan? breakPeriod = null,
            AccessCondition accessCondition = null,
            BlobRequestOptions requestOptions = null,
            OperationContext operationContext = null,
            CancellationToken? cancellationToken = null);

        /// <inheritdoc cref="ICloudBlob.RenewLease" />
        Task RenewLease(AccessCondition accessCondition,
            BlobRequestOptions requestOptions = null,
            OperationContext operationContext = null,
            CancellationToken? cancellationToken = null);

        /// <inheritdoc cref="ICloudBlob.ChangeLease" />
        Task<string> ChangeLease(string proposedLeaseId, AccessCondition accessCondition,
            BlobRequestOptions requestOptions = null,
            OperationContext operationContext = null,
            CancellationToken? cancellationToken = null);

        /// <inheritdoc cref="ICloudBlob.ReleaseLease" />
        Task ReleaseLease(AccessCondition accessCondition,
            BlobRequestOptions requestOptions = null,
            OperationContext operationContext = null,
            CancellationToken? cancellationToken = null);

        /// <inheritdoc cref="ICloudBlob.Delete" />
        Task Delete(DeleteSnapshotsOption deleteSnapshotsOption = DeleteSnapshotsOption.None,
            AccessCondition accessCondition = null,
            BlobRequestOptions requestOptions = null,
            OperationContext operationContext = null,
            CancellationToken? cancellationToken = null);

        /// <inheritdoc cref="ICloudBlob.DeleteIfExists" />
        Task<bool> DeleteIfExists(DeleteSnapshotsOption deleteSnapshotsOption = DeleteSnapshotsOption.None,
            AccessCondition accessCondition = null,
            BlobRequestOptions requestOptions = null,
            OperationContext operationContext = null,
            CancellationToken? cancellationToken = null);
    }
}