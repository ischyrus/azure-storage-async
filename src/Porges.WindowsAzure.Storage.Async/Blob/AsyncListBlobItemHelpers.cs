﻿using System.Diagnostics.Contracts;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Porges.WindowsAzure.Storage.Async.Blob
{
    public static class AsyncListBlobItemHelpers
    {
        [Pure] 
        public static IAsyncListBlobItem FromIListBlobItem(IListBlobItem result)
        {
            var blob = result as ICloudBlob;
            if (blob != null)
            {
                return AsyncCloudBlobHelpers.FromICloudBlob(blob);
            }

            var dir = result as CloudBlobDirectory;
            if (dir != null)
            {
                return new AsyncCloudBlobDirectory(dir);
            }

            // TODO: maybe better to throw an exception
            return new AsyncListBlobItemBase<IListBlobItem>(result);
        }
    }
}