using GroceryStoreAPI.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace GroceryStoreAPITests
{
    [TestClass]
    public class FileRepositoryTests : FileRepository
    {
        [TestInitialize]
        public void Setup()
        {
            _customers = new List<CustomerModel>
            { 
                new CustomerModel {Id = 1, Name = "Peter"},
                new CustomerModel {Id = 2, Name = "Mary" },
                new CustomerModel { Id = 3, Name = "Paul"}
            };
        }

        [TestMethod]
        public void InsertRecord()
        {
            CustomerModel customer = new CustomerModel { Name = "John" };
            int nextId = _customers.Max(x => x.Id) + 1;
            int count = _customers.Count();
            var inserted = Save(customer);

            Assert.IsNotNull(inserted);
            Assert.AreEqual(nextId, inserted.Id);
            Assert.AreEqual(count + 1, _customers.Count());
        }

        [TestMethod]
        public void UpdateRecord()
        {
            int id = 2;
            CustomerModel customer = new CustomerModel { Id = id, Name = "Rose" };
            int count = _customers.Count();
            var inserted = Save(customer);

            Assert.IsNotNull(inserted);
            Assert.AreEqual(id, inserted.Id);
            Assert.AreEqual(count, _customers.Count());
            var found = _customers.FirstOrDefault(x => x.Id == id);
            Assert.IsNotNull(found);
            Assert.AreEqual("Rose", found.Name);
        }

        [TestMethod]
        public void DeleteRecord()
        {
            int id = 2;
            int count = _customers.Count();
            Delete(id);

            Assert.AreEqual(count-1, _customers.Count());
            var found = _customers.FirstOrDefault(x => x.Id == id);
            Assert.IsNull(found);
        }

        [TestMethod]
        public void GetRecord()
        {
            int id = 2;
            var found = Get(id);

            Assert.IsNotNull(found);
            Assert.AreEqual(id, found.Id);
        }

        [TestMethod]
        public void GetRecordInvalidId()
        {
            int id = 0;
            var found = Get(id);

            Assert.IsNull(found);
        }

        [TestMethod]
        public void GetRecordNotFound()
        {
            int id = 100;
            var found = Get(id);

            Assert.IsNull(found);
        }

        [TestMethod]
        public void GetRecordAll()
        {
            var found = GetAll();

            Assert.IsNotNull(found);
            Assert.AreEqual(_customers.Count(), found.Count());
        }
    }
}
