using System;
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

        public Uri Uri
        {
            get { return Inner.Uri; }
        }

        public AsyncCloudBlobDirectory Parent
        {
            get { return new AsyncCloudBlobDirectory(Inner.Parent); }
        }

        public AsyncCloudBlobContainer Container
        {
            get { return new AsyncCloudBlobContainer(Inner.Container); }
        }

    }
}