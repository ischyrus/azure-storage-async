using System.Net;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Porges.WindowsAzure.Storage.Async.Table;

namespace Porges.WindowsAzure.Storage.Async.Test.Integration
{
    [TestClass]
    public abstract class CloudTableTests
    {
        private string _tableUnderTest;
        private readonly CloudStorageAccount _account;
        private AsyncCloudTable _table;

        protected CloudTableTests(CloudStorageAccount account)
        {
            _account = account;

        }

        [TestInitialize]
        public void Setup()
        {
            _tableUnderTest = TableName.GenerateUnique();
            _table = new AsyncCloudTableClient(_account.CreateCloudTableClient()).GetTableReference(_tableUnderTest);
        }

        [TestCleanup]
        public void Cleanup()
        {
            Task.Run(() => _account.CreateCloudTableClient().GetTableReference(_tableUnderTest).DeleteIfExists());
        }

        [TestMethod]
        public async Task CanCreate()
        {
            Assert.IsFalse(await _table.Exists());
            await _table.Create();
            Assert.IsTrue(await _table.Exists());
        }

        [TestMethod]
        public async Task CanCreateIfNotExists()
        {
            Assert.IsFalse(await _table.Exists());
            Assert.IsTrue(await _table.CreateIfNotExists());
            Assert.IsTrue(await _table.Exists());
        }

        [TestMethod]
        public async Task CantCreateAgain()
        {
            Assert.IsFalse(await _table.Exists());
            Assert.IsTrue(await _table.CreateIfNotExists());
            Assert.IsTrue(await _table.Exists());
            Assert.IsFalse(await _table.CreateIfNotExists());
            Assert.IsTrue(await _table.Exists());
        }

        [TestMethod]
        [ExpectedException(typeof(StorageException))]
        public async Task CantCreateAgainThrows()
        {
            try
            {
                Assert.IsFalse(await _table.Exists());
                await _table.Create();
                Assert.IsTrue(await _table.Exists());
            }
            catch (StorageException)
            {
                Assert.Fail("wrong exception");
            }
            await _table.Create();
        }

        [TestMethod]
        public async Task CantDeleteNonExistent()
        {
            Assert.IsFalse(await _table.Exists());
            Assert.IsFalse(await _table.DeleteIfExists());
        }

        [TestMethod]
        [ExpectedException(typeof(StorageException))]
        public async Task CantDeleteNonExistentThrows()
        {
            try
            {
                Assert.IsFalse(await _table.Exists());
            }
            catch (StorageException)
            {
                Assert.Fail("wrong exception");
            }

            await _table.Delete();
        }

        [TestMethod]
        public async Task CanDelete()
        {
            await CanCreate();
            await _table.Delete();
            Assert.IsFalse(await _table.Exists());
        }

        [TestMethod]
        public async Task CanDeleteIfExists()
        {
            await CanCreate();
            Assert.IsTrue(await _table.DeleteIfExists());
            Assert.IsFalse(await _table.Exists());
        }

        [TestMethod]
        public async Task CanInsert()
        {
            await CanCreate();
            var result = await _table.Execute(TableOperation.Insert(new TableEntity("a", "a")));
            Assert.AreEqual((int)HttpStatusCode.Created, result.HttpStatusCode);
        }
    }
}
