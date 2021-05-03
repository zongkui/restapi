using GroceryStoreAPI.DataAccess;
using GroceryStoreAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace GroceryStoreAPI.Services
{
    public class CustomerService : ICustomerService
    {
        IRepository _repository;
        public CustomerService(IRepository repository)
        {
            _repository = repository;
        }
        public List<CustomerReadModel> GetAll()
        {
            var list = _repository.GetAll();
            return list?.Select(x => ToViewModel(x)).ToList();
        }
        public CustomerReadModel Get(int id)
        {
            var record = _repository.Get(id);
            return record == null ? null : ToViewModel(record);
        }
        public int Create(CustomerUpdateModel customer)
        {
            var model = ToModel(customer);
            return _repository.Save(model).Id;
        }
        public void Update(int id, CustomerUpdateModel customer)
        {
            var model = ToModel(customer);
            model.Id = id;
            _repository.Save(model);
        }
        public void Delete(int id)
        {
            _repository.Delete(id);
        }
        public string ValidateUpdate(CustomerUpdateModel customer)
        {
            if (string.IsNullOrWhiteSpace(customer.Name))
            {
                return "Missing value for Name";
            }
            return null;
        }

        public CustomerModel ToModel(CustomerUpdateModel customer)
        {
            if (customer == null) 
            { 
                return null; 
            }
            return new CustomerModel { Name = customer.Name };
        }
        public CustomerReadModel ToViewModel(CustomerModel customer)
        {
            if (customer == null)
            {
                return null;
            }
            return new CustomerReadModel
            {
                Id = customer.Id,
                Name = customer.Name
            };
        }

        public bool Exists(int id)
        {
            return _repository.Get(id) != null;
        }
    }
}
