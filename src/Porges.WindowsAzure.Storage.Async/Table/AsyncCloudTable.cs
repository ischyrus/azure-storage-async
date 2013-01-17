using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using PureAttribute = System.Diagnostics.Contracts.PureAttribute;

namespace Porges.WindowsAzure.Storage.Async.Table
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class AsyncCloudTable
    {
        [NotNull] private readonly CloudTable _inner;

        public AsyncCloudTable([NotNull] CloudTable cloudTable)
        {
            _inner = cloudTable;
        }

        #region Non-Async methods

        public Uri Uri
        {
            [Pure] 
            get { return _inner.Uri; }
        }

        public string Name
        {
            [Pure] 
            get { return _inner.Name; }
        }

        public AsyncCloudTableClient ServiceClient
        {
            [Pure] 
            get { return new AsyncCloudTableClient(_inner.ServiceClient); }
        }
       
        [Pure]
        public string GetSharedAccessSignature(SharedAccessTablePolicy policy, string accessPolicyIdentifier, string startPartitionKey, string startRowKey, string endPartitionKey, string endRowKey)
        {
            return _inner.GetSharedAccessSignature(policy, accessPolicyIdentifier, startPartitionKey, startRowKey,
                                                   endPartitionKey, endRowKey);
        }

        #endregion

        #region Async methods

        public Task Create(TableRequestOptions requestOptions = null, OperationContext operationContext = null, CancellationToken? cancellationToken = null)
        {
            return AsyncTaskUtil.RunAsyncCancellable(
                _inner.BeginCreate(requestOptions, operationContext, null, null),
                _inner.EndCreate,
                cancellationToken);
        }

        public Task<bool> CreateIfNotExists(TableRequestOptions requestOptions = null, OperationContext operationContext = null, CancellationToken? cancellationToken = null)
        {
            return AsyncTaskUtil.RunAsyncCancellable<bool>(
                _inner.BeginCreateIfNotExists(requestOptions, operationContext, null, null),
                _inner.EndCreateIfNotExists,
                cancellationToken);
        }

        public Task Delete(TableRequestOptions requestOptions = null, OperationContext operationContext = null, CancellationToken? cancellationToken = null)
        {
            return AsyncTaskUtil.RunAsyncCancellable(
                _inner.BeginDelete(requestOptions, operationContext, null, null),
                _inner.EndDelete,
                cancellationToken);
        }

        public Task<bool> DeleteIfExists(TableRequestOptions requestOptions = null, OperationContext operationContext = null, CancellationToken? cancellationToken = null)
        {
            return AsyncTaskUtil.RunAsyncCancellable<bool>(
                _inner.BeginDeleteIfExists(requestOptions, operationContext, null, null),
                _inner.EndDeleteIfExists,
                cancellationToken);
        }

        public Task<bool> Exists(TableRequestOptions requestOptions = null, OperationContext operationContext = null, CancellationToken? cancellationToken = null)
        {
            return AsyncTaskUtil.RunAsyncCancellable<bool>(
                _inner.BeginExists(requestOptions, operationContext, null, null),
                _inner.EndExists,
                cancellationToken);
        }

        public Task<TablePermissions> GetPermissions(TableRequestOptions requestOptions = null, OperationContext operationContext = null, CancellationToken? cancellationToken = null)
        {
            return AsyncTaskUtil.RunAsyncCancellable<TablePermissions>(
                _inner.BeginGetPermissions(requestOptions, operationContext, null, null),
                _inner.EndGetPermissions,
                cancellationToken);
        }

        public Task SetPermissions(TablePermissions permissions, TableRequestOptions requestOptions = null, OperationContext operationContext = null, CancellationToken? cancellationToken = null)
        {
            return AsyncTaskUtil.RunAsyncCancellable(
                _inner.BeginSetPermissions(permissions, requestOptions, operationContext, null, null),
                _inner.EndSetPermissions,
                cancellationToken);
        }

        public Task<TableResult> Execute(TableOperation operation, TableRequestOptions requestOptions = null, OperationContext operationContext = null, CancellationToken? cancellationToken = null)
        {
            return AsyncTaskUtil.RunAsyncCancellable<TableResult>(
                _inner.BeginExecute(operation, requestOptions, operationContext, null, null),
                _inner.EndExecute,
                cancellationToken);
        }

        public Task<IList<TableResult>> ExecuteBatch(TableBatchOperation operation, TableRequestOptions requestOptions = null, OperationContext operationContext = null, CancellationToken? cancellationToken = null)
        {
            return AsyncTaskUtil.RunAsyncCancellable<IList<TableResult>>(
                _inner.BeginExecuteBatch(operation, requestOptions, operationContext, null, null),
                _inner.EndExecuteBatch,
                cancellationToken);
        }

        private Task<TableQuerySegment<T>> ExecuteQuerySegmented<T>(TableQuery<T> query, TableContinuationToken token, TableRequestOptions requestOptions = null, OperationContext operationContext = null, CancellationToken? cancellationToken = null) where T : ITableEntity, new()
        {
            return AsyncTaskUtil.RunAsyncCancellable<TableQuerySegment<T>>(
                _inner.BeginExecuteQuerySegmented(query, token, requestOptions, operationContext, null, null),
                _inner.EndExecuteQuerySegmented<T>,
                 cancellationToken);
        }
        
        public IObservable<T> ExecuteQuery<T>(TableQuery<T> query, TableRequestOptions requestOptions = null, OperationContext operationContext = null) where T : ITableEntity, new()
        {
            return Observable.Create<T>(
                async (observer, ct) =>
                {
                    var tableToken = new TableContinuationToken();
                    while (tableToken != null)
                    {
                        var results = await ExecuteQuerySegmented(query, tableToken, requestOptions, operationContext, ct);
                        foreach (var result in results)
                        {
                            observer.OnNext(result);
                        }

                        tableToken = results.ContinuationToken;
                    }
                });
        }
        
        #endregion
    }
}