using Microsoft.WindowsAzure.Storage.Blob;

namespace Porges.WindowsAzure.Storage.Async.Blob
{
    internal static class AsyncCloudBlobHelpers
    {
        public static IAsyncCloudBlob FromICloudBlob(ICloudBlob cloudBlob)
        {
            var block = cloudBlob as CloudBlockBlob;
            if (block != null)
            {
                return new AsyncCloudBlockBlob(block);
            }

            var page = cloudBlob as CloudPageBlob;
            if (page != null)
            {
                return new AsyncCloudPageBlob(page);
            }

            // unknown blob type... TODO - it might be better to actually throw an exception here
            return new AsyncCloudBlobBase<ICloudBlob>(cloudBlob);
        }
    }
}