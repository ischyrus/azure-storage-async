using System.Reactive.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.WindowsAzure.Storage;
using Porges.WindowsAzure.Storage.Async.Table;

namespace Porges.WindowsAzure.Storage.Async.Test.Integration
{
    [TestClass]
    public abstract class CloudTableClientTests
    {
        private readonly AsyncCloudTableClient _client;

        protected CloudTableClientTests(CloudStorageAccount testAccount)
        {
            _client = new AsyncCloudTableClient(testAccount);
        }

        [TestMethod]
        public async Task CanListTables()
        {
            var tables = await _client.ListTables(null, null).ToList();

            Assert.IsNotNull(tables);
        }

        [TestMethod]
        [Ignore]
        public async Task CanGetServiceProperties()
        {
            var x = await _client.GetServiceProperties();

            Assert.IsNotNull(x);
        }
    }
}
