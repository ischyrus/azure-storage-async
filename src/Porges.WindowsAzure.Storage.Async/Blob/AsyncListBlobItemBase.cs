using System;
using System.Diagnostics.Contracts;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Porges.WindowsAzure.Storage.Async.Blob
{
    public class AsyncListBlobItemBase<T> : IAsyncListBlobItem
        where T : class, IListBlobItem
    {
        protected readonly T Inner;

        public AsyncListBlobItemBase(T item)
        {
            Inner = item;
        }

        [Pure] 
        public Uri Uri
        {
            get { return Inner.Uri; }
        }

        [Pure] 
        public AsyncCloudBlobDirectory Parent
        {
            get { return new AsyncCloudBlobDirectory(Inner.Parent); }
        }

        [Pure] 
        public AsyncCloudBlobContainer Container
        {
            get { return new AsyncCloudBlobContainer(Inner.Container); }
        }

    }
}