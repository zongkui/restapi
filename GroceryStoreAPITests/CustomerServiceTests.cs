using GroceryStoreAPI.DataAccess;
using GroceryStoreAPI.Models;
using GroceryStoreAPI.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace GroceryStoreAPITests
{
    [TestClass]
    public class CustomerServiceTests
    {
        ICustomerService _service;

        [TestInitialize]
        public void Setup()
        {
            Mock<IRepository> repository = new Mock<IRepository>();
            repository.Setup(x => x.GetAll()).Returns(MockGetAll());
            repository.Setup(x => x.Get(It.IsAny<int>())).Returns(MockGet());
            repository.Setup(x => x.Save(It.IsAny<CustomerModel>())).Returns(MockSave());
            _service = new CustomerService(repository.Object);
        }

        [TestMethod]
        public void GetAllTest()
        {
            var found = _service.GetAll();
            Assert.IsNotNull(found);
            Assert.AreEqual(MockGetAll().Count, found.Count);
        }

        [TestMethod]
        public void GetTest()
        {
            int id = 1;
            var found = _service.Get(id);
            Assert.IsNotNull(found);
            Assert.AreEqual(id, found.Id);
        }

        [TestMethod]
        public void CreateTest()
        {
            string name = "Coolidge";
            var customer = new CustomerUpdateModel { Name = name };
            var created = _service.Create(customer);
            Assert.IsTrue(created!=0);
        }

        [TestMethod]
        public void UpdateTest()
        {
            int id = 2;
            string name = "Coolidge";
            var customer = new CustomerUpdateModel { Name = name };
            _service.Update(id, customer);
        }

        [TestMethod]
        public void DeleteTest()
        {
            int id = 2;
            _service.Delete(id);
        }

        private List<CustomerModel> MockGetAll() => new List<CustomerModel>
        {
            new CustomerModel {Id = 1, Name = "Joseph"},
            new CustomerModel {Id = 2, Name = "Ruth"},
            new CustomerModel {Id = 3, Name = "Chuck"}
        };

        private CustomerModel MockGet() => new CustomerModel { Id = 1, Name = "Joseph" };

        private CustomerModel MockSave() => new CustomerModel { Id = 9, Name = "Coolidge" };

    }
}
