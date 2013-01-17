using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.Storage.Queue.Protocol;

namespace Porges.WindowsAzure.Storage.Async.Queue
{
    public class AsyncCloudQueue
    {
        [NotNull]
        private readonly CloudQueue _inner;

        public AsyncCloudQueue([NotNull] CloudQueue queue)
        {
            _inner = queue;
        }

        public Task Create(QueueRequestOptions options = null, OperationContext operationContext = null, CancellationToken? cancellationToken = null)
        {
            return AsyncTaskUtil.RunAsyncCancellable(
                _inner.BeginCreate(options, operationContext, null, null),
                _inner.EndCreate,
                cancellationToken);
        }

        public Task<bool> CreateIfNotExists(QueueRequestOptions options = null, OperationContext operationContext = null, CancellationToken? cancellationToken = null)
        {
            return AsyncTaskUtil.RunAsyncCancellable<bool>(
                _inner.BeginCreateIfNotExists(options, operationContext, null, null),
                _inner.EndCreateIfNotExists,
                cancellationToken);
        }

        public Task Delete(QueueRequestOptions options = null, OperationContext operationContext = null, CancellationToken? cancellationToken = null)
        {
            return AsyncTaskUtil.RunAsyncCancellable(
                _inner.BeginDelete(options, operationContext, null, null),
                _inner.EndDelete,
                cancellationToken);
        }

        public Task<bool> DeleteIfExists(QueueRequestOptions options = null, OperationContext operationContext = null, CancellationToken? cancellationToken = null)
        {
            return AsyncTaskUtil.RunAsyncCancellable<bool>(
                _inner.BeginDeleteIfExists(options, operationContext, null, null),
                _inner.EndDeleteIfExists,
                cancellationToken);
        }

        public Task Clear(QueueRequestOptions options = null, OperationContext operationContext = null, CancellationToken? cancellationToken = null)
        {
            return AsyncTaskUtil.RunAsyncCancellable(
                _inner.BeginClear(options, operationContext, null, null),
                _inner.EndBeginClear,
                cancellationToken);
        }

        public Task AddMessage(CloudQueueMessage message, TimeSpan? timeToLive = null, TimeSpan? initialVisibilityDelay = null, QueueRequestOptions options = null, OperationContext operationContext = null, CancellationToken? cancellationToken = null)
        {
            return AsyncTaskUtil.RunAsyncCancellable(
                _inner.BeginAddMessage(message, timeToLive, initialVisibilityDelay, options, operationContext, null, null),
                _inner.EndAddMessage,
                cancellationToken);
        }

        public Task DeleteMessage(CloudQueueMessage message, QueueRequestOptions options = null, OperationContext operationContext = null, CancellationToken? cancellationToken = null)
        {
            return AsyncTaskUtil.RunAsyncCancellable(
                _inner.BeginDeleteMessage(message, options, operationContext, null, null),
                _inner.EndDeleteMessage,
                cancellationToken);
        }

        public Task DeleteMessage(string messageId, string popReceipt, QueueRequestOptions options = null, OperationContext operationContext = null, CancellationToken? cancellationToken = null)
        {
            return AsyncTaskUtil.RunAsyncCancellable(
                _inner.BeginDeleteMessage(messageId, popReceipt, options, operationContext, null, null),
                _inner.EndDeleteMessage,
                cancellationToken);   
        }

        public Task<bool> Exists(QueueRequestOptions options = null, OperationContext operationContext = null, CancellationToken? cancellationToken = null)
        {
            return AsyncTaskUtil.RunAsyncCancellable<bool>(
                _inner.BeginExists(options, operationContext, null, null),
                _inner.EndExists,
                cancellationToken);
        }

        public Task FetchAttributes(QueueRequestOptions options = null, OperationContext operationContext = null, CancellationToken? cancellationToken = null)
        {
            return AsyncTaskUtil.RunAsyncCancellable(
                _inner.BeginFetchAttributes(options, operationContext, null, null),
                _inner.EndFetchAttributes,
                cancellationToken);
        }

        public Task<CloudQueueMessage> GetMessage(TimeSpan? visibilityTimeout = null, QueueRequestOptions options = null, OperationContext operationContext = null, CancellationToken? cancellationToken = null)
        {
            return AsyncTaskUtil.RunAsyncCancellable<CloudQueueMessage>(
                _inner.BeginGetMessage(visibilityTimeout, options, operationContext, null, null),
                _inner.EndGetMessage,
                cancellationToken);
        }

        public Task<IEnumerable<CloudQueueMessage>> GetMessages(int messageCount, TimeSpan? visibilityTimeout = null, QueueRequestOptions options = null, OperationContext operationContext = null, CancellationToken? cancellationToken = null)
        {
            return AsyncTaskUtil.RunAsyncCancellable<IEnumerable<CloudQueueMessage>>(
                _inner.BeginGetMessages(messageCount, visibilityTimeout, options, operationContext, null, null),
                _inner.EndGetMessages,
                cancellationToken);
        }

        public Task<CloudQueueMessage> PeekMessage(QueueRequestOptions options = null, OperationContext operationContext = null, CancellationToken? cancellationToken = null)
        {
            return AsyncTaskUtil.RunAsyncCancellable<CloudQueueMessage>(
                _inner.BeginPeekMessage(options, operationContext, null, null),
                _inner.EndPeekMessage,
                cancellationToken);
        }

        public Task<IEnumerable<CloudQueueMessage>> PeekMessages(int messageCount, QueueRequestOptions options = null, OperationContext operationContext = null, CancellationToken? cancellationToken = null)
        {
            return AsyncTaskUtil.RunAsyncCancellable<IEnumerable<CloudQueueMessage>>(
                _inner.BeginPeekMessages(messageCount, options, operationContext, null, null),
                _inner.EndPeekMessages,
                cancellationToken);
        }

        public Task<QueuePermissions> GetPermissions(QueueRequestOptions options = null, OperationContext operationContext = null, CancellationToken? cancellationToken = null)
        {
            return AsyncTaskUtil.RunAsyncCancellable<QueuePermissions>(
                _inner.BeginGetPermissions(options, operationContext, null, null),
                _inner.EndGetPermissions,
                cancellationToken);
        }

        public Task SetPermissions(QueuePermissions permissions, QueueRequestOptions options = null, OperationContext operationContext = null, CancellationToken? cancellationToken = null)
        {
            return AsyncTaskUtil.RunAsyncCancellable(
                _inner.BeginSetPermissions(permissions, options, operationContext, null, null),
                _inner.EndSetPermissions,
                cancellationToken);
        }


    }
}