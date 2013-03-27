using System;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.Storage.RetryPolicies;

namespace Porges.WindowsAzure.Storage.Async.Queue
{
    [PublicAPI]
    public class AsyncCloudQueueClient
    {
        [NotNull] private readonly CloudQueueClient _inner;

        public AsyncCloudQueueClient([NotNull] CloudQueueClient cqc)
        {
            _inner = cqc;

        }

        public AsyncCloudQueueClient(CloudStorageAccount csa)
            : this(csa.CreateCloudQueueClient())
        {
        }

        public AsyncCloudQueueClient(Uri baseUri)
            : this(new CloudQueueClient(baseUri))
        {
        }

        public AsyncCloudQueueClient(Uri baseUri, StorageCredentials credentials)
            : this(new CloudQueueClient(baseUri, credentials))
        {
        }

        #region Non-Async methods

        public Uri BaseUri
        {
            [System.Diagnostics.Contracts.Pure]
            get { return _inner.BaseUri; }
        }

        public StorageCredentials Credentials
        {
            [System.Diagnostics.Contracts.Pure]
            get { return _inner.Credentials; }
        }

        public IRetryPolicy RetryPolicy
        {
            [System.Diagnostics.Contracts.Pure]
            get { return _inner.RetryPolicy; }
            set { _inner.RetryPolicy = value; }
        }

        public TimeSpan? MaximumExecutionTime
        {
            [System.Diagnostics.Contracts.Pure]
            get { return _inner.MaximumExecutionTime; }
            set { _inner.MaximumExecutionTime = value; }
        }

        public TimeSpan? ServerTimeout
        {
            [System.Diagnostics.Contracts.Pure]
            get { return _inner.ServerTimeout; }
            set { _inner.ServerTimeout = value; }
        }
        
        public AsyncCloudQueue GetQueueReference(string queueName)
        {
            return new AsyncCloudQueue(_inner.GetQueueReference(queueName));
        }

        #endregion

        #region Async methods

        
        #endregion
    }
}
