using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.WindowsAzure.Storage;

namespace Porges.WindowsAzure.Storage.Async.Test.Integration
{
    [TestClass]
    public class DevStorageCloudTableClientTests : CloudTableClientTests
    {
        public DevStorageCloudTableClientTests() : base(CloudStorageAccount.DevelopmentStorageAccount)
        { }
    }

    [TestClass]
    public class DevStorageCloudTableTests : CloudTableTests
    {
        public DevStorageCloudTableTests(): base(CloudStorageAccount.DevelopmentStorageAccount)
        { }
    }
}
