using System;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.RetryPolicies;
using Microsoft.WindowsAzure.Storage.Shared.Protocol;
using Microsoft.WindowsAzure.Storage.Table;
using PureAttribute = System.Diagnostics.Contracts.PureAttribute;

namespace Porges.WindowsAzure.Storage.Async.Table
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class AsyncCloudTableClient
    {
        private readonly CloudTableClient _inner;

        public AsyncCloudTableClient(CloudTableClient ctc)
        {
            _inner = ctc;
        }

        public TimeSpan? MaximumExecutionTime
        {
            [Pure] 
            get { return _inner.MaximumExecutionTime; }
            set { _inner.MaximumExecutionTime = value; }
        }

        public Uri BaseUri
        {
            [Pure] 
            get { return _inner.BaseUri; }
        }

        public TimeSpan? ServerTimeout
        {
            [Pure] 
            get { return _inner.ServerTimeout; }
            set { _inner.ServerTimeout = value; }
        }

        public IRetryPolicy RetryPolicy
        {
            [Pure] 
            get { return _inner.RetryPolicy; }
            set { _inner.RetryPolicy = value; }
        }

        public StorageCredentials Credentials
        {
            [Pure] 
            get { return _inner.Credentials; }
        }

        public AsyncCloudTableClient(CloudStorageAccount csa) : this(csa.CreateCloudTableClient())
        { }

        public AsyncCloudTableClient(Uri baseUri) : this(new CloudTableClient(baseUri))
        { }

        public AsyncCloudTableClient(Uri baseUri, StorageCredentials credentials) : this(new CloudTableClient(baseUri, credentials))
        { }

        [Pure] 
        public AsyncCloudTable GetTableReference(string tableAddress)
        {
            return new AsyncCloudTable(_inner.GetTableReference(tableAddress));
        }

        public Task<ServiceProperties> GetServiceProperties(TableRequestOptions requestOptions = null, OperationContext operationContext = null, CancellationToken? cancellationToken = null)
        {
            return AsyncTaskUtil.RunAsyncCancellable<ServiceProperties>(
                _inner.BeginGetServiceProperties(requestOptions, operationContext, null, null),
                _inner.EndGetServiceProperties,
                cancellationToken);
        }

        public Task SetServiceProperties(ServiceProperties properties, TableRequestOptions requestOptions = null, OperationContext operationContext = null, CancellationToken? cancellationToken = null)
        {
            return AsyncTaskUtil.RunAsyncCancellable(
                _inner.BeginSetServiceProperties(properties, requestOptions, operationContext, null, null),
                _inner.EndSetServiceProperties,
                cancellationToken);
        }

        private Task<TableResultSegment> ListTablesSegmented(string prefix, int? maxResults, TableContinuationToken tableToken, TableRequestOptions requestOptions = null, OperationContext operationContext = null, CancellationToken? cancellationToken = null)
        {
            return AsyncTaskUtil.RunAsyncCancellable<TableResultSegment>(
                _inner.BeginListTablesSegmented(prefix, maxResults, tableToken, requestOptions, operationContext, null, null),
                _inner.EndListTablesSegmented,
                cancellationToken);
        }

        public IObservable<AsyncCloudTable> ListTables(string prefix, int? maxResults, TableRequestOptions requestOptions = null, OperationContext operationContext = null, CancellationToken? cancellationToken = null)
        {
            return Observable.Create<AsyncCloudTable>(
            async (observer, ct) =>
            {
                var tableToken = new TableContinuationToken();
                while (tableToken != null)
                {
                    var results = await ListTablesSegmented(prefix, maxResults, tableToken, requestOptions, operationContext);
                    foreach (var result in results)
                    {
                        observer.OnNext(new AsyncCloudTable(result));
                    }

                    tableToken = results.ContinuationToken;
                }
            });
        }
    }
}
