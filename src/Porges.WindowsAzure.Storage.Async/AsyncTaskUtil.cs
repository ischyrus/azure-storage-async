using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;

namespace Porges.WindowsAzure.Storage.Async
{
    internal static class AsyncTaskUtil
    {
        /// <summary>
        /// Runs a cancellable query as if it was a task.
        /// </summary>
        public static Task<T> RunAsyncCancellable<T>(ICancellableAsyncResult result, Func<IAsyncResult, T> completer, CancellationToken? ct)
        {
            if (ct.HasValue) ct.Value.Register(result.Cancel);
            return Task.Factory.FromAsync(result, r =>
            {
                try
                {
                    return completer(result);
                }
                catch (StorageException)
                {
                    if (ct.HasValue) ct.Value.ThrowIfCancellationRequested();

                    throw;
                }
            });
        }

        /// <summary>
        /// Runs a cancellable operation as if it was a task.
        /// </summary>
        public static Task RunAsyncCancellable(ICancellableAsyncResult result, Action<IAsyncResult> completer, CancellationToken? ct)
        {
            if (ct.HasValue) ct.Value.Register(result.Cancel);
            return Task.Factory.FromAsync(result, r =>
            {
                try
                {
                    completer(result);
                }
                catch (StorageException)
                {
                    if (ct.HasValue) ct.Value.ThrowIfCancellationRequested();

                    throw;
                }
            });
        }
    }
}